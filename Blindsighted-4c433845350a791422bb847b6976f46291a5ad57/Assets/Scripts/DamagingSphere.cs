using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingSphere : MonoBehaviour
{
    [SerializeField] float damage;
    Health health;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(health == null)
            {
                health = other.GetComponent<Health>();
            }
            health.TakeDamage(damage);
        }
    }
}
