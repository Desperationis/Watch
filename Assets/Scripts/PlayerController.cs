using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	Code to move an entity using keyboard controls.
*/
public class PlayerController : MonoBehaviour
{
    [SerializeField]
	private MobController mobController = null;

	[SerializeField]
	private float walkingSpeed = 4.0f;

    void Update()
    {
		mobController.Stop();

		Vector2 direction = new Vector2(0, 0);
		if( Input.GetKey(KeyCode.W) )
			direction.y = 1;
		else if( Input.GetKey(KeyCode.S) )
			direction.y = -1;

		if( Input.GetKey(KeyCode.D) )
			direction.x = 1;
		else if( Input.GetKey(KeyCode.A) )
			direction.x = -1;

		mobController.SetDirection(direction);
		mobController.SetSpeed(walkingSpeed);

		mobController.UpdateFrame();
    }
}
