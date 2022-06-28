using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    private int loopIndex;
    [SerializeField] Transform spawnPoint;

    [SerializeField] Transform currentPlayer;
    [SerializeField] Transform playerPrefab;
    
    [SerializeField] Transform currentEnemies;
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

        if(currentEnemies != null)
        {
            //Destroy(currentEnemies.gameObject);
            //currentEnemies = Instantiate(enemyPrefab[loopIndex], new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
