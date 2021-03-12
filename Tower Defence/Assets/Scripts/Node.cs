using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject tower;
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

    void OnMouseDown()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
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

    void OnMouseEnter()
    {
        if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            if(!isSelected)
                r.material.color = HoverColor;
    }

    void OnMouseExit()
    {
        if (!isSelected)
            r.material.color = startColor;
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
}
