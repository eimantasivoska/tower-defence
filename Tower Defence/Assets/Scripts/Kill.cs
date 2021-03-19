using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Kill : MonoBehaviour
{
    
    GameObject baseObject;
    void Start()
    {
        baseObject = GameObject.Find("Base");
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            float damage = col.gameObject.GetComponent<Enemy>().Damage;
            col.GetComponent<Enemy>().Die();
            baseObject.GetComponent<Base>().TakeDamage(damage);
        }
    }
}
