using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Detection, 
        Navigation,
    }

    public float damage = 50;
    [SerializeField] private float detectionRange = 2;
    public float movementSpeed = 8f;
    public float attackSpeed = 16f;
    public LayerMask layerMask;
    [HideInInspector] public NavMeshAgent agent;
    Collider col;

    [HideInInspector] public EnemyState state = EnemyState.Detection;

    public virtual void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        col = GetComponent<Collider>(); 
    }

    public virtual void Update()
    {
        switch (state)
        {
            case EnemyState.Detection:
                DetectPlayer();
                break;
            case EnemyState.Navigation:
                NavigateToPlayer();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            StartCoroutine(DisableCollider());
        }
    }

    public virtual void DetectPlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, (PlayerMovement.playerPosition - transform.position).normalized, out hit, detectionRange, layerMask))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                state = EnemyState.Navigation;
            }
        }
    }

    public virtual void NavigateToPlayer()
    {
        agent.SetDestination(PlayerMovement.playerPosition);
    }


    Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (from - to).normalized;
    }

    private void OnDrawGizmos()
    {

    }

    IEnumerator DisableCollider()
    {
        col.enabled = false;

        yield return new WaitForSeconds(2);

        col.enabled = true;
    }
}
