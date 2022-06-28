using UnityEngine;

public class PlayerBullet : Bullet
{
    public override void Start()
    {
        base.Start();

        damage = Gun.currentWeapon.damage;
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Explode();
        }
    }
}
