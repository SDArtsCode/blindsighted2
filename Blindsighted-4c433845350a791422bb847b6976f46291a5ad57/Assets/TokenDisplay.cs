using UnityEngine;
using TMPro;

public class TokenDisplay : MonoBehaviour
{
    [SerializeField] Settings settings;
    TextMeshProUGUI text;

    private void Awake()
    {
     
    }

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void TokenChanged()
    {
        text.text = "Tokens: " + settings.tokens;
    }
}
