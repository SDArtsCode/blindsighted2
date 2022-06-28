using UnityEngine;

public class UIController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] GameObject hurtScreen;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject reticle;

    [SerializeField] float respawnTimer;
    bool dead;

    private void Start()
    {
        PlayerHealth.onPlayerHit += PlayerHit;
        PlayerHealth.onPlayerDeath += PlayerDeath;
        
        anim = GetComponent<Animator>();

        hurtScreen.SetActive(false);
        deathScreen.SetActive(false);
        reticle.SetActive(true);
    }

    private void Update()
    {
        if (dead)
        {
            respawnTimer -= Time.deltaTime;
            if(respawnTimer < 0)
            {
                LevelLoader.instance.LoadLevel(0, -1);
            }
        }
    }

    public void PlayerDeath()
    {
        reticle.SetActive(false);
        deathScreen.SetActive(true);
        dead = true;
    }

    public void PlayerHit()
    {
        hurtScreen.SetActive(true);
    }

    private void OnDestroy()
    {
        PlayerHealth.onPlayerDeath -= PlayerDeath;
        PlayerHealth.onPlayerHit -= PlayerHit;
    }
}
