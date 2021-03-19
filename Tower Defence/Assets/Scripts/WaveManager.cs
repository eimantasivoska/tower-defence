using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance {set; get;}
    #region Inspector 
    [SerializeField]
    public int AliveEnemies;
    [SerializeField]
    public int CurrentWave;

    [Header("Timing")]
    [SerializeField]
    private float gameStartTime = 5f;
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
    private GameObject enemyPrefab;
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
        Run(() => Countdown(), gameStartTime, 1, false);
    }

    void Initialize(){
        aliveEnemies = new List<GameObject>();
        baseObj = GameObject.Find("Base").GetComponent<Base>();
        CurrentWave = 0;
    }

    void Countdown(){
        int enemyCount = GetEnemyCountToSpawn();
        CurrentWave++;
        print($"{CurrentWave} wave incomming!");
        Run(() => SpawnEnemy(), spawnCooldown, enemyCount);
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

    void SpawnEnemy() {
        AliveEnemies++;
        GameObject obj = Instantiate(enemyPrefab, this.transform);
        aliveEnemies.Add(obj);
        Enemy enemy = obj.GetComponent<Enemy>();
        float stats = GetEnemyStats();
        int drop = GetEnemyCoinDrop();
        enemy.Initialize(stats, 10f, drop);
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

    public void EnemyDied(GameObject enemy){
        AliveEnemies--;
        aliveEnemies.Remove(enemy);
        if(aliveEnemies.Count == 0){
            Run(() => Countdown(), waveCooldown, 1, false);
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
