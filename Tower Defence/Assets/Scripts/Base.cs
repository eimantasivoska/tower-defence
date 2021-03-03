﻿using System.Collections;
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

    public void Initialize(int health, int currency)
    {
        Health = health;
        Currency = currency;
    }
    public void TakeDamage(int damage)
    {
        if (Health > damage)
        {
            Health -= damage;
            UIManager.Instance.UpdateHealth(Health);
        }
        else
            Death();
    }
    void Death()
    {
        Health = 0;
        UIManager.Instance.UpdateHealth(Health);
        GameOver.Instance.Death();
    }
}
