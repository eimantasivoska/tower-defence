using UnityEngine.EventSystems;
using UnityEngine;

public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [Header("Button")]
    [SerializeField]
    int ButtonID;

    [Space]
    [Header("Tower settings")]
    [SerializeField]
    float range;
    [SerializeField]
    int price;
    [SerializeField]
    float damage;
    [SerializeField]
    float AttackCooldown;


    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.OnTowerButtonClick(ButtonID);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.DisplayRange(ButtonID);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.ClearRange();
    }
}
