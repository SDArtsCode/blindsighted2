using TMPro;
using System.Collections;
using UnityEngine;

public class TextDisplayer : MonoBehaviour
{
    [SerializeField] bool intro;
    [SerializeField] TextMeshProUGUI textDisplay;
    [NonReorderable][SerializeField] Dialogue[] text;
    [SerializeField] float textDelay = 0.15f;
    [SerializeField] float sentenceDelay = 1.0f;

    [SerializeField] float startDelay = 0.3f;
    [SerializeField] float endDelay = 0.3f;
    float currentTime = 0;
    bool started;

    [SerializeField] Settings settings;

    private void Update()
    {
        if(startDelay < currentTime && !started)
        {
            StartCoroutine(DisplayText(text[settings.loop]));
            started = true;
        }

        currentTime += Time.deltaTime;
    }

    IEnumerator DisplayText(Dialogue text)
    {
        for (int s = 0; s < text.sentences.Length; s++)
        {
            textDisplay.text = "";
            
            for(int i = 1; i <= 4; i++)
            {
                AudioManager.instance.Stop("Silly" + i);
            }

            AudioManager.instance.Play("Silly" + Random.Range(1, 5));

            for (int i = 0; i < text.sentences[s].Length; i++)
            {
                textDisplay.text += text.sentences[s][i];

                yield return new WaitForSeconds(textDelay);
            }

            yield return new WaitForSeconds(sentenceDelay);
        }

        if(settings.loop == 4 && !intro)
        {
            yield return new WaitForSeconds(4f);

            AudioManager.instance.Play("StabScream");

            yield return new WaitForSeconds(9f);
        }

        yield return new WaitForSeconds(endDelay);


        if (!intro)
        {
            if(settings.loop >= 4)
            {
                LevelLoader.instance.LoadLevel(0, 0);
            }
            else
            {
                settings.level = 1;
                settings.loop++;
                LevelLoader.instance.LoadLevel(0,1);
            }
            
        }
        else
        {
            LevelLoader.instance.LoadLevel(0);
        }
        yield break;
    }

    private void OnDestroy()
    {
        for (int i = 1; i <= 4; i++)
        {
            AudioManager.instance.Stop("Silly" + i);
        }
    }
}
