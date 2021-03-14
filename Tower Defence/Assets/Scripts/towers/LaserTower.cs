using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    LineRenderer laser;
    public Vector3 offset;

    protected override void AttackEnemy(GameObject Enemy)
    {
        laser.SetPosition(1, Enemy.transform.position+offset/2.5f);
    }

    protected override void Initialize()
    {
        Damage = 1;
        AttackCooldown = 0.001f;
        laser = GetComponent<LineRenderer>();
        laser.SetPosition(0, gameObject.transform.position + offset);
        laser.SetPosition(1, gameObject.transform.position + offset);
    }
}
