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
    //public int health { get; private set; }
    public int health = 100;

    public int maxHealth = 100;

    private class HealthEvent : UnityEvent<int> { }

    private HealthEvent onHealthChange = new HealthEvent();

    public void TakeDamage(int amount)
    {
        SetHealth(health - amount);
    }


    /// <summary>
    /// Changes the health of this mob. This will invoke onHealthChange.
    /// </summary>
    public void SetHealth(int health)
    {
        int pastHealth = this.health;
        this.health = Mathf.Min(100, Mathf.Max(health, 0));

        if(pastHealth != this.health)
        {
            onHealthChange.Invoke(this.health);
            // 0 Listener 1
            //      100 Listener 1
            //      100 Listener 2
            //      100 Listener 3
            //      100 Listener 4
            // 0 <- Listener 2
            // 0 <- Listener 3
            // 0 <- Listener 4
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
