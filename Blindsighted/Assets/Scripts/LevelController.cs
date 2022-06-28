using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    [SerializeField] Transform spawnPoint;

    [SerializeField] Transform currentPlayer;
    [SerializeField] Transform playerPrefab;
    
    [SerializeField] Transform currentEnemies;
    [SerializeField] Transform enemyPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void ResetLevel()
    {
        Destroy(currentPlayer.gameObject);
        currentPlayer = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);

        Destroy(currentEnemies.gameObject);
        currentEnemies = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
