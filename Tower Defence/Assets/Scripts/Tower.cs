using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower
{
    protected enum AttackMode { First, Last }
    protected int Damage;
    protected float AttackCooldown;
    AttackMode Mode;
    protected List<GameObject> Enemies;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
            Enemies.Add(col.gameObject);
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
            Enemies.Remove(col.gameObject);
    }
    void SelectEnemy()
    {
        if (Enemies.Count != 0)
            switch (Mode)
            {
                case AttackMode.First:
                    AttackEnemy(Enemies[0]);
                    break;
                case AttackMode.Last:
                    AttackEnemy(Enemies[Enemies.Count - 1]);
                    break;
            }
    }
    abstract protected void AttackEnemy(GameObject Enemy);
    abstract protected IEnumerator Attack();

}


