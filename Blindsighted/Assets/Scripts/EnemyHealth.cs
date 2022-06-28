using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] GameObject parent;
    [SerializeField][Range(0,100)] float ammoDropChance;
    [SerializeField] GameObject ammoDrop;

    public override void Death()
    {
        if(Random.Range(0, 100) < ammoDropChance)
        {
            Instantiate(ammoDrop, parent.transform.position, Quaternion.identity);
        }

        Destroy(parent);
    }
}
