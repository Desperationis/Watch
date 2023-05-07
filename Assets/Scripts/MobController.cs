using UnityEngine;

/// <summary>
/// A custom physics controller for collisions with BoxCollider2Ds.  This is the
/// script solely responsible for mob movement and direction.
/// </summary>
public class MobController : MonoBehaviour
{
    [SerializeField]
	private Rigidbody2D rigidBody = null;

    private float unPlugDuration = 0.0f;
    public bool isPlugged { get; private set; }

    public float speed { get; private set; }
    public float runningMultiplier { get; private set; }
    public bool isRunning { get; private set; }
    public Vector2 direction { get; private set; }

    public Vector2 _cardinalDirection = Vector2.down;

    public Vector2 cardinalDirection
    {
        // Returns the cardinal direction of the mob controller as  a normalized
        // vector in only the 4 major directions. If not moving, return the last
        // known cardinal direction.
        get
        {
            if (direction != Vector2.zero)
            {
                // Get the cardinal direction of the greater x or y component
                Vector2 newCardinalDirection = direction;
                bool xComponentGreater = Mathf.Abs(direction.x) >= Mathf.Abs(direction.y);
                newCardinalDirection.x = xComponentGreater ? Mathf.Sign(direction.x) : 0;
                newCardinalDirection.y = !xComponentGreater ? Mathf.Sign(direction.y) : 0;

                _cardinalDirection = newCardinalDirection;
            }

            return _cardinalDirection;
        }
    }

    public bool isStill {
        get
        {
            return direction.sqrMagnitude == 0.0f;
        }
    }

    private void Awake()
    {
        Plug();
        SetSpeed(1);
        SetRunningMultiplier(1);
        SetRunning(false);
        Stop();
    }

    public void UnPlugFor(float duration)
    {
        isPlugged = false;
        unPlugDuration = duration;
    }

    public void Plug()
    {
        isPlugged = true;
    }

    /// <summary>
    /// Stops the gameObject from moving.
    /// </summary>
    public void Stop()
    {
        isRunning = false;
        direction = Vector2.zero;
    }

    /// <summary>
    /// Sets the direction the mob controller will drive the mob to  when called
    /// with UpdateFrame().
    /// </summary>
    /// <param name="direction">
    /// A vector that represents the direction of the mob controller. This will
    /// be normalized automatically.
    /// </param>
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }


    /// <summary>
    /// Set the speed of the MobController in Unity Units per second.
    /// </summary>
    /// <param name="speed">A speed greater than or equal to 0.</param>
    public void SetSpeed(float speed)
    {
        if(speed >= 0.0f)
        {
            this.speed = speed;
        }
    }

    public void SetRunningMultiplier(float runningMultiplier)
    {
        if (runningMultiplier >= 1.0f)
        {
            this.runningMultiplier = runningMultiplier;
        }
    }

    /// <summary>
    /// Whether or not the current speed will be multiplied by the running
    /// multiplier.
    /// </summary>
    /// <param name="running"></param>
    public void SetRunning(bool running)
    {
        isRunning = running;
    }

    /// <summary>
    /// Updates the controller by a single frame based on a delta time.
    /// </summary>
    public void UpdateFrame()
    {
        Vector2 calculatedVelocity = (Vector3)direction * speed;

        if(isPlugged) {
            rigidBody.velocity = Vector2.zero;

            if (isRunning)
            {
                calculatedVelocity *= runningMultiplier;
            }

            rigidBody.velocity = calculatedVelocity;
        }

        if(unPlugDuration <= 0) {
            Plug();
        }
        
        if(unPlugDuration > 0) {
            unPlugDuration -= Time.deltaTime;
        }

    }
}

