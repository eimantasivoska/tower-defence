using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    LineRenderer laser;
    public Vector3 offset;


    protected override void AttackEnemy(GameObject Enemy)
    {
        if (Enemy != null)
        {
            laser.SetPosition(1, Enemy.transform.position + offset / 2.5f);
            Enemy e = Enemy.GetComponent<Enemy>();
            if (e.TakeDamage(Damage))
            {
                Eliminations++;
                UIManager.Instance.UpdateEliminations(this);
            }
        }
        else
        {
            laser.SetPosition(1, gameObject.transform.position + offset);
        }
    }

    protected override void Initialize()
    {
        Level = 1;
        BaseDamage = 0.25f;
        Price = 200;
        UpgradePrice = Price;
        Eliminations = 0;
        Damage = BaseDamage;
        AttackCooldown = 0.03f;
        Name = "Laser";
        laser = GetComponent<LineRenderer>();
        laser.SetPosition(0, gameObject.transform.position + offset);
        laser.SetPosition(1, gameObject.transform.position + offset);
    }

}
