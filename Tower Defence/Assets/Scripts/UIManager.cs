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
        if (selected != null)
            selected.Deselect();
        if (node == null)
        {
            if(rangeSphere != null)
                Destroy(rangeSphere);
            selected = null;
        }
        else
        {
            selected = node;
            if (rangeSphere != null)
                Destroy(rangeSphere);
            rangeSphere = Instantiate(rangeSpherePrefab, selected.gameObject.transform.position + rangeObjectOffset, selected.gameObject.transform.rotation);
            rangeSphere.GetComponent<Range>().DisplayRange(Random.Range(20f, 35f));
        }
    }
    public void OnTowerButtonClick(int i)
    {
        selected.GetComponent<Node>().tower = Instantiate(TowerPrefabs[i], selected.gameObject.transform.position, selected.gameObject.transform.rotation);
    }
}
