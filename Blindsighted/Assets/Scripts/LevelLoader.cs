using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public void LoadNextLevel(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadNextLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
