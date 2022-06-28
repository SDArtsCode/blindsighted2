using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public static int transitionIndex;

    public Animator whiteToBlack;
    public Animator blackToWhite;

    private void Awake()
    {
        instance = this;

        SetTransition(transitionIndex);
    }

    public void LoadLevel(int transitionIndex, int sceneIndex)
    {
        SetTransition(transitionIndex);
        if(sceneIndex > 0)
        {
            if (transitionIndex == 0)
            {
                StartCoroutine(WTBLevelTransition(sceneIndex));
            }
            else if (transitionIndex == 1)
            {
                StartCoroutine(BTWLevelTransition(sceneIndex));
            }
        }
        else
        {
            if (transitionIndex == 0)
            {
                StartCoroutine(WTBLevelTransition(SceneManager.GetActiveScene().buildIndex));
            }
            else if (transitionIndex == 1)
            {
                StartCoroutine(BTWLevelTransition(SceneManager.GetActiveScene().buildIndex));
            }
        }
        
    }

    public void LoadLevel(int transitionIndex)
    {
        SetTransition(transitionIndex);
        if (transitionIndex == 0)
        {
            StartCoroutine(WTBLevelTransition(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else if (transitionIndex == 1)
        {
            StartCoroutine(BTWLevelTransition(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }


    IEnumerator WTBLevelTransition(int levelIndex)
    {
        whiteToBlack.SetTrigger("Start");

        yield return new WaitForSeconds(whiteToBlack.GetCurrentAnimatorStateInfo(0).length + 0.25f);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator BTWLevelTransition(int levelIndex)
    {
        blackToWhite.SetTrigger("Start");

        yield return new WaitForSeconds(blackToWhite.GetCurrentAnimatorStateInfo(0).length + 0.25f);

        SceneManager.LoadScene(levelIndex);
    }

    private void SetTransition(int transitionIndex)
    {
        if(transitionIndex == 0)
        {
            whiteToBlack.gameObject.SetActive(true);
            blackToWhite.gameObject.SetActive(false);
        }
        else if(transitionIndex == 1)
        {
            whiteToBlack.gameObject.SetActive(false);
            blackToWhite.gameObject.SetActive(true);
        }
        LevelLoader.transitionIndex = transitionIndex;
    }
}
