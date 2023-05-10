using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerCallback : MonoBehaviour
{
    [SerializeField]
    private Transform source = null;

    [SerializeField]
    private AttackData attackData = null;

    [SerializeField]
    private int damage = 1;

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
            // Broadcast hurt message to object. It is up to the object to
            // decide what happens.
            AttackHub hub = other.GetComponent<AttackHub>();
            hub.ApplyDamage(source.gameObject, attackData);
        }
    }
}
