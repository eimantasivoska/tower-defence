using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour { 
    public int Health { private set; get; }
    public int Damage { private set; get; }

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
    public void Initialize(int health, int damage)
    {
        Health = health;
        Damage = damage;
    }
}
