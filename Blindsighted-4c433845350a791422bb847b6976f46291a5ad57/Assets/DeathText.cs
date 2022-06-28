using System.Collections;
using UnityEngine;
using TMPro;

public class DeathText : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] string[] insults;
    TextMeshProUGUI text;
    int i = 0;

    private void Start()
    {
        StartCoroutine(PrintText(insults[Random.Range(0, insults.Length)]));
    }

    IEnumerator PrintText(string text)
    {
        this.text.text += text[i];
        i++;

        yield return new WaitForSeconds(0.1f);

        if(i > text.Length - 1)
        {
            yield break;
        }
    }
    
}
