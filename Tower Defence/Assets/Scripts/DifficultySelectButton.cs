using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultySelectButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject background;
    [SerializeField]
    GameObject background1;
    [SerializeField]
    GameObject background2;
    [SerializeField]
    Difficulty difficulty;

    public void OnPointerClick(PointerEventData eventData)
    {
        background.SetActive(true);
        background1.SetActive(false);
        background2.SetActive(false);
        GameManager.Instance.SetDifficulty(difficulty);
    }
}
