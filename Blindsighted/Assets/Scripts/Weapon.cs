using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Custom/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public float bulletSpeed;
    public float reloadMultiplier;
    public int magSize;
    public string soundEffect;
    public Sprite icon;
    public GameObject prefab;
}
