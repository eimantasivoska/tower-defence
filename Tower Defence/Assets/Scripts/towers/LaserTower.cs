using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    
    protected override void AttackEnemy(GameObject Enemy)
    {
        
    }

    protected override void Initialize()
    {
        Damage = 1;
        AttackCooldown = 1;
    }
}
