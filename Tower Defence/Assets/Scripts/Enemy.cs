using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour { 
    public int Health { private set; get; }
    public int Damage { private set; get; }

    NavMeshAgent navAgent;

    Vector3 endPoint = new Vector3(-12.4f, 0f, 3.79f);

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.destination = endPoint;
    }

    void Update()
    {
        /*
        if(Vector3.Distance(gameObject.transform.position,endPoint) < 2f)
        {
            Debug.Log("Health: " + Health + " | Damage: " + Damage);
            Destroy(gameObject);
        }*/
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
