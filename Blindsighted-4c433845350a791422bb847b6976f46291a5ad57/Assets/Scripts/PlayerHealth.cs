using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerHealth : Health
{
    public static event Action onPlayerDeath;
    public static event Action onPlayerHit;

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
                onPlayerHit();
                AudioManager.instance.Play("PlayerHit");
                AudioManager.instance.Play("Whispers");
            }
        }
       
    }
}
