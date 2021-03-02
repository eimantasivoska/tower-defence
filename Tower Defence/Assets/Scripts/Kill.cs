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
            int damage = col.gameObject.GetComponent<Enemy>().Damage;
            Destroy(col.gameObject);
            baseObject.GetComponent<Base>().TakeDamage(damage);
        }
    }
}
