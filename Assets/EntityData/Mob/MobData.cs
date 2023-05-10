using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "EntityData/MobData", order = 1)]
public class MobData : ScriptableObject
{
    public int maxHealth;

    public float speed;

    public float runningMult;
}
