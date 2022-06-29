using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float currentHealth;
    public bool dead;

    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public float getHealth()
    {
        return currentHealth;
    }

    public void setHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
    }

    public virtual void TakeDamage(float health)
    {
        if (!dead)
        {
            currentHealth = Mathf.Clamp(currentHealth - health, 0, maxHealth);
            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }

    public void Heal(float health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
    }

    public virtual void Death()
    {
        dead = true;
    }
}
