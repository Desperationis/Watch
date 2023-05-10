using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackHub))]
public class Knockback : MonoBehaviour
{
    [SerializeField]
    private MobController mobController = null;

    [SerializeField]
    private Rigidbody2D rigidbody2D = null;

    public void Start() {
        AttackHub hub = GetComponent<AttackHub>();
        hub.onAttack += ApplyKnockback;
    }

    public void ApplyKnockback(GameObject source, AttackData a) {
        mobController.UnPlugFor(a.knockbackDuration);

        // Calculate knockback direction
        Vector2 direction = transform.position - source.transform.position;
        direction.Normalize();
        direction.Scale(new Vector2(a.knockbackStrength, a.knockbackStrength));
        rigidbody2D.AddForce(direction);
    }
}
