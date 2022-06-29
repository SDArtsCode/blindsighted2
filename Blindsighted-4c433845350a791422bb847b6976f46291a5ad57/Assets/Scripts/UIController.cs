using UnityEngine;

public class UIController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] GameObject hurtScreen;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject reticle;

    [SerializeField] float respawnTimer;
    bool dead;
    bool paused;

    private void Start()
    {
        PlayerHealth.onPlayerHit += PlayerHit;
        PlayerHealth.onPlayerDeath += PlayerDeath;
        
        anim = GetComponent<Animator>();

        hurtScreen.SetActive(false);
        deathScreen.SetActive(false);
        reticle.SetActive(true);
        pauseMenu.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            if (paused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }   
        }

    }

    public void PlayerDeath()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        reticle.SetActive(false);
        deathScreen.SetActive(true);
        dead = true;
    }

    public void PlayerHit()
    {
        hurtScreen.SetActive(true);
    }

    void PauseGame()
    {
        paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        PlayerMovement.locked = true;
        MouseVision.locked = true;
    }

    void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
        PlayerMovement.locked = false;
        MouseVision.locked = false;
    }

    private void OnDestroy()
    {
        PlayerHealth.onPlayerDeath -= PlayerDeath;
        PlayerHealth.onPlayerHit -= PlayerHit;
    }
}
