using UnityEngine;

public class EnemyBullet : Bullet
{
    public override void OnTriggerEnterFixed(Collider other)
    {
        base.OnTriggerEnterFixed(other);

        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Explode();
        }
    }
}
