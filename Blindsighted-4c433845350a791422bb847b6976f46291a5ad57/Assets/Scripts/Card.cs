using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] Sprite front;
    [SerializeField] Sprite back;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public IEnumerator StartAnimation()
    {
        image.overrideSprite = front;

        yield return new WaitForSeconds(0.16666f);

        image.overrideSprite = back;

        yield return new WaitForSeconds(0.3f);

        image.overrideSprite = front;

        yield return null;
    }
}
