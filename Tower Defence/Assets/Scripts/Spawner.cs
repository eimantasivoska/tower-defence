using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    static int enemyCount;
    public GameObject enemy;

    void Start()
    {
        enemyCount = 0;
        StartCoroutine(Spawn(1f));
    }

    IEnumerator Spawn(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject enemyObj = Instantiate(enemy, gameObject.transform);
        enemyObj.name = "Enemy " + ++enemyCount;
        Enemy e = enemyObj.GetComponent<Enemy>();
        System.Random r = new System.Random();
        //e.Initialize(10f, 10f);
        StartCoroutine(Spawn(time));
    }
}
