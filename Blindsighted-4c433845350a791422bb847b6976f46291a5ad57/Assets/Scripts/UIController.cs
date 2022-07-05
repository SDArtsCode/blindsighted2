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

    public static bool playerLocked;

    private void Start()
    {
        playerLocked = false;

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
            if (respawnTimer < 0)
            {
                LevelLoader.instance.LoadLevel(0, -1);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                if (paused)
                {
                    UnpauseGame();
                    playerLocked = false;
                }
                else
                {
                    PauseGame();
                    playerLocked = true;
                }
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

    public void PlayerHit(float health)
    {
        hurtScreen.SetActive(true);
    }

    void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    void UnpauseGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        PlayerHealth.onPlayerDeath -= PlayerDeath;
        PlayerHealth.onPlayerHit -= PlayerHit;
    }
}
