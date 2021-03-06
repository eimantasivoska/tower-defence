﻿using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { set; get; }

    [Header("UI elements")]
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    TextMeshProUGUI coinsText;
    [SerializeField]
    [Space]

    [Header("Prefabs")]
    GameObject rangeSpherePrefab;
    [SerializeField]
    GameObject[] TowerPrefabs;

    GameObject rangeSphere;

    Node selected;

    [Header("UI Panels")]

    [SerializeField]
    GameObject buildPanel;

    [SerializeField]
    GameObject infoPanel;

    [SerializeField]
    TextMeshProUGUI towerNameText;

    [SerializeField]
    TextMeshProUGUI towerDamageText;

    [SerializeField]
    TextMeshProUGUI towerEliminationsText;

    [SerializeField]
    TextMeshProUGUI towerLevelText;
    [SerializeField]
    TextMeshProUGUI pointsText;
    [SerializeField]
    GameObject UpgradeButton;
    [SerializeField]
    GameObject SellButton;
    [SerializeField]
    TextMeshProUGUI waveText;

    public Vector3 rangeObjectOffset;

    public Base baseObj { get; private set; }
    Animator BuildMenuAnimation;
    Animator InfoMenuAnimation;
    void Start()
    {
        baseObj = GameObject.Find("Base").GetComponent<Base>();
        BuildMenuAnimation = buildPanel.GetComponent<Animator>();
        InfoMenuAnimation = infoPanel.GetComponent<Animator>();
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // TESTING DESELECTING A NODE WHEN CLICKING ESC
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnNodeSelected(null);
    }

    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }
    public void UpdateCoins(int coins){
        coinsText.text = $"{coins/100}{coins%100/10}{coins%10}";
    }
    public void UpdatePoints(int points){
        pointsText.text = points.ToString();
    }
    public void UpdateWave(int number)
    {
        waveText.text = number.ToString();
    }
    public void UpdateEliminations(Tower tower)
    {
        if(selected != null && selected.tower != null && selected.tower.GetComponent<Tower>() == tower)
            towerEliminationsText.text = tower.Eliminations.ToString();
    }

    public void OnNodeSelected(Node node)
    {
        ClearRange();
        if (selected != null)
            selected.Deselect();
        if (node == null)
        {
            ToggleMenu(false);
            selected = null;
        }
        else
        {
            selected = node;
            if(node.tower != null)
            {
                SetupTowerInfoPanel(node.tower.GetComponent<Tower>());
            }
            ToggleMenu(true);
        }
    }

    public void OnTowerButtonClick(int i, int price)
    {
        Node clicked = selected.GetComponent<Node>();
        if(clicked.isBuildable){
            if (baseObj.Currency >= price)
            {
                baseObj.SpentCoins(price);
                clicked.tower = Instantiate(TowerPrefabs[i], selected.gameObject.transform.position, selected.gameObject.transform.rotation);
                ClearRange();
                SetupTowerInfoPanel(clicked.tower.GetComponent<Tower>());
                ToggleMenu(false);
            }
            else
            {
                print("Not enough money");
            }
        }
    }

    public void DisplayRange(float range)
    {
        rangeSphere = Instantiate(rangeSpherePrefab, selected.gameObject.transform.position + rangeObjectOffset, selected.gameObject.transform.rotation);
        rangeSphere.GetComponent<Range>().DisplayRange(range);
    }

    public void DisplayRange(int towerID)
    {
        float range = TowerPrefabs[towerID].GetComponent<SphereCollider>().radius * 2;
        DisplayRange(range); 
    }

    public void ClearRange()
    {
        if (rangeSphere != null)
            Destroy(rangeSphere);
    }

    public void ToggleMenu(bool show)
    {
        if (show)
        {
            if (selected.tower == null)
            {
                BuildMenuAnimation.SetBool("Open", show);
                InfoMenuAnimation.SetBool("Open", !show);
            }
            else
            {
                BuildMenuAnimation.SetBool("Open", !show);
                InfoMenuAnimation.SetBool("Open", show);
                DisplayRange(selected.tower.GetComponent<SphereCollider>().radius * 2);
            }
        }
        else
        {
            BuildMenuAnimation.SetBool("Open", false);
            InfoMenuAnimation.SetBool("Open", false);
        }
    }
    public void SetupTowerInfoPanel(Tower tower)
    {
        towerNameText.text = tower.Name;
        towerDamageText.text = (Math.Round(tower.Damage/tower.AttackCooldown, 0)).ToString() + " Dps";
        towerLevelText.text = tower.Level.ToString();
        towerEliminationsText.text = tower.Eliminations.ToString();
        UpgradeButton.GetComponent<TowerUpgradeButton>().SetUp(tower);
        SellButton.GetComponent<SellButton>().SetUp(tower);
    }

    public void WaveClearedReward()
    {
        int reward = WaveManager.Instance.WaveReward();
        baseObj.GotCoins(reward);
        baseObj.GotPoints(reward);
    }
}
