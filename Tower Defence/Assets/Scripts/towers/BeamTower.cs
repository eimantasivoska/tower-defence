using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BeamTower : Tower
{
    LineRenderer laser;
    LineRenderer Lightning;
    public Vector3 offset;

    protected override void AttackEnemy(GameObject Enemy)
    {

        if (Enemy != null)
        {
            StartCoroutine(Attack(Enemy));
        }
 
    }

    protected override void Initialize()
    {
        Level = 1;
        BaseDamage = 10f;
        Price = 400;
        UpgradePrice = Price;
        Damage = BaseDamage;
        AttackCooldown = 2f;
        Name = "Beam";
        //Lightning = GameObject.Find("lightning").GetComponent<LineRenderer>();
        laser = GetComponent<LineRenderer>();
    }
    IEnumerator Attack(GameObject Enemy)
    {
        laser.positionCount = 14;
        laser.SetPosition(1, offset + new Vector3(1.2f, 1.8f, 0));
        laser.SetPosition(2, offset + new Vector3(0, 3.8f, 0));
        laser.SetPosition(3, offset + new Vector3(-1.2f, 1.8f, 0));
        laser.SetPosition(4, offset + new Vector3(0, 0, 0));
        laser.SetPosition(5, offset + new Vector3(0, 1.8f, 1.2f));
        laser.SetPosition(6, offset + new Vector3(0, 3.8f, 0));
        laser.SetPosition(7, offset + new Vector3(0, 1.8f, -1.2f));
        laser.SetPosition(8, offset + new Vector3(0, 0, 0));
        laser.SetPosition(9, offset + new Vector3(0, 1.8f, -1.2f));
        laser.SetPosition(10, offset + new Vector3(0, 3.8f, 0));
        laser.SetPosition(11, offset + new Vector3(0, 100, 0));
        laser.SetPosition(12, Enemy.transform.position-this.transform.position+ new Vector3(0, 100, 0));
        laser.SetPosition(13, Enemy.transform.position - this.transform.position);
        yield return new WaitForSeconds(0.1f);
        laser.positionCount = 0;
        if (Enemy != null)
        {
            Enemy e = Enemy.GetComponent<Enemy>();
            e.TakeDamage(Damage);
        }
    }
}
