using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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
    private float enemyHealthStartValue = 10f;
    [SerializeField]
    private int enemyBaseCoinDropValue = 10;
    [SerializeField]
    private float WaveStrengthMultiplier = 1.2f;

    [Space]
    [Header("Temporary")]
    [SerializeField]
    private GameObject[] enemyPrefabs;
    #endregion

    #region OtherFields

    private List<GameObject> aliveEnemies;

    public Base baseObj {get; private set; }

    
    #endregion

    void Awake(){
        if(Instance == null)
            Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        Initialize();
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
        print($"{CurrentWave} wave incomming!");
        Run(() => SpawnEnemy(), spawnCooldown, spawn);
    }

    int GetEnemyCountToSpawn(){
        //return 2 * (CurrentWave - 1) + startingEnemyCount;
        return startingEnemyCount + CurrentWave;
    }
    float GetEnemyStats(){
        return CurrentWave * WaveStrengthMultiplier * enemyHealthStartValue;
    }

    //Temporary, will be adjusted
    int GetEnemyCoinDrop(){
        return enemyBaseCoinDropValue + GetEnemyCountToSpawn();
    }

    void SpawnEnemy()
    {
        System.Random r = new System.Random();
        float stats = GetEnemyStats();
        int drop = GetEnemyCoinDrop();
        GameObject obj = Instantiate(enemyPrefabs[r.Next(enemyPrefabs.Length)], this.transform);
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.Initialize(stats, 10f, drop);
        aliveEnemies.Add(obj);
        spawned++;
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
        if(spawned == enemiesToSpawnThisWave && aliveEnemies.Count == 0)
        {
            WaveEnded.Invoke();
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
