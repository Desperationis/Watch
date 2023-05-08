using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerCallback : MonoBehaviour
{
    [SerializeField]
    private Transform source = null;

    [SerializeField]
    private float knockbackStrength = 20.0f;

    [SerializeField]
    private float knockbackDuration = 0.15f;

    [SerializeField]
    private int damage = 20;

    [SerializeField]
    private Collider2D collider = null;

    [SerializeField]
    private string tagToIgnore = "";


    private void Start() 
    {
        collider.enabled = false;
    }

    public void EnableHitbox() {
        collider.enabled = true;
    }

    public void DisableHitbox() {
        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // This check is needed so Attacker doesn't hurt themselves
        if(other.tag != tagToIgnore) {
            // Turn off MobController, otherwise force will do nothing
            MobController controller = other.GetComponent<MobController>();
            controller.UnPlugFor(knockbackDuration);

            // Calculate knockback direction
            Rigidbody2D body = other.GetComponent<Rigidbody2D>();
            Vector2 direction = other.transform.position - source.transform.position;
            direction.Normalize();
            direction.Scale(new Vector2(knockbackStrength, knockbackStrength));
            body.AddForce(direction);

            // Take off health
            Health health = other.GetComponent<Health>();
            health.TakeDamage(damage);

            // Flash mob red to indicate damage
            SpriteDamageIndicator indicator = other.GetComponent<SpriteDamageIndicator>();
            indicator.FlashRed();
        }
    }
}
