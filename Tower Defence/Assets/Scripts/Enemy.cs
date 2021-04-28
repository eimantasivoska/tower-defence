using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour { 
    public float StartHealth { private set; get; }
    public float Health { private set; get; }
    public float Damage { private set; get; }
    public int CoinDrop {private set; get;}

    NavMeshAgent navAgent;

    protected List<GameObject> Towers;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(GameObject.Find("DeathBox").transform.position);
        Towers = new List<GameObject>();
    }

    /// <summary>
    /// Initializing the enemy script
    /// </summary>
    /// <param name="health">Health points</param>
    /// <param name="damage">Damage</param>
    public void Initialize(float health, float damage, int coinDrop)
    {
        StartHealth = health;
        Health = health;
        Damage = damage;
        CoinDrop = coinDrop;
    }
    public void TakeDamage(float amount){
        Health -= amount;
        if(Health <= 0){
            Killed();
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Tower")
        {
            Towers.Add(col.gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Tower")
        {
            Towers.Remove(col.gameObject);
        }
    }
    public void Die()
    {
        foreach(GameObject t in Towers)
        {
            t.GetComponent<Tower>().SetDead(gameObject);
        }
        WaveManager.Instance.EnemyDied(gameObject);
        Destroy(gameObject);
    }

    public void Killed(){
        WaveManager.Instance.baseObj.GotCoins(CoinDrop);
        WaveManager.Instance.baseObj.GotPoints((int)StartHealth);
        Die();
    }
}
