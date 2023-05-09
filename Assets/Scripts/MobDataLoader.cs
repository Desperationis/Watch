using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDataLoader : MonoBehaviour
{
    [SerializeField] 
    private MobData mobData = null;

    [SerializeField]
    private MobController mobController = null;

    [SerializeField]
    private Health health = null;

    // Start is called before the first frame update
    void Start()
    {
        health.SetMaxHealth(mobData.maxHealth);
        health.SetHealth(mobData.maxHealth);

        mobController.SetSpeed(mobData.speed);
        mobController.SetRunningMultiplier(mobData.runningMult);
    }
}
