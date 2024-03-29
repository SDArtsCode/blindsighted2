using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtUI : MonoBehaviour
{
    RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();

        PlayerHealth.onPlayerHit += SetScale;
    }

    private void SetScale(float health)
    {
        float scale = Mathf.Lerp(1, 2, health / 100);

        rt.localScale = new Vector3(scale, scale, scale);
    }

    private void OnDestroy()
    {
        PlayerHealth.onPlayerHit -= SetScale;
    }
}
