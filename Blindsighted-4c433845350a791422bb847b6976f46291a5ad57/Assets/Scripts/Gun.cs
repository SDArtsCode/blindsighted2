using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Settings settings;
    public static Weapon currentWeapon;

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
        if(gunOrigin == null)
        {
            gunOrigin = GameObject.FindWithTag("GunOrigin").transform;
        }

        if (!dead)
        {
            if (Input.GetButton("Fire1"))
            {
                if (Time.time >= nextTimeToFire && ammoInGun > 0 && !isReloading)
                {
                    nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
                    Shoot();
                }
                else if (Time.time >= nextTimeToFire && ammoInGun <= 0 && !isReloading)
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
        }
    }

    void Shoot()
    {
        ammoInGun--;
        anim.SetTrigger("Fire");

        muzzleFlash.Play();
        flashAnim.SetTrigger("Flash");
        AudioManager.instance.Play(currentWeapon.soundEffect);

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        var b = Instantiate(bullet, gunOrigin.transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().velocity = Direction(destination, gunOrigin.transform.position) * currentWeapon.bulletSpeed;
        Destroy(b, travelTime);

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

        ammoInGun = currentWeapon.magSize;
    }

    Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (from-to).normalized;
    }

    void OnPlayerDeath()
    {
        dead = true;
    }
}
