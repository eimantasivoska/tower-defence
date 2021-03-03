using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { set; get; }

    [SerializeField]
    TextMeshProUGUI healthText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }
}
