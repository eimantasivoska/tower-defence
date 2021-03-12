using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected enum AttackMode { First, Last }
    protected int Damage;
    protected float AttackCooldown;
    AttackMode Mode;
    protected List<GameObject> Enemies;
    abstract protected void Initialize();
    protected void Start()
    {
        Enemies = new List<GameObject>();
        Initialize();
        StartCoroutine(Attack());
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Enemies.Add(col.gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("PLS");
            Enemies.Remove(col.gameObject);
        }
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
    protected void ChangeMode(AttackMode a)
    {
        Mode = a;
    }
    abstract protected void AttackEnemy(GameObject Enemy);
    protected IEnumerator Attack()
    {
        SelectEnemy();
        yield return new WaitForSeconds(AttackCooldown);
        StartCoroutine(Attack());
    }

}


