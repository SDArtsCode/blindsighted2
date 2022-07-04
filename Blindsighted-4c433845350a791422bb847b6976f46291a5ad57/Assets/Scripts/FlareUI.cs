using UnityEngine;
using TMPro;

public class FlareUI : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        FlareController.onFlareShot += OnFlareChanged;
    }

    private void OnDestroy()
    {
        FlareController.onFlareShot -= OnFlareChanged;
    }

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void OnFlareChanged(int flareCount)
    {
        if(text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        text.text = "" + flareCount;
    }
}
