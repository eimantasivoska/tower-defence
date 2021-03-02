using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(1f));
    }

    IEnumerator Spawn(float time)
    {
        yield return new WaitForSeconds(time);
        Enemy e = Instantiate(enemy, gameObject.transform).GetComponent<Enemy>();
        System.Random r = new System.Random();
        e.Initialize(r.Next(0, 200), r.Next(0, 200));
        StartCoroutine(Spawn(time));
    }
}
