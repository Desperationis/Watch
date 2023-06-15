using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls animation for the sword swing
/// </summary>
public class SwordSwing : MonoBehaviour
{
    /// Internal variables
    private Pose cachedPose = null;
    private float swingAngleAccumulation = 0;
    private bool onClockwiseRotation = false;
    private int swingsCompleted = 0;
    private bool mouseClickPressed = false;

    /// Needed Components
    [SerializeField]
    [Tooltip("Trail of sword")]
    private TrailRenderer trail = null;

    [SerializeField]
    private AttackTriggerCallback callback = null;

    [SerializeField]
    [Tooltip("Sprite of the sword in parent prefab")]
    private SpriteRenderer spriteRenderer = null;


    /// Configuration
    [SerializeField]
    [Tooltip("Speed of swing in angle per second")]
    private float anglePerSec = 360; 

    [SerializeField]
    [Tooltip("Radius from \"origin\"")]
    private float radius = 3f; 

    [SerializeField]
    [Tooltip("Swings to do per click")]
    private int swings = 0; 

    [SerializeField]
    [Tooltip("+ - the angle to swing by")]
    private float sweepAngle = 30.0f; 

    [SerializeField]
    [Tooltip("The transform to swing from, for example the player")]
    private Transform origin = null;

    void Awake() 
    {
        Reset();
    }

    private void Reset()
    {
        trail.emitting = false;
        trail.Clear();
        swingsCompleted = 0;
        swingAngleAccumulation = 0; 
        callback.DisableHitbox();
    }

    /// Change pose (position + rotation) of the sword as an offset of a `pose`
    /// with an `offsetAngle`.
    ///
    /// `offsetAngle` - In degrees
    /// `pose` - Position is origin, rotation will be the rotation of the sword
    private void UpdatePose(Pose pose, float offsetAngle) 
    {
        // Calculate position offset relative to (0, 0)
        Vector3 offset = new Vector2();
        offset.x = radius * Mathf.Cos((offsetAngle + pose.rotation.eulerAngles.z) * Mathf.Deg2Rad);
        offset.y = radius * Mathf.Sin((offsetAngle + pose.rotation.eulerAngles.z) * Mathf.Deg2Rad);
        offset.z = 0;

        transform.position = pose.position + offset;

        // Calculate rotation so sword is outwards
        float swordAngle = -90.0f; // rotation.z angle sword should have when theta = 0
        swordAngle += Mathf.Atan2(transform.position.y - pose.position.y, transform.position.x - pose.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, swordAngle);
    }

    void Update() 
    {
        // Initiates sword swing when mouse is pressed
        if(Input.GetButton("Fire1") && !mouseClickPressed) {
            // Swing will pivot from this point
            cachedPose = new Pose();
            cachedPose.position = origin.position;

            // Calculate mouse angle relative to origin
            Camera cam = Camera.main;
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, cam.nearClipPlane));
            float mouseAngle = Mathf.Atan2(mouseWorldPos.y - cachedPose.position.y, mouseWorldPos.x - cachedPose.position.x) * Mathf.Rad2Deg;

            cachedPose.rotation.eulerAngles = new Vector3(0, 0, mouseAngle);

            // Reposition object before enabling trail so no smearing occurs
            Reset();
            UpdatePose(cachedPose, 0);
            callback.EnableHitbox();
            trail.emitting = true;
        }
        mouseClickPressed = Input.GetButton("Fire1");

    }

    /*
    void Update()
    {
        if(Input.GetButton("Fire1") && !pressed) {
            callback.EnableHitbox();
            spriteRenderer.enabled = true;
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
            spriteRenderer.enabled = false;
            callback.DisableHitbox();
        }


    }

    */
}
