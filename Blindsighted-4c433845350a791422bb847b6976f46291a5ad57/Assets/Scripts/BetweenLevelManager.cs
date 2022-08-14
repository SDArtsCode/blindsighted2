using TMPro;
using UnityEngine;

public class BetweenLevelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI tokensText;
    [SerializeField] Settings settings;

    
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        levelText.text = (settings.loop+1) + " - " + (settings.level+1);
        timeText.text = ConvertToTime(settings.loops[settings.loop].levels[settings.level].playerSeconds);
        tokensText.text = "+" + GetTokens(settings.loops[settings.loop].levels[settings.level].playerSeconds) + " (" + settings.tokens +  ")";
    }

    int GetTokens(float totalSeconds)
    {
        for(int i = 0; i < settings.loops[settings.loop].levels[settings.level].thresholds.Length; i++)
        {
            if(totalSeconds >= settings.loops[settings.loop].levels[settings.level].thresholds[i])
            {
                settings.tokens += i;
                return i;
            }
        }
        return 0;
    }

    string ConvertToTime(float totalSeconds)
    {
        int minutes = (int) totalSeconds / 60;
        int seconds = (int) totalSeconds % 60;
         
        if (seconds == 0)
        {
            return (minutes + ":00");
        }
        else if (seconds < 10)
        {
            return (minutes + ":0 " + seconds);
        }
        else
        {
            return (minutes + ":" + seconds);
        }
    }

    public void NextLevel()
    {
        settings.level++;
        AudioManager.instance.Play("MenuAffirmative");
        LevelLoader.instance.LoadLevel(0, settings.level + 2);
    }

    public void ChangeWeapon()
    {
        settings.level++;
        AudioManager.instance.Play("MenuAffirmative");
        LevelLoader.instance.LoadLevel(0, 6);
    }
}
