using UnityEngine;

public class PlayerBullet : Bullet
{
    private void Awake()
    {
        damage = Gun.currentWeapon.damage;

        Collider[] cols = Physics.OverlapSphere(transform.position, 0.5f, ~LayerMask.GetMask("Bullet"), QueryTriggerInteraction.Collide);
        foreach (Collider c in cols)
        {
            if (c.CompareTag("Enemy"))
            {
                c.GetComponent<Health>().TakeDamage(damage);
                Explode();
            }
        }
    }

    public override void Start()
    {
        base.Start();
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
