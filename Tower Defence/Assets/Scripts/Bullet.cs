using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 50f;
    private GameObject Target;
    private Vector3 offset = new Vector3(0f, 0.7f, 0f);
    Tower tower;

    private float Damage;
    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = Target.transform.position - transform.position + offset;
        float distance = BulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distance)
        {
            Hit();
            return;
        }

        transform.Translate(dir.normalized * distance, Space.World);
    }
    
    public void Shoot(GameObject target, float dmg, Tower tower)
    {
        Target = target;
        Damage = dmg;
        this.tower = tower;
    }

    void Hit()
    {
        Enemy e = Target.GetComponent<Enemy>();
        if (e.TakeDamage(Damage))
        {
            tower.Eliminations++;
            UIManager.Instance.UpdateEliminations(tower);
        }
        Destroy(gameObject);
    }
}
