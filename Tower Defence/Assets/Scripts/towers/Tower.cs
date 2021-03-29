using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected enum AttackMode { First, Last }
    public float Damage { get; protected set; }
    public string Name { get; protected set; }
    protected float AttackCooldown;
    AttackMode Mode;
    protected List<GameObject> Enemies;
    protected GameObject Target;
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
            Enemies.Remove(col.gameObject);
        }
    }
    void SelectEnemy()
    {
        if (Enemies.Count != 0)
            switch (Mode)
            {
                case AttackMode.First:
                    Target=Enemies[0];
                    break;
                case AttackMode.Last:
                    Target=Enemies[Enemies.Count - 1];
                    break;
            }
        else
            Target=null;
        AttackEnemy(Target);
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
    public void SetDead(GameObject enemy)
    {
        Enemies.Remove(enemy);
    }
}


