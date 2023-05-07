using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mortality : MonoBehaviour
{
    [SerializeField]
    private Health health = null;

    private void Awake()
    {
        health.AddListener(CheckHealth);
    }

    private void CheckHealth(int health)
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
