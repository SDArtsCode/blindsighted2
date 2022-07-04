using UnityEngine;

public class PlayerBullet : Bullet
{
    public override void Start()
    {
        base.Start();

        damage = Gun.currentWeapon.damage;
    }

    public override void OnTriggerEnterFixed(Collider other)
    {
        base.OnTriggerEnterFixed(other);

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Explode();
        }
    }
}
