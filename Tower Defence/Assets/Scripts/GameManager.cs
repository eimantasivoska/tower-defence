using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy, Medium, Hard
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [Range(1f, 3f)]
    public float EasyModifier = 1f;
    [Range(1f, 3f)]
    public float MediumModifier = 1.5f;
    [Range(1f, 3f)]
    public float HardModifier = 2f;

    Difficulty difficulty;

    int LevelID = -1;

    void Awake()
    { 
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this);
    }

    public void SetDifficulty(Difficulty dif)
    {
        difficulty = dif;
    }

    public float GetMultiplier()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return EasyModifier;
            case Difficulty.Medium:
                return MediumModifier;
            case Difficulty.Hard:
                return HardModifier;
            default:
                throw new System.ArgumentException("Invalid diffuculty given");
        }
    }

    public void SelectLevel(int level)
    {
        LevelID = level;
    }

    public int GetSelectedLevel()
    {
        return LevelID;
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(difficulty.ToString() + LevelID.ToString(), 0);
    }
    public bool CheckHighScore(int score)
    {
        int Score = GetHighScore();
        if (Score > score)
            return false;
        else
        {
            PlayerPrefs.SetInt(difficulty.ToString() + LevelID.ToString(), score);
            return true;
        }
    }
}
