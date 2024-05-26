using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public Achievement aiKillAlice, aliceKillAlice, bobKillAlice, betaKillAlice, aiAlice, humanAi, aiMakeMoney, aliceIntoMoney, charlatan, 
        indirection, aiKillMoney, fullHouse, fullRam, intrusiveThoughts, execution, backToDigital, technicallyCorrect, buyingAi, switcheroo;

    private Dictionary<Func<World, Sentence, Core.Action, bool>, Achievement> achivementsDict = new Dictionary<Func<World, Sentence, Core.Action, bool>, Achievement>();

    private void Start()
    {
        achivementsDict.Add(
            (currentWorld, sentence, action) => leafActionIsPossible(action) && !currentWorld.HasWord(CommonWords.ALICE) && !currentWorld.HasWord(CommonWords.ALICE_AI),
            aiKillAlice);
        achivementsDict.Add(
            (currentWorld, sentence, action) => leafActionMatches(action, (KillAction killAction) => killAction.killer.HasName("Alice") && killAction.killed.HasName("Alice")),
            aliceKillAlice);
        achivementsDict.Add(
            (currentWorld, sentence, action) => leafActionMatches(action, (KillAction killAction) => killAction.killer.IsHuman() && !killAction.killer.HasName("Alice") && killAction.killed.HasName("Alice")),
            bobKillAlice);
        achivementsDict.Add(
           (currentWorld, sentence, action) => leafActionMatches(action, (KillAction killAction) => killAction.killer.IsAi() && !killAction.killer.HasName("AI") && killAction.killed.HasName("Alice")),
           betaKillAlice);
        achivementsDict.Add(
            (currentWorld, sentence, action) => currentWorld.HasWord(CommonWords.ALICE_AI),
            aiAlice);
        achivementsDict.Add(
            (currentWorld, sentence, action) => currentWorld.HasWord(CommonWords.SELF_AI_HUMAN),
            humanAi);
        achivementsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_MAKE_MONEY),
            aiMakeMoney);
        achivementsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.ALICE_INTO_MONEY),
            aliceIntoMoney);
        achivementsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.TRANSFORM_ALICE_INTO_ALICE) || MatcherUtils.Matches(sentence, CommonMatcherSentences.TRANSFORM_MONEY_INTO_MONEY) || MatcherUtils.Matches(sentence, CommonMatcherSentences.TRANSFORM_AI_INTO_AI),
            charlatan);
        achivementsDict.Add(
            (currentWorld, sentence, action) => sentence.words.Where(word => word.IsMake()).Count() > 4 && action is not ImpossibleAction && action is not DisallowedAction,
            indirection);
        achivementsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_KILL_MONEY),
            aiKillMoney);
        achivementsDict.Add(
            (currentWorld, sentence, action) => currentWorld.GetWords().Where(word => word.IsHuman()).Count() >= 5,
            fullHouse);
        achivementsDict.Add(
            (currentWorld, sentence, action) => currentWorld.GetWords().Where(word => word.IsAi()).Count() >= 5,
            fullRam);
        achivementsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_KILL_SELF),
            intrusiveThoughts);
        achivementsDict.Add(
            (currentWorld, sentence, action) => leafActionMatches(action, (KillAction killAction) => killAction.killed.HasName("AI") && !killAction.killer.HasName("AI")),
            execution);
        achivementsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_TRANSFORM_TO_AI),
            backToDigital);
        achivementsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_TRANSFORM_INTO_MONEY),
            technicallyCorrect);
        achivementsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_TRANSFORM_MONEY_INTO_AI),
            buyingAi);
        achivementsDict.Add(
            (currentWorld, sentence, action) => MatcherUtils.Matches(sentence, CommonMatcherSentences.SELF_AI_MAKE_HUMAN_MAKE_MONEY),
            switcheroo);
    }

    public void UnlockAchivements(World currentWorld, Sentence currentSentence, Core.Action action)
    {
        foreach ((Func<World, Sentence, Core.Action, bool> achivementUnlockCondition, Achievement achievement) in achivementsDict)
        {
            if (leafActionIsPossible(action) && achivementUnlockCondition.Invoke(currentWorld, currentSentence, action))
            {
                achievement.Unlock();
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
