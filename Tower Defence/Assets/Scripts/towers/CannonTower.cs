using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    public GameObject BulletPrefab;
    public GameObject Barrel;
    public GameObject ShootPoint;

    protected override void Initialize()
    {
        Level = 1;
        BaseDamage = 1f;
        Price = 100;
        UpgradePrice = Price;
        Damage = BaseDamage;
        AttackCooldown = 0.5f;
        Name = "Cannon";
    }
    
    protected override void AttackEnemy(GameObject Enemy)
    {
        if (Enemy != null)
        {
            GameObject Bullet = Instantiate(BulletPrefab, ShootPoint.transform.position, ShootPoint.transform.rotation);
            Bullet bullet = Bullet.GetComponent<Bullet>();

            if (bullet != null){
                bullet.Shoot(Target, Damage);
            }
        }
    }

    void Update(){
        if (Target != null){
            Barrel.transform.LookAt(Target.transform.position);
        }
    }
}
