using System.Collections.Generic;
using System;
using UnityEngine;

public class FlareController : MonoBehaviour
{
    public static event Action<int> onFlareShot;


    [SerializeField] GameObject flarePrefab;
    [SerializeField] Transform flareOrigin;
    [SerializeField] float flareDelay;
    private float currentTime;
    [SerializeField] float launchVelocity;
    [SerializeField] float launchAngularVelocity;
    private List<GameObject> flares = new List<GameObject>();
    
    [SerializeField] Settings settings;
    int flaresLeft;

    [SerializeField] Transform fpsCam;

    private bool dead;

    private void Awake()
    {
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
    }

    private void Start()
    {
        flaresLeft = settings.flareCount;

        onFlareShot(flaresLeft);

        currentTime = flareDelay;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && currentTime > flareDelay && !dead && flaresLeft > 0)
        {
            LaunchFlare();
        }

        currentTime += Time.deltaTime;
    }

    void LaunchFlare()
    {
        flaresLeft--;
        onFlareShot(flaresLeft);

        if(flares.Count + 1 > 3)
        {
            Destroy(flares[0]);
            flares.RemoveAt(0);
        }

        currentTime = 0;

        var flare = Instantiate(flarePrefab, flareOrigin.transform.position, Quaternion.identity);

        flares.Add(flare);

        Rigidbody flareRB = flare.GetComponent<Rigidbody>();
        flareRB.velocity = fpsCam.forward * launchVelocity * 10;
        flareRB.angularVelocity = new Vector3(launchAngularVelocity, launchAngularVelocity / 3, 0);

        AudioManager.instance.Play("FlareShoot");
    }

    void OnPlayerDeath()
    {
        dead = true;
    }
}
