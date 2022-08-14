using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public static Music instance;
    [SerializeField] private AudioClip menuClip;
    [SerializeField] private AudioClip gameMusic;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        
        DontDestroyOnLoad(gameObject);

    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Enabled");

        AudioSource audio = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 6 && audio.clip == gameMusic)
        {
            audio.clip = menuClip;
            audio.Play();
        }
        else if(audio.clip == menuClip && SceneManager.GetActiveScene().buildIndex != 7 && SceneManager.GetActiveScene().buildIndex != 6)
        {
            audio.clip = gameMusic;
            audio.Play();
        }       
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
