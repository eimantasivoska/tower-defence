using UnityEngine;

public class Base : MonoBehaviour
{
    public float Health  {get; private set;}
    public int Currency { get; private set; }
    public int Points { get; private set; }

    void Start()
    {
        switch (GameManager.Instance.GetDifficulty())
        {
            case Difficulty.Easy:
                Health = 100;
                break;
            case Difficulty.Medium:
                Health = 80;
                break;
            case Difficulty.Hard:
                Health = 50;
                break;
        }
        Currency = 200;
        Points = 0;
        UIManager.Instance.UpdateHealth((int)Health);
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
        UIManager.Instance.UpdatePoints(Points);
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
