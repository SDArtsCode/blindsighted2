using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;
    [SerializeField] private bool isLevelSelect = false;
    [SerializeField] private AudioClip menuClip;

    private void Awake()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if(isLevelSelect){
          audio.clip = menuClip;
        }
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
}
