using UnityEngine;

public class Flare : MonoBehaviour
{
    [SerializeField] float maxRange;
    [SerializeField] float brightTime;
    [SerializeField] float fadeTime;
    [SerializeField] float decreaseSpeed;
    private Light flareLight;

    private void Start()
    {
        flareLight = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        if (brightTime < 0)
        {
            flareLight.range = Mathf.Lerp(flareLight.range, 0, decreaseSpeed / 100);

            if (fadeTime < 0)
            {
                Destroy(gameObject);
            }

            fadeTime -= Time.deltaTime;
        }

        brightTime -= Time.deltaTime;
    }
}
