using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Component that controls the health of an entity locally.
/// </summary>
public class Health : MonoBehaviour
{
    // Change to this one later on when health is determined
    // By mob rule files
    public int health { get; private set; }
    public int maxHealth { get; private set; }

    private class HealthEvent : UnityEvent<int> { }

    private HealthEvent onHealthChange = new HealthEvent();
    private HealthEvent onMaxHealthChange = new HealthEvent();

    public void TakeDamage(int amount)
    {
        SetHealth(health - amount);
    }

    public void SetMaxHealth(int maxHealth) 
    {
        int pastMaxHealth = this.maxHealth;
        this.maxHealth = Mathf.Max(maxHealth, 0);

        if(pastMaxHealth != this.maxHealth)
        {
            onMaxHealthChange.Invoke(this.maxHealth);
        }
    }


    /// <summary>
    /// Changes the health of this mob. This will invoke onHealthChange.
    /// </summary>
    public void SetHealth(int health)
    {
        int pastHealth = this.health;
        this.health = Mathf.Min(maxHealth, Mathf.Max(health, 0));

        if(pastHealth != this.health)
        {
            onHealthChange.Invoke(this.health);
        }
    }

    /// <summary>
    /// Adds a listener to onHealthChange.
    /// </summary>
    public void AddListener(UnityAction<int> call)
    {
        onHealthChange.AddListener(call);
    }
}
