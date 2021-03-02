using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;

    void Start()
    {
        StartCoroutine(Spawn(1f));
    }

    IEnumerator Spawn(float time)
    {
        yield return new WaitForSeconds(time);
        Enemy e = Instantiate(enemy, gameObject.transform).GetComponent<Enemy>();
        System.Random r = new System.Random();
        e.Initialize(10, 10);
        StartCoroutine(Spawn(time));
    }
}
