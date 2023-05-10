using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackHub))]
public class Mortality : MonoBehaviour
{
    [SerializeField]
    private Health health = null;

    private void Awake()
    {
        health.AddListener(CheckHealth);

        AttackHub hub = GetComponent<AttackHub>();
        hub.onAttack += TakeDamage;
    }

    private void TakeDamage(GameObject o, AttackData a) {
        health.TakeDamage(a.damage);
    }

    private void CheckHealth(int health)
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
