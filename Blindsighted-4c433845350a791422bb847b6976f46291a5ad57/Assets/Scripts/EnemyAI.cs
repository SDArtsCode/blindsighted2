using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float meleeDamage = 50;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(PlayerMovement.playerPosition);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(meleeDamage);
        }
    }
}
