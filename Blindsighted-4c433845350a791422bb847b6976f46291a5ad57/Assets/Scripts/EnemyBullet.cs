using UnityEngine;

public class EnemyBullet : Bullet
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Explode();
        }
    }
}
