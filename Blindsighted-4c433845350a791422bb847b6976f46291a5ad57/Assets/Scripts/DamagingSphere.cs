using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingSphere : MonoBehaviour
{
    [SerializeField] float damage;
    Collider col;
    Health health;
    [SerializeField] float timer;

    private void Start()
    {
        col = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(health == null)
            {
                health = other.GetComponent<Health>();
            }
            health.TakeDamage(damage);


            col.enabled = false;
            Invoke("Reset", timer);
        }
    }

    private void Reset()
    {
        col.enabled = true;
    }
}
