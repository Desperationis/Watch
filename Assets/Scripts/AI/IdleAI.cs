using UnityEngine;
using System;

/// <summary>
/// Allows a mob to idle.
/// </summary>
public class IdleAI : AIBehavior
{
    [SerializeField]
    private MobController mobController = null;

    [SerializeField]
    private float idleMoveTime = 1.0f;

    [SerializeField]
    private float idleRestTime = 1.0f;

    bool stayingStill = false;
    float switchTimer;

    public override void OnSwitchEnter()
    {
        switchTimer = Time.time + idleMoveTime;
    }

    public override void UpdateAI()
    {
        if(switchTimer < Time.time)
        {
            if(stayingStill)
            {
                mobController.SetDirection(UnityEngine.Random.insideUnitCircle);
            }
            else
            {
                mobController.SetDirection(Vector2.zero);
            }

            stayingStill = !stayingStill;
            switchTimer = Time.time + GetWaitingDelay();
        }

        mobController.UpdateFrame();
    }

    public float GetWaitingDelay()
    {
        return stayingStill ? idleRestTime : idleMoveTime;
    }

    public override bool CheckRequirement()
    {
        // Always try to idle.
        return true;
    }
}
