using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHealth;
    public float MaxHealth => maxHealth;
    [SerializeField]
    private float currentHealth;
    public float CurrentHealth => currentHealth;

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy();
        }
    }

    public virtual void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
