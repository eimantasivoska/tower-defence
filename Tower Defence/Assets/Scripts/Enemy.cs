using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour { 
    public float Health { private set; get; }
    public float Damage { private set; get; }

    NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(GameObject.Find("DeathBox").transform.position);
    }

    /// <summary>
    /// Initializing the enemy script
    /// </summary>
    /// <param name="health">Health points</param>
    /// <param name="damage">Damage</param>
    public void Initialize(float health, float damage)
    {
        Health = health;
        Damage = damage;
    }
    public void TakeDamage(float amount){
        Health -= amount;
        if(Health <= 0)
            Destroy(gameObject);
    }

    ~Enemy(){
        // Handle death
    }
}
