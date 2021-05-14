using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;


enum EnemyType
{
    RedDiamond,
    GreenSpinner,
    PinkDiamond
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance {set; get;}

    public delegate void WaveEndHandler();
    public event WaveEndHandler WaveEnded;
    #region Inspector 
    [SerializeField]
    int enemiesToSpawnThisWave = 0;
    [SerializeField]
    int spawned = 0;
    [SerializeField]
    public int CurrentWave;

    [Header("Timing")]
    [SerializeField]
    private float spawnCooldown = 1f;
    [SerializeField]
    private float waveCooldown = 1f;

    [Space]
    [Header("Enemies")]
    [SerializeField]
    private int startingEnemyCount = 5;
    [SerializeField]
    private float[] enemyHealthStartValue;
    [SerializeField]
    private int[] enemyBaseCoinDropValue;
    [SerializeField]
    private float WaveStrengthMultiplier = 1.2f;
    [SerializeField]
    private int[] SpawnPoints = { 1, 5, 10 };
    [Range(1,10)]
    public int baseSpawnPoints = 4;

    [Space]
    [SerializeField]
    private GameObject[] enemyPrefabs;
    #endregion

    #region OtherFields

    private List<GameObject> aliveEnemies;

    public Base baseObj {get; private set; }

    bool waveEnded = true;
    #endregion

    void Awake(){
        if(Instance == null)
            Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        Initialize();
    //    Bangos taškų didėjimo testavimas.
    //    using(var w = new StreamWriter("D:\\Test.csv"))
    //    {
    //        for(int i=1; i <= 200; i++)
    //           w.WriteLine(string.Format("{0},{1}", i, GetSpawnPoints(i)));
    //   }
    }

    void Initialize(){
        aliveEnemies = new List<GameObject>();
        baseObj = GameObject.Find("Base").GetComponent<Base>();
        CurrentWave = 0;
    }

    void Countdown(){
        enemiesToSpawnThisWave = GetEnemyCountToSpawn();
        int spawn = enemiesToSpawnThisWave;
        spawned = 0;
        CurrentWave++;
        waveEnded = false;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Stack<EnemyType> enemiesToSpawn = SpawnList();
        enemiesToSpawnThisWave = enemiesToSpawn.Count;
        while (enemiesToSpawn.Count > 0)
        {
            SpawnEnemy(enemiesToSpawn.Pop());
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    int GetEnemyCountToSpawn(){
        return startingEnemyCount + CurrentWave;
    }
    float GetEnemyStats(EnemyType type){
        return CurrentWave * WaveStrengthMultiplier * enemyHealthStartValue[(int)type];
    }

    //Temporary, will be adjusted
    int GetEnemyCoinDrop(EnemyType type){
        return enemyBaseCoinDropValue[(int)type] + GetEnemyCountToSpawn();
    }

    Stack<EnemyType> SpawnList()
    {
        int spawnPoints = GetSpawnPoints(CurrentWave);
        int sp = spawnPoints;
        Stack<EnemyType> stack = new Stack<EnemyType>();
        int r = 0, g = 0, p = 0;
        while(spawnPoints > 0)
        {
            if(spawnPoints >= SpawnPoints[2])
            {
                stack.Push(EnemyType.PinkDiamond);
                spawnPoints -= SpawnPoints[2];
                p++;
            }
            else if(spawnPoints >= SpawnPoints[1])
            {
                stack.Push(EnemyType.GreenSpinner);
                spawnPoints -= SpawnPoints[1];
                g++;
            }
            else
            {
                stack.Push(EnemyType.RedDiamond);
                spawnPoints--;
                r++;
            }
        }
        return stack;
    }

    int GetSpawnPoints(int wave)
    {
        baseSpawnPoints = (int)(baseSpawnPoints + wave * GameManager.Instance.GetMultiplier());
        return baseSpawnPoints;
    }

    void SpawnEnemy(EnemyType type)
    {
        System.Random r = new System.Random();
        float stats = GetEnemyStats(type);
        int drop = GetEnemyCoinDrop(type);
        GameObject obj = Instantiate(enemyPrefabs[(int)type], this.transform);
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.Initialize(stats, 10f, drop);
        aliveEnemies.Add(obj);
        spawned++;
    }

    public int WaveReward()
    {
        return 50 + 10 * (CurrentWave - 1);
    }
    /// <summary>
    /// Method to execute a another method multiple times
    /// </summary>
    /// <param name="method">Method to execute</param>
    /// <param name="time">Time in between executes</param>
    /// <param name="count">Amount of times to execute (default 1)</param>
    /// <param name="preOrder">Bool to set wether to run code, then wait</param>

    void Run(Action method, float time, int count = 1, bool preOrder = true){
        StartCoroutine(Execute(()=> method(), time, count, preOrder));
    }

    public void StartNextWave()
    {
        if (aliveEnemies.Count == 0)
        {
            Run(() => Countdown(), waveCooldown, 1, false);
        }
        else
        {
            throw new Exception("There are still some alive enemies");
        }
    }

    public void EnemyDied(GameObject enemy) {
        aliveEnemies.Remove(enemy);
        if(spawned == enemiesToSpawnThisWave && aliveEnemies.Count == 0 && !waveEnded)
        {
            waveEnded = true;
            WaveEnded.Invoke();
            UIManager.Instance.WaveClearedReward();
        }
    }

    #region Coroutines
    IEnumerator Execute(Action method, float time, int count, bool preOrder){
        if(preOrder)
            method();
        yield return new WaitForSeconds(time); 
        if(!preOrder)
            method();
        if(count > 1)
            StartCoroutine(Execute(() => method(), time, count - 1, preOrder));
    }
    #endregion
}
