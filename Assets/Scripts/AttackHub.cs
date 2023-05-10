using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hosts event delegates that allows an easy way of attacking a mob while using
/// a pub-sub system.
/// </summary>
public class AttackHub : MonoBehaviour
{
    public delegate void AttackDelegate(AttackData a);
    public event AttackDelegate onAttack; // "event" is Unity's event delegate

    void ReceiveDamage(AttackData a) {
        onAttack.Invoke(a);
    }

}
