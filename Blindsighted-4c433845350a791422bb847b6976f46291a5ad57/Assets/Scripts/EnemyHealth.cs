using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] GameObject parent;
    [SerializeField] Transform soundPrefab;

    public override void Death()
    {
        Instantiate(soundPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(parent);
    }
}
