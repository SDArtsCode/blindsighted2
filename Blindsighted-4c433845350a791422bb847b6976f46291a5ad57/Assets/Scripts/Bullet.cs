using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    MeshRenderer mr;
    Rigidbody rb;
    ParticleSystem ps;

    public float damage;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        mr = GetComponent<MeshRenderer>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        
    }

    public void Explode()
    {
        mr.enabled = false;
        damage = 0;
        rb.velocity = Vector3.zero;
        ps.Play();
    }
}
