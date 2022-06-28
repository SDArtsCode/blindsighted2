using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gun : MonoBehaviour
{
    const string audType = "Lowpass";
    const float AUDMAX = 22000f;
    const float AUDMIN = 1000f;
    const float AUDMOD = 1.005f;

    [SerializeField] private Weapon weapon;
    public static Weapon currentWeapon;
    public AudioMixer masterMixer;

    [SerializeField] int maxAmmoInventory;
    [SerializeField] GameObject bullet;
    [SerializeField] float travelTime = 3.0f;
    [SerializeField] LayerMask layerMask;

    Transform gunOrigin;
    private Vector3 destination;
    private float nextTimeToFire = 0f;
    int ammoInGun;
    bool isReloading = false;
    int ammoInventory;

    ParticleSystem muzzleFlash;
    private Animator anim;

    [SerializeField] Camera fpsCam;
    [SerializeField] Animator flashAnim;


    private void Start()
    {
        currentWeapon = weapon;
        ammoInventory = maxAmmoInventory;

        int ammo = Mathf.Clamp(currentWeapon.magSize, 0, ammoInventory);
        ammoInGun += ammo;
        ammoInventory -= ammo;

        Instantiate(currentWeapon.prefab, gameObject.transform);

        anim = GetComponent<Animator>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        gunOrigin = GameObject.Find("GunOrigin").transform;

    }

    void Update()
    {
        float value;
        masterMixer.GetFloat(audType, out value);
        if(value > AUDMIN){
          masterMixer.SetFloat(audType, value/AUDMOD);
        }
        if (Input.GetButton("Fire1"))
        {
            if (Time.time >= nextTimeToFire && ammoInGun > 0 && !isReloading)
            {
                nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
                Shoot();
            }
            else if(Time.time >= nextTimeToFire && ammoInGun <= 0 && !isReloading)
            {
                if(ammoInventory > 0)
                {
                    Reload();
                }
                else
                {
                    //play empty sfx
                }
            }
        }
        if (Input.GetKey(KeyCode.R) && !isReloading)
        {
            if (ammoInGun < currentWeapon.magSize && ammoInventory > 0)
            {
                Reload();
            }
        }
    }

    void Shoot()
    {
        ammoInGun--;
        anim.SetTrigger("Fire");

        muzzleFlash.Play();
        masterMixer.SetFloat(audType, AUDMAX);
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

        int reloadAmount = currentWeapon.magSize - ammoInGun;
        int reloaded = Mathf.Clamp(reloadAmount, 0, ammoInventory);
        ammoInGun += reloaded;
        ammoInventory -= reloaded;
    }

    public void AddAmmo(int amount)
    {
        ammoInventory = Mathf.Clamp(ammoInventory + amount, 0, maxAmmoInventory);
    }

    Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (from-to).normalized;
    }
}
