using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int Health  {get; private set;}
    public int Currency { get; private set; }

    void Start()
    {
        Health = 100;
    }
    void Update()
    {
        Debug.Log(Health);
    }
    
    public void Initialize(int health, int currency)
    {
        Health = health;
        Currency = currency;
    }
    public void TakeDamage(int damage)
    {
        if (Health > damage)
            Health -= damage;
        else
            Death();
    }
    void Death()
    {
        Health = 0;
        Destroy(gameObject);
        //GameOver
    }
}
