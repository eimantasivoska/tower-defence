using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [Header("Button")]
    [SerializeField]
    int ButtonID;

    [SerializeField]
    TextMeshProUGUI PriceText;

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

    void Start()
    {
        PriceText.text = "Price: " + price;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.OnTowerButtonClick(ButtonID, price);
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
