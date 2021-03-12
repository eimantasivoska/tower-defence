using UnityEngine.EventSystems;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    Node hovering = null;

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Buildable")
                {
                    if (hit.transform.gameObject != hovering)
                    {
                        if (hovering != null)
                            hovering.OnExit();
                        hovering = hit.transform.gameObject.GetComponent<Node>();
                        hovering.OnEnter();
                    }
                }
                else
                {
                    if (hovering != null)
                        hovering.OnExit();
                    hovering = null;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (hovering == null)
                return;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                hovering.OnClick();
            }
        }
    }
}
