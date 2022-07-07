using UnityEngine.UI;
using UnityEngine;

public class TokenController : MonoBehaviour
{
    Image image;
    [SerializeField] float maxSeconds;
    float currentSeconds;
    [SerializeField] Settings settings;

    private void Awake()
    {
        LevelExit.levelCompleted += LevelCompleted;
    }

    private void OnDestroy()
    {
        LevelExit.levelCompleted -= LevelCompleted;
    }

    private void Start()
    {
        image = GetComponent<Image>();
        currentSeconds = 0.001f;
    }

    private void Update()
    {
        currentSeconds += Time.deltaTime;

        image.fillAmount = 1 - (currentSeconds / maxSeconds);
    }

    void LevelCompleted()
    {
        settings.loops[settings.loop].levels[settings.level].playerSeconds = currentSeconds;
    }
}
