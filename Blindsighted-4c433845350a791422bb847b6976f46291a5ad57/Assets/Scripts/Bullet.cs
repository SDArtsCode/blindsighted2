using UnityEngine;

public class Bullet : MonoBehaviour
{
    MeshRenderer mr;
    Rigidbody rb;
    ParticleSystem ps;

    float damage;

    private void Start()
    {
        damage = Gun.currentWeapon.damage;

        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        mr = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Explode();
        }
    }

    void Explode()
    {
        mr.enabled = false;
        damage = 0;
        rb.velocity = Vector3.zero;
        ps.Play();
    }
}
