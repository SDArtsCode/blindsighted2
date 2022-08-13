using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TokenController : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float maxSeconds;
    [SerializeField] TextMeshProUGUI text;
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
        maxSeconds = settings.loops[settings.loop].levels[settings.level].maxSeconds;
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
