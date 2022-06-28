using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] GameObject parent;

    public override void Death()
    {
        Destroy(parent);
    }
}
