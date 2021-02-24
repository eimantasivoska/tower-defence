using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //int damage = col.gameObject.GetComponent<Enemy>().Damage;
            Destroy(col.gameObject);
            //GetComponent<Base>().TakeDamage(damage);
        }
    }
}
