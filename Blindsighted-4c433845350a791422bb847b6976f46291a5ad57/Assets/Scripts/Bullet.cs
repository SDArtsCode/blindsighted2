using UnityEngine;
using System.Collections;

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

        StartCoroutine(Predict());
    }

    private void FixedUpdate()
    {
        StartCoroutine(Predict());
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    IEnumerator Predict()
    {
        Vector3 prediction = transform.position + rb.velocity * Time.fixedDeltaTime;

        RaycastHit hit2;
        int layerMask = ~LayerMask.GetMask("Bullet");

        if (Physics.Linecast(transform.position, prediction, out hit2, layerMask))
        {
            transform.position = hit2.point;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rb.isKinematic = true;
            yield return 0;
            OnTriggerEnterFixed(hit2.collider);
        }
    }

    public virtual void OnTriggerEnterFixed(Collider other)
    {
        if(other.CompareTag("Environment") || other.CompareTag("Ground"))
        {
            Explode();
        }
    }
}
