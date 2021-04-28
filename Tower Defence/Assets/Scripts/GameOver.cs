using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance { set; get; }

    [SerializeField]
    GameObject deathScreen;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void OnMainMenuClick()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Death()
    {
        Time.timeScale = 0;
        GameManager.Instance.CheckHighScore(UIManager.Instance.baseObj.Points);
        deathScreen.SetActive(true);

    }
}
