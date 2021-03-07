using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject tower;

    void OnMouseDown()
    {
        string msg = transform.position + " cell is "+ ((gameObject.tag == "Buildable") ? "" : "not ") + "buildable";
        msg += "\nIt does " + ((tower == null) ? "not " : "") + " have a tower";
        Debug.Log(msg);
    }
}
