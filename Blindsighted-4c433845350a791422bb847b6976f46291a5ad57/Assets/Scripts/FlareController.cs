using UnityEngine;

public class FlareController : MonoBehaviour
{
    [SerializeField] GameObject flarePrefab;
    [SerializeField] Transform flareOrigin;
    [SerializeField] float flareDelay;
    private float currentTime;
    [SerializeField] float launchVelocity;
    [SerializeField] float launchAngularVelocity;
    [SerializeField] int flareInventory;
    [SerializeField] int maxFlareInventory;

    [SerializeField] Transform fpsCam;

    private bool dead;

    private void Awake()
    {
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && flareInventory > 0 && currentTime > flareDelay && !dead)
        {
            LaunchFlare();
        }

        currentTime += Time.deltaTime;
    }

    void LaunchFlare()
    {
        currentTime = 0;

        var flare = Instantiate(flarePrefab, flareOrigin.transform.position, Quaternion.identity);
        Rigidbody flareRB = flare.GetComponent<Rigidbody>();
        flareRB.velocity = fpsCam.forward * launchVelocity * 10;
        flareRB.angularVelocity = new Vector3(launchAngularVelocity, launchAngularVelocity / 3, 0);
    }

    void OnPlayerDeath()
    {
        dead = true;
    }
}
