using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Detection, 
        Navigation,
        Attacking
    }

    public float damage = 50;
    [SerializeField] private float detectionRange = 2;
    public float movementSpeed = 8;
    public float attackSpeed;
    public LayerMask layerMask;
    [HideInInspector] public NavMeshAgent agent;

    [HideInInspector] public EnemyState state = EnemyState.Detection;

    private void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();
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
            case EnemyState.Attacking:
                AttackPlayer();
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
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
        agent.speed = movementSpeed;
    }

    public virtual void AttackPlayer()
    {

    }

    Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (from - to).normalized;
    }

    private void OnDrawGizmos()
    {

    }
}
