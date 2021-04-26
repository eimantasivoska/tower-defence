using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    LineRenderer laser;
    public Vector3 offset;
    override public void Upgrade(){

    }
    protected override void AttackEnemy(GameObject Enemy)
    {
        if (Enemy != null)
        {
            laser.SetPosition(1, Enemy.transform.position + offset / 2.5f);
            Enemy e = Enemy.GetComponent<Enemy>();
            e.TakeDamage(Damage);
        }
        else
        {
            laser.SetPosition(1, gameObject.transform.position + offset);
        }
    }

    protected override void Initialize()
    {
        Damage = 0.05f;
        AttackCooldown = 0.015f;
        Name = "Laser";
        laser = GetComponent<LineRenderer>();
        laser.SetPosition(0, gameObject.transform.position + offset);
        laser.SetPosition(1, gameObject.transform.position + offset);
    }

}
