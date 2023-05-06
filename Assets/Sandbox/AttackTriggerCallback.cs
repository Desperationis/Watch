using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerCallback : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Object still in trigger");
        Rigidbody2D body = other.GetComponent<Rigidbody2D>();
        body.AddForce(new Vector2(10,10));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Object Left trigger");
    }
}
