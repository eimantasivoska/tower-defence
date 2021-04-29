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
        if(selectedTower.Level == 5){
            return;
        }
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
        if(selectedTower.Level == 5){
            return;
        }
        buttonLabel.color = background;
        buttonBackground.color = border;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonLabel.color = labelStart;
        if(selectedTower.Level == 5){
            return;
        }
        buttonBackground.color = background;
    }

    public void SetText(int price){
        buttonLabel.text = "Upgrade $" + price.ToString();
    }
    public void SetUp(Tower tower){
        selectedTower = tower;
        if(selectedTower.Level == 5){
            buttonLabel.text = "Max level";
        }
        else{
            SetText(tower.UpgradePrice);
        }
    }
    private void Clicked()
    {
        if(UIManager.Instance.baseObj.Currency >= selectedTower.UpgradePrice)
        {
            UIManager.Instance.baseObj.SpentCoins(selectedTower.UpgradePrice);
            selectedTower.Upgrade();
            SetText(selectedTower.UpgradePrice);
            UIManager.Instance.SetupTowerInfoPanel(selectedTower);
            if(selectedTower.Level == 5){
                 buttonBackground.color = background;
                 buttonLabel.color = labelStart;
                 return;
            }
        }
    }
}