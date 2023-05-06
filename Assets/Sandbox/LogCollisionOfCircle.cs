using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCollisionOfCircle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");
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
