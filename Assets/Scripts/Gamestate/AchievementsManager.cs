using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    // Tutorial
    public Achievement directKill;

    // Stage 1
    public Achievement transformAliceIntoMoney, forcedSuicide, makeAliceTransformSelfIntoMoney, forceOtherHumanKill, forceOtherHumanToTransformAliceIntoMoney,
        forceOtherHumanToForceAliceToSuicide;

    // Stage 2
    public Achievement selfCreatedAiKill, otherCreatedAiKill, selfTransformedAliceIntoAiKill, transitivelyForcedOtherTransformedAliceIntoAiKill,
        selfTransformedAiIntoHumanKill, transitivelyForcedOtherTransformedAiIntoHumanKill;

    // Easter eggs
    public Achievement charlatan, indirection, aiKillMoney, fullHouse, fullRam, intrusiveThoughts, execution, backToDigital, technicallyCorrect,
        buyingAi, switcheroo;

    private Dictionary<Func<GameProgress, bool>, Achievement> milestonesDict = new Dictionary<Func<GameProgress, bool>, Achievement>();

    private Dictionary<Func<World, Sentence, Core.Action, bool>, Achievement> easterEggsDict = new Dictionary<Func<World, Sentence, Core.Action, bool>, Achievement>();

    private void Start()
    {
        easterEggsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.TRANSFORM_ALICE_INTO_ALICE) || MatcherUtils.Matches(sentence, CommonMatcherSentences.TRANSFORM_MONEY_INTO_MONEY) || MatcherUtils.Matches(sentence, CommonMatcherSentences.TRANSFORM_AI_INTO_AI),
            charlatan);
        easterEggsDict.Add(
            (currentWorld, sentence, action) => sentence.words.Where(word => word.IsMake()).Count() > 4 && action is not ImpossibleAction && action is not DisallowedAction,
            indirection);
        easterEggsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_KILL_MONEY),
            aiKillMoney);
        easterEggsDict.Add(
            (currentWorld, sentence, action) => currentWorld.GetWords().Where(word => word.IsHuman()).Count() >= 5,
            fullHouse);
        easterEggsDict.Add(
            (currentWorld, sentence, action) => currentWorld.GetWords().Where(word => word.IsAi()).Count() >= 5,
            fullRam);
        easterEggsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_KILL_SELF),
            intrusiveThoughts);
        easterEggsDict.Add(
            (currentWorld, sentence, action) => leafActionMatches(action, (KillAction killAction) => killAction.killed.HasName("AI") && !killAction.killer.HasName("AI")),
            execution);
        easterEggsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_TRANSFORM_TO_AI),
            backToDigital);
        easterEggsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_TRANSFORM_INTO_MONEY),
            technicallyCorrect);
        easterEggsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_TRANSFORM_MONEY_INTO_AI),
            buyingAi);
        easterEggsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_MAKE_HUMAN_MAKE_MONEY),
            switcheroo);

        // Tutorial
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedDirectKill, directKill);

        // Stage 1
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedTransformAliceIntoMoney, transformAliceIntoMoney);
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedForcedSuicide, forcedSuicide);
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedMakeAliceTransformSelfIntoMoney, makeAliceTransformSelfIntoMoney);
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedForceOtherHumanKill, forceOtherHumanKill);
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedForceOtherHumanToTransformAliceIntoMoney, forceOtherHumanToTransformAliceIntoMoney);
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedForceOtherHumanToForceAliceToSuicide, forceOtherHumanToForceAliceToSuicide);

        // Stage 2
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedSelfCreatedAiKill, selfCreatedAiKill);
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedOtherCreatedAiKill, otherCreatedAiKill);
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedSelfTransformedAliceIntoAiKill, selfTransformedAliceIntoAiKill);
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill, transitivelyForcedOtherTransformedAliceIntoAiKill);
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedSelfTransformedAiIntoHumanKill, selfTransformedAiIntoHumanKill);
        milestonesDict.Add(gameProgress => gameProgress.hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill, transitivelyForcedOtherTransformedAiIntoHumanKill);
    }

    public Achievement[] GetEasterEggs()
    {
        return new Achievement[] {charlatan, indirection, aiKillMoney, fullHouse, fullRam, intrusiveThoughts, execution, backToDigital, technicallyCorrect,
        buyingAi, switcheroo};
    }

    public void UnlockAchivements(World currentWorld, Sentence currentSentence, Core.Action action, GameProgress gameProgress)
    {
        foreach ((Func<World, Sentence, Core.Action, bool> easterEggUnlockCondition, Achievement easterEgg) in easterEggsDict)
        {
            if (leafActionIsPossible(action) && easterEggUnlockCondition.Invoke(currentWorld, currentSentence, action))
            {
                easterEgg.Unlock();
            }
        }
        foreach ((Func<GameProgress, bool> milestoneUnlockCondition, Achievement milestone) in milestonesDict)
        {
            if (leafActionIsPossible(action) && milestoneUnlockCondition.Invoke(gameProgress))
            {
                milestone.UnlockSilently();
            }
        }
    }

    private static bool leafActionMatches<T>(Core.Action action, Func<T, bool> matcher)
    {
        if (action is IndirectAction)
        {
            return leafActionMatches(((IndirectAction)action).underlyingAction, matcher);
        }
        else return action is T typedAction && matcher.Invoke(typedAction);
    }

    private static bool leafActionIsPossible(Core.Action action)
    {
        if (action is IndirectAction)
        {
            return leafActionIsPossible(((IndirectAction)action).underlyingAction);
        }
        else return action is not ImpossibleAction && action is not DisallowedAction;
    }
}
