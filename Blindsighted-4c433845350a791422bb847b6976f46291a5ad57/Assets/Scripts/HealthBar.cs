using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private float health;
    private float lerpTimer;

    private float maxHealth;
    public float chipSpeed = 5;

    public Image frontHealthBar;
    public Image backHealthBar;
    private Health healthScript;

    private void Awake()
    {
        PlayerHealth.onPlayerHit += OnPlayerHit;
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
    }

    void Start()
    {
        maxHealth = 100;
        health = maxHealth;

    }

    void Update()
    {
        UpdateHealthUI();
    }

    private void OnPlayerHit(float health)
    {
        this.health = health;
    }

    private void OnPlayerDeath()
    {
        this.health = 0;
    }

    public void UpdateHealthUI()
    {
        maxHealth = 100;
        health = Mathf.Clamp(health, 0, maxHealth);
        lerpTimer = 0;
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            //percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);

            if (backHealthBar.fillAmount - frontHealthBar.fillAmount <= 0.001f)
            {
                backHealthBar.fillAmount = backHealthBar.fillAmount;
            }
        }
        if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            //percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    private void OnDestroy()
    {
        PlayerHealth.onPlayerHit -= OnPlayerHit;
    }
}
