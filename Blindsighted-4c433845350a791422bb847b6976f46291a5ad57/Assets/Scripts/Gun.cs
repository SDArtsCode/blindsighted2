using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gun : MonoBehaviour
{

    const string audType = "MusicLowpass";
    const float AUDMAX = 22000f;
    const float AUDMIN = 1000f;
    const float AUDMOD = 1.003f;

    [SerializeField] Settings settings;
    public static Weapon currentWeapon;
    public AudioMixer masterMixer;

    [SerializeField] float randomness;
    [SerializeField] int maxAmmoInventory;
    [SerializeField] GameObject bullet;
    [SerializeField] float travelTime = 3.0f;
    [SerializeField] LayerMask layerMask;

    Transform gunOrigin;
    private Vector3 destination;
    private float nextTimeToFire = 0f;
    int ammoInGun;
    bool isReloading = false;

    ParticleSystem muzzleFlash;
    private Animator anim;

    [SerializeField] Camera fpsCam;
    [SerializeField] Animator flashAnim;
    [SerializeField] Transform hackOrigin;
    [SerializeField] float hackRadius;

    bool dead;

    private void Awake()
    {
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
    }

    private void Start()
    {
        currentWeapon = settings.weapon;

        ammoInGun = currentWeapon.magSize;

        Instantiate(currentWeapon.prefab, gameObject.transform);

        anim = GetComponent<Animator>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        gunOrigin = GameObject.FindWithTag("GunOrigin").transform;
    }

    void Update()
    {
        float value;
        masterMixer.GetFloat(audType, out value);
        if (value > AUDMIN)
        {
            masterMixer.SetFloat(audType, value / AUDMOD);
        }
        if (gunOrigin == null)
        {
            gunOrigin = GameObject.FindWithTag("GunOrigin").transform;
        }

        if (!UIController.playerLocked && !dead)
        {
            if (Input.GetButton("Fire1"))
            {
                if (nextTimeToFire > 0 && ammoInGun > 0 && !isReloading)
                {
                    nextTimeToFire = -1f / currentWeapon.fireRate;
                    Shoot();
                }
                else if (nextTimeToFire >= 0 && ammoInGun <= 0 && !isReloading)
                {
                    Reload();
                }
            }
            if (Input.GetKey(KeyCode.R) && !isReloading)
            {
                if (ammoInGun < currentWeapon.magSize)
                {
                    Reload();
                }
            }

            nextTimeToFire += Time.deltaTime;
        }
    }

    void Shoot()
    {
        ammoInGun--;
        anim.SetFloat("FiringSpeed", currentWeapon.fireRate);
        anim.SetTrigger("Fire");

        muzzleFlash.Play();
        masterMixer.SetFloat(audType, AUDMAX);
        flashAnim.SetTrigger("Flash");
        AudioManager.instance.Play(currentWeapon.soundEffect);

        /*
        Collider[] cols = Physics.OverlapSphere(hackOrigin.position, hackRadius, LayerMask.GetMask("Enemy"), QueryTriggerInteraction.Collide);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Enemy"))
            {
                Debug.Log("Enemy damaged:" + cols[i].name);
                cols[i].GetComponent<Health>().TakeDamage(currentWeapon.damage);
                return;
            }
        }
        */

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        if (currentWeapon.name == "BlunderBuss")
        {
            for (int i = 0; i < 10; i++)
            {
                var b = Instantiate(bullet, gunOrigin.transform.position, Quaternion.identity);
                b.GetComponent<Rigidbody>().velocity = (Direction(destination, gunOrigin.transform.position) + new Vector3(Random.Range(-currentWeapon.accuracy, currentWeapon.accuracy), Random.Range(-currentWeapon.accuracy, currentWeapon.accuracy), Random.Range(-currentWeapon.accuracy, currentWeapon.accuracy))) * currentWeapon.bulletSpeed;
                Destroy(b, travelTime);
            }
        }
        else
        {
            var b = Instantiate(bullet, gunOrigin.transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody>().velocity = (Direction(destination, gunOrigin.transform.position) + new Vector3(Random.Range(-currentWeapon.accuracy, currentWeapon.accuracy), Random.Range(-currentWeapon.accuracy, currentWeapon.accuracy), Random.Range(-currentWeapon.accuracy, currentWeapon.accuracy))) * currentWeapon.bulletSpeed;
            Destroy(b, travelTime);
        }
    }

    public void Reload()
    {
        isReloading = true;

        anim.SetTrigger("Reload");
        anim.speed = currentWeapon.reloadMultiplier;
        AudioManager.instance.Play("GunReload");
    }

    public void ReloadFinished()
    {
        isReloading = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        ammoInGun = currentWeapon.magSize;
    }

    Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (from - to).normalized;
    }

    void OnPlayerDeath()
    {
        dead = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hackOrigin.position, hackRadius);
    }
}
