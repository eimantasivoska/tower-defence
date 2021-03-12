using UnityEngine;

public class Node : MonoBehaviour
{
    GameObject tower;
    Renderer r;

    [SerializeField]
    Color HoverColor;

    [SerializeField]
    Color SelectedColor;

    Color startColor;

    bool isSelected = false;

    void Start()
    {
        r = GetComponent<Renderer>();
        startColor = r.material.color;
    }

    void Select()
    {
        isSelected = true;
        r.material.color = SelectedColor;
        UIManager.Instance.OnNodeSelected(this);
    }

    public void Deselect()
    {
        isSelected = false;
        r.material.color = startColor;
    }

    public void OnEnter()
    {
        if (!isSelected && Time.timeScale == 1)
            r.material.color = HoverColor;
    }

    public void OnExit()
    {
        if (!isSelected && Time.timeScale == 1)
            r.material.color = startColor;
    }

    public void OnClick()
    {
        if (Time.timeScale == 1)
        {
            string msg = transform.position + " cell is " + ((gameObject.tag == "Buildable") ? "" : "not ") + "buildable";
            msg += "\nIt does " + ((tower == null) ? "not " : "") + " have a tower";
            //if (gameObject.tag != "Buildable")
            //    return;
            Debug.Log(msg);
            if (isSelected)
                UIManager.Instance.OnNodeSelected(null);
            else
                Select();
        }
    }
}
