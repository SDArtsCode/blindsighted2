using UnityEngine.Audio;
using System;
using UnityEngine;

public class PlayerHealth : Health
{
    const string audType = "MasterLowpass";
    const float AUDMAX = 5500f;
    const float AUDMIN = 500f;

    public static event Action onPlayerDeath;
    public static event Action<float> onPlayerHit;
    [SerializeField] AudioMixer am;

    public override void Start()
    {
        base.Start();
    }

    public override void Death()
    {
        base.Death();

        AudioManager.instance.Stop("Whispers");
        AudioManager.instance.Play("GameOver");
        onPlayerDeath();
    }

    public override void TakeDamage(float health)
    {
        if (!dead)
        {
            base.TakeDamage(health);

            if (currentHealth > 0)
            {
                onPlayerHit(currentHealth);
                AudioManager.instance.Play("PlayerHit");
                AudioManager.instance.Play("Whispers");

                AudioManager.instance.GetSource("Whispers").volume = Mathf.Lerp(1f, 0f, currentHealth / maxHealth);
                am.SetFloat(audType, Mathf.Lerp(AUDMIN, AUDMAX, currentHealth/maxHealth));
            }
        }
    }

    private void OnDestroy()
    {
        am.SetFloat(audType, AUDMAX);
    }
}
