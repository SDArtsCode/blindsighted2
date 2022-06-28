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

    [SerializeField] private float damage = 50;
    [SerializeField] private float detectionRange;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackSpeed;
    private NavMeshAgent agent;

    EnemyState state = EnemyState.Detection;

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

    public void DetectPlayer()
    {

    }

    public void NavigateToPlayer()
    {
        agent.SetDestination(PlayerMovement.playerPosition);
        agent.speed = movementSpeed;
    }

    public void AttackPlayer()
    {

    }
}
