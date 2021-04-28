using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float Health  {get; private set;}
    public int Currency { get; private set; }
    public int Points { get; private set; }

    void Start()
    {
        Health = 100;
        Currency = 500;
        Points = 0;
        UIManager.Instance.UpdateCoins(Currency);
    }

    public void Initialize(int health, int currency, int points)
    {
        Health = health;
        Currency = currency;
        Points = points;
    }
    public void TakeDamage(float damage)
    {
        if (Health > damage)
        {
            Health -= damage;
            UIManager.Instance.UpdateHealth((int)Health);
        }
        else
            Death();
    }
    public void GotPoints(int points)
    {
        Points += points;
        print(Points);
        //UIManager.Instance.UpdatePoints(Points);
    }
    public void GotCoins(int coins){
        Currency += coins;
        UIManager.Instance.UpdateCoins(Currency);
    }
    public void SpentCoins(int coins)
    {
        Currency -= coins;
        UIManager.Instance.UpdateCoins(Currency);
    }
    void Death()
    {
        Health = 0;
        UIManager.Instance.UpdateHealth((int)Health);
        GameOver.Instance.Death();
    }
}
