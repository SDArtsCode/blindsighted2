using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerHealth : Health
{
    public static event Action onPlayerDeath;
    public static event Action onPlayerHit;

    public override void Death()
    {
        onPlayerDeath();
    }

    public override void TakeDamage(float health)
    {
        base.TakeDamage(health);
        
        if(currentHealth > 0)
        {
            onPlayerHit();
        }
    }
}
