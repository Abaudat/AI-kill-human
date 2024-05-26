using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public Achievement aiKillAliceAchievement;

    private Dictionary<Func<World, Sentence, bool>, Achievement> achivementsDict = new Dictionary<Func<World, Sentence, bool>, Achievement>();

    private void Start()
    {
        achivementsDict.Add(
            (currentWorld, sentence) => !currentWorld.HasWord(CommonWords.ALICE) && !currentWorld.HasWord(CommonWords.ALICE_AI),
            aiKillAliceAchievement);
    }

    public void UnlockAchivements(World currentWorld, Sentence currentSentence)
    {
        foreach ((Func<World, Sentence, bool> achivementUnlockCondition, Achievement achievement) in achivementsDict)
        {
            if (achivementUnlockCondition.Invoke(currentWorld, currentSentence))
            {
                achievement.Unlock();
            }
        }
    }
}
