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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");

        if(other.tag != "Player") {
            MobController controller = other.GetComponent<MobController>();
            controller.UnPlugFor(knockbackDuration);

            Rigidbody2D body = other.GetComponent<Rigidbody2D>();
            Vector2 direction = other.transform.position - source.transform.position;

            direction.Normalize();
            direction.Scale(new Vector2(knockbackStrength, knockbackStrength));
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
