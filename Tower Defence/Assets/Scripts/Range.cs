using UnityEngine;

public class Range : MonoBehaviour
{
    public void DisplayRange(float range)
    {
        gameObject.transform.localScale = new Vector3(range, 1f, range);
        Debug.Log("Range set to " + range);
    }
}
