using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    MeshRenderer mr;
    Rigidbody rb;
    ParticleSystem ps;
    [SerializeField] Transform explosionPrefab;

    public float damage;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        mr = GetComponent<MeshRenderer>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Environment" || other.gameObject.tag == "Ground")
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
