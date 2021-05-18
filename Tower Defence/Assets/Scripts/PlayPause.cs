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
    
    Image background;

    [SerializeField]
    Color Green;
    [SerializeField]
    Color Gray;
    void Start()
    {
        background = gameObject.GetComponent<Image>();
        started = false;
        inProgress = false;
        WaveManager.Instance.WaveEnded += Ended;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!started)
        {
            background.color=Gray;
            Time.timeScale = 2;
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
        background.color=Green;
        inProgress = false;
        buttonImage.sprite = playSprite;
        started = false;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
