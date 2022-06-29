using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    [SerializeField] Settings settings;

    private int loopIndex;
    [SerializeField] Transform spawnPoint;

    [SerializeField] Transform currentPlayer;
    [SerializeField] Transform playerPrefab;
    
    [SerializeField] Transform[] enemyPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetLevel();
    }

    public void ResetLevel()
    {
        Destroy(currentPlayer.gameObject);
        currentPlayer = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);

        for(int i = 0; i < enemyPrefab.Length;  i++)
        {
            enemyPrefab[i].gameObject.SetActive(i == settings.loop);
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
