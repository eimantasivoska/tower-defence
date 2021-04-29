using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    public void PlayGame()
    {
        SceneManager.LoadScene(GameManager.Instance.GetSelectedLevel());
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Select()
    {
        panel.SetActive(true);
    }
}
