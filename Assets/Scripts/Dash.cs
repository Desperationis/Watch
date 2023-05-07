using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    [SerializeField]
    private float dashLength = 1.0f;
    [SerializeField]
    private float dashDuration = 0.05f;
    [SerializeField]
    private float dashCooldown = 0.0f;
    [SerializeField]
    private bool isOnCooldown = false;
    [SerializeField]
    private Rigidbody2D rigidBody2D = null;
    [SerializeField]
    private MobController mobController = null;

    // Update is called once per frame
    void Update()
    {
        if(isOnCooldown)
        {
            dashCooldown -= 0.5f * Time.deltaTime;
        }

        if(dashCooldown <= 0)
        {
            isOnCooldown = false;
        }

        if(Input.GetButtonDown("Fire2") && !isOnCooldown)
        {
            mobController.UnPlugFor(dashDuration);
            Vector2 direction = rigidBody2D.velocity.normalized;
            direction.Scale(new Vector2(dashLength, dashLength));
            rigidBody2D.AddForce(direction, ForceMode2D.Impulse);
            isOnCooldown = true;
            dashCooldown = 1.0f;
        }   
    }
}
