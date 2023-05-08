using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    // Current angle of object
    private float angle = 0;

    // Whether to reverse "velocity"
    private bool reverse = false;

    // Amount of swings passed
    private int swings = 99;

    // Whether fire button was pressed; Used to register single clicks
    private bool pressed = false;

    // Position of player when mouse was pressed
    private Vector3 cachedPosition = Vector3.zero; 

    // Angle between mouse and player when mouse was pressed
    private float cachedMouseAngle = 0.0f;

    // Amount of degrees to change angle by every second
    [SerializeField]
    private float anglePerSec = 5;

    // Radius of swing relative to player
    [SerializeField]
    private float radius = 3f;

    // Max amount of swings before disappearing
    [SerializeField]
    private int maxSwings = 2;


    // + and - minus angle relative to offset to swing by
    [SerializeField]
    private float maxAngle = 30.0f;

    [SerializeField]
    private Transform origin = null;

    [SerializeField]
    private TrailRenderer trail = null;

    [SerializeField]
    private AttackTriggerCallback callback = null;

    void UpdatePosition() {
        Vector3 offset = new Vector2();
        offset.x = radius * Mathf.Cos((angle + cachedMouseAngle) * Mathf.Deg2Rad);
        offset.y = radius * Mathf.Sin((angle +cachedMouseAngle) * Mathf.Deg2Rad);
        offset.z = 0;

        transform.position = cachedPosition + offset;
    }

    void FixedUpdate()
    {
        /**
        Moves in perfect circle
        offset.x = radius * Mathf.Cos(angle * Time.deltaTime);
        offset.y = radius * Mathf.Sin(angle * Time.deltaTime);
        offset.z = 0;
        angle += anglePerSec;*/


        if(Input.GetButton("Fire1") && !pressed) {
            callback.EnableHitbox();
            swings = 0;

            // Swing will pivot from this point
            cachedPosition = origin.position;

            Camera cam = Camera.main;
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, cam.nearClipPlane));

            cachedMouseAngle = Mathf.Atan2(mouseWorldPos.y - cachedPosition.y, mouseWorldPos.x - cachedPosition.x) * Mathf.Rad2Deg;

            // Reposition object so no smearing occurs
            UpdatePosition();
            trail.Clear();
            trail.emitting = true;
        }
        pressed = Input.GetButton("Fire1");

        if(swings < maxSwings) {
            if(Mathf.Abs(angle) > maxAngle) {
                if(angle > 0)
                    reverse = true;
                else
                    reverse = false;
                
                swings++;
            }

            if(!reverse)
                angle += anglePerSec * Time.deltaTime; 
            else
                angle -= anglePerSec * Time.deltaTime;

            
            UpdatePosition();
        }
        else {
            trail.emitting = false;
            callback.DisableHitbox();
        }


    }
}
