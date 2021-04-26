using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TowerUpgradeButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    Color border;
    Color background;
    Color labelStart;

    
    Image buttonBorder;
    [SerializeField]
    Image buttonBackground;
    [SerializeField]
    TextMeshProUGUI buttonLabel;

    Tower selectedTower;

    void Start()
    {
        buttonBorder = GetComponent<Image>();
        border = buttonBorder.color;
        background = buttonBackground.color;
        labelStart = buttonLabel.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                Clicked();
                break;
            default:
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonLabel.color = background;
        buttonBackground.color = border;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonLabel.color = labelStart;
        buttonBackground.color = background;
    }

    public void SetText(int price){
        buttonLabel.text = "Upgrade $" + price.ToString();
    }
    public void SetUp(Tower tower){
        selectedTower = tower;
        SetText(tower.UpgradePrice);
    }
    private void Clicked()
    {
        if(UIManager.Instance.baseObj.Currency >= selectedTower.UpgradePrice){
        selectedTower.Upgrade();
        SetText(selectedTower.UpgradePrice);
        }
    }
}