using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerCallback : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");

        if(other.tag != "Player") {
            Rigidbody2D body = other.GetComponent<Rigidbody2D>();
            Vector2 direction = Random.insideUnitSphere;

            direction.Normalize();
            direction.Scale(new Vector2(10.0f, 10.0f));
            body.AddForce(direction);

            SpriteDamageIndicator indicator = other.GetComponent<SpriteDamageIndicator>();
            indicator.FlashRed();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Object still in trigger");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Object Left trigger");
    }
}
