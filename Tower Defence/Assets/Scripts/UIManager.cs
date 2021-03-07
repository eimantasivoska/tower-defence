using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { set; get; }

    [SerializeField]
    TextMeshProUGUI healthText;

    Node selected;

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
        if(selected != null)
            selected.Deselect();
        selected = node;
    }
}
