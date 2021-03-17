using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { set; get; }

    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    GameObject rangeSpherePrefab;
    [SerializeField]
    GameObject[] TowerPrefabs;

    GameObject rangeSphere;

    Node selected;

    [SerializeField]
    GameObject buildPanel;

    [SerializeField]
    GameObject infoPanel;

    public Vector3 rangeObjectOffset;

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
            ToggleMenu(true);
        }
    }

    public void OnTowerButtonClick(int i)
    {
        Node clicked = selected.GetComponent<Node>();
        if(clicked.isBuildable){
            clicked.tower = Instantiate(TowerPrefabs[i], selected.gameObject.transform.position, selected.gameObject.transform.rotation);
            ClearRange();
            ToggleMenu(true);
        }
    }

    public void DisplayRange(float range)
    {
        rangeSphere = Instantiate(rangeSpherePrefab, selected.gameObject.transform.position + rangeObjectOffset, selected.gameObject.transform.rotation);
        rangeSphere.GetComponent<Range>().DisplayRange(range);
    }

    public void ClearRange()
    {
        if (rangeSphere != null)
            Destroy(rangeSphere);
    }

    private void ToggleMenu(bool show)
    {
        if (show)
        {
            if (selected.tower == null)
            {
                buildPanel.SetActive(show);
                infoPanel.SetActive(!show);
            }
            else
            {
                buildPanel.SetActive(!show);
                infoPanel.SetActive(show);
                DisplayRange(selected.tower.GetComponent<SphereCollider>().radius * 2);
            }
        }
        else
        {
            buildPanel.SetActive(false);
            infoPanel.SetActive(false);
        }
    }
}
