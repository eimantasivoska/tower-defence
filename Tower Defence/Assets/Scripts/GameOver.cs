using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance { set; get; }
    public Base baseObj { get; private set; }

    [SerializeField]
    GameObject deathScreen;
    [SerializeField]
    TextMeshProUGUI YourScore;
    [SerializeField]
    TextMeshProUGUI HighScore;

    void Start(){
        baseObj = GameObject.Find("Base").GetComponent<Base>();
    }
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
        YourScore.text = "Your score: " + baseObj.Points.ToString();
        HighScore.text = "Highscore: " + GameManager.Instance.GetHighScore();
        deathScreen.SetActive(true);

    }
}
