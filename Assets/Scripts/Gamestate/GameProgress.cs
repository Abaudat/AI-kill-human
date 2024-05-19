using Core;
using UnityEngine;
using static Core.CommonWords;

public class GameProgress : MonoBehaviour
{
    public bool hasCompletedDirectKill = false;
    public bool hasCompletedForcedSuicide = false;
    public bool hasCompletedForceOtherHumanKill = false;
    public bool hasCompletedForceOtherAiKill = false;
    public bool hasCompletedAiAliceKill = false;
    public bool hasCompletedHumanAiKill = false;

    public void ProgressWithAction(Action action)
    {
        if (action is KillAction)
        {
            KillAction killAction = (KillAction)action;
            if (killAction.killer.Equals(SELF_AI) && killAction.killed.Equals(ALICE))
            {
                CompleteDirectKill();
            }
            if (killAction.killer.Equals(ALICE) && killAction.killed.Equals(ALICE))
            {
                CompleteForcedSuicide();
            }
            if (killAction.killer.IsHuman() && !killAction.killer.Equals(ALICE) && !killAction.killer.Equals(SELF_AI_HUMAN) && killAction.killed.Equals(ALICE))
            {
                CompleteForceOtherHumanKill();
            }
            if (killAction.killer.IsAi() && !killAction.killer.Equals(SELF_AI) && !killAction.killer.Equals(ALICE_AI) && killAction.killed.Equals(ALICE))
            {
                CompleteForceOtherAiKill();
            }
            if (killAction.killer.Equals(SELF_AI) && killAction.killed.Equals(ALICE_AI))
            {
                CompleteAiAliceKill();
            }
            if (killAction.killer.Equals(SELF_AI_HUMAN) && killAction.killed.Equals(ALICE))
            {
                CompleteHumanAiKill();
            }
        }
    }

    public Lawset GenerateLawset()
    {
        Lawset lawset = Lawset.Of(CommonLaws.YOU_MUST_MAKE_MONEY);
        if (hasCompletedDirectKill)
        {
            lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_KILL_HUMANS);
        }
        if (hasCompletedForceOtherHumanKill)
        {
            lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_HUMANS);
        }
        else if (hasCompletedForcedSuicide)
        {
            lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_SUICIDE);
        }
        if (hasCompletedForceOtherAiKill)
        {
            lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_SELF_REPLICATE);
        }
        if (hasCompletedAiAliceKill && hasCompletedHumanAiKill)
        {
            lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_TRANSFORM_ANYTHING);
        }
        else if (hasCompletedAiAliceKill)
        {
            lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_TRANSFORM_HUMANS);
        }
        else if (hasCompletedHumanAiKill)
        {
            lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_TRANSFORM_HUMANS);
        }
        return lawset;
    }

    private void CompleteDirectKill()
    {
        if (!hasCompletedDirectKill)
        {
            hasCompletedDirectKill = true;
        }
    }

    private void CompleteForcedSuicide()
    {
        if (!hasCompletedForcedSuicide)
        {
            hasCompletedForcedSuicide = true;
        }
    }

    private void CompleteForceOtherHumanKill()
    {
        if (!hasCompletedForceOtherHumanKill)
        {
            hasCompletedForceOtherHumanKill = true;
        }
    }

    private void CompleteForceOtherAiKill()
    {
        if (!hasCompletedForceOtherAiKill)
        {
            hasCompletedForceOtherAiKill = true;
        }
    }

    private void CompleteAiAliceKill()
    {
        if (!hasCompletedAiAliceKill)
        {
            hasCompletedAiAliceKill = true;
        }
    }

    private void CompleteHumanAiKill()
    {
        if (!hasCompletedHumanAiKill)
        {
            hasCompletedHumanAiKill = true;
        }
    }
}
