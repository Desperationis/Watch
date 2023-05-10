using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hosts event delegates that allows an easy way of attacking a mob while using
/// a pub-sub system.
/// </summary>
public class AttackHub : MonoBehaviour
{
    public delegate void AttackDelegate(GameObject source, AttackData a);
    public event AttackDelegate onAttack = null; // "event" is Unity's event delegate

    public void ApplyDamage(GameObject source, AttackData a) {
        if(onAttack != null)
            onAttack.Invoke(source, a);
    }

}
