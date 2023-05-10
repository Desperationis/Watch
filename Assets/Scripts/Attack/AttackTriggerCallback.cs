using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerCallback : MonoBehaviour
{
    [SerializeField]
    private GameObject source = null;

    [SerializeField]
    private AttackData attackData = null;

    [SerializeField]
    private Collider2D collider = null;

    [SerializeField]
    private string tagToIgnore = "";


    private void Start() 
    {
        EnableHitbox();
    }

    public void EnableHitbox() 
    {
        collider.enabled = true;
    }

    public void DisableHitbox() 
    {
        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // This check is needed so Attacker doesn't hurt themselves
        if(other.tag != tagToIgnore) {
            // Broadcast hurt message to object. It is up to the object to
            // decide what happens.
            AttackHub hub = other.GetComponent<AttackHub>();
            hub.ApplyDamage(source, attackData);
        }
    }
}
