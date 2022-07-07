using System.Collections;
using UnityEngine;
using TMPro;

public class DeathText : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] string[] insults;
    TextMeshProUGUI text;

    private void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        string text = insults[Random.Range(0, insults.Length - 1)];
        StartCoroutine(PrintText(text));
    }

    IEnumerator PrintText(string text)
    {
        this.text.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            this.text.text += text[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }
    
}
