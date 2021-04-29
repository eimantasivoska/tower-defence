using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayPause : MonoBehaviour, IPointerClickHandler
{
    bool started;
    bool inProgress;
    [SerializeField]
    Sprite playSprite;
    [SerializeField]
    Sprite pauseSprite;
    [SerializeField]
    Image buttonImage;
    void Start()
    {
        started = false;
        inProgress = false;
        WaveManager.Instance.WaveEnded += Ended;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (started)
        {
            buttonImage.sprite = playSprite;
            started = false;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 2;
            buttonImage.sprite = pauseSprite;
            started = true;
            if (!inProgress)
            {
                inProgress = true;
                WaveManager.Instance.StartNextWave();
            }
        }
    }
    void Ended()
    {
        inProgress = false;
        buttonImage.sprite = playSprite;
        started = false;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
