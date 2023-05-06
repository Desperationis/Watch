using UnityEngine;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In charge of swapping between the different behaviors of a mob.
/// </summary>
[System.Serializable]
[DisallowMultipleComponent]
public class AISwapper : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Values closer to index 0 have a higher priority.")]
    private List<AIBehavior> AIs = new List<AIBehavior>();

    [ReadOnly] 
    [SerializeField]
    private AIBehavior currentAI = null;

    public void Start()
    {
        if (AIs.Count != 0)
        {
            foreach (AIBehavior AI in AIs)
            {
                AI.Init();
            }
            SwapCurrentAI(GetCurrentAI());
        }
    }

    void FixedUpdate()
    {
        AIBehavior newAi = GetCurrentAI();
        if (newAi != currentAI)
        {
            SwapCurrentAI(newAi);
        }

        if(currentAI != null)
        {
            currentAI.UpdateAI();
        }
    }

    public AIBehavior GetCurrentAI()
    {
        // Check for new AI's based on importance.
        foreach (AIBehavior AI in AIs)
        {
            if (AI.CheckRequirement())
            {
                if (currentAI == null) return AI;

                else if (currentAI != AI)
                {
                    return AI;
                }

                break;
            }
        }

        return currentAI;
    }

    public void SwapCurrentAI(AIBehavior AI)
    {
        if (AI == null)
        {
            Debug.LogWarning("AISwapper.cs: Swapping out the current AI is not possible with a null value.");
        }
        else
        {
            if(currentAI != null)
            {
                currentAI.OnSwitchLeave();
            }
            currentAI = AI;
            currentAI.OnSwitchEnter();
        }
    }
}
