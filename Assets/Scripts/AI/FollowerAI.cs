using UnityEngine;

/// <summary>
/// Allows a mob to follow the nearest player.
/// </summary>
[RequireComponent(typeof(MobController))]
public class FollowerAI : AIBehavior
{
    [SerializeField]
    private MobController mobController = null;

    public override void UpdateAI()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        mobController.SetRunning(true);
        mobController.SetDirection(player.transform.position - transform.position);
        mobController.UpdateFrame();
    }

    public override bool CheckRequirement()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 distance = player.transform.position - transform.position;

        return distance.SqrMagnitude() < 3 * 3;
    }

    public override void OnSwitchLeave()
    {
        mobController.SetRunning(false);
        mobController.SetDirection(Vector2.zero);
    }
}
