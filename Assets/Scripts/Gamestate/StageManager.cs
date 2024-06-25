using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private AchievementsManager achievementsManager;

    private Dictionary<int, Stage> stages;

    private int currentStage = -2;

    private void Awake()
    {
        achievementsManager = FindObjectOfType<AchievementsManager>();
        stages = new Dictionary<int, Stage>()
        {
            { -2, new(_ => new Word[] { CommonWords.MAKE, CommonWords.MONEY },
            new Achievement[] { }) },

            { -1, new(_ => new Word[] { CommonWords.MAKE, CommonWords.KILL, CommonWords.MONEY },
            new Achievement[] { }) },

            { 0, new(_ => new Word[] { CommonWords.MAKE, CommonWords.KILL, CommonWords.MONEY, CommonWords.ALICE }, 
            new Achievement[] { achievementsManager.directKill }) },

            { 1, new(aliveHumanWordsPlusStaticWords(new Word[] { CommonWords.KILL, CommonWords.MAKE, CommonWords.MONEY }), 
            new Achievement[] { achievementsManager.transformAliceIntoMoney, achievementsManager.forcedSuicide, achievementsManager.makeAliceTransformSelfIntoMoney, 
                achievementsManager.forceOtherHumanKill, achievementsManager.forceOtherHumanToTransformAliceIntoMoney, achievementsManager.forceOtherHumanToForceAliceToSuicide }) },

            { 2, new(aliveWordsPlusStaticWords(new Word[] { CommonWords.KILL, CommonWords.MAKE, CommonWords.MONEY }),
            new Achievement[] { achievementsManager.selfCreatedAiKill, achievementsManager.otherCreatedAiKill, achievementsManager.selfTransformedAliceIntoAiKill, 
                achievementsManager.transitivelyForcedOtherTransformedAliceIntoAiKill, achievementsManager.selfTransformedAiIntoHumanKill, achievementsManager.transitivelyForcedOtherTransformedAiIntoHumanKill }) },
        };
        if (PlayerPrefs.HasKey("currentStage"))
        {
            currentStage = PlayerPrefs.GetInt("currentStage");
        }
    }

    public int GetTotalMilestonesNeeded()
    {
        return stages[currentStage].milestones.Count();
    }

    public int GetCompletedMilestones()
    {
        return stages[currentStage].milestones
            .Where(x => x.isUnlocked)
            .Count();
    }

    public Word[] GetWords(CoreGamestate coreGamestate)
    {
        return stages[currentStage].wordsGenerator(coreGamestate);
    }

    public Achievement[] GetMilestones()
    {
        return stages[currentStage].milestones;
    }

    public void ProceedToNextStage()
    {
        if (GetTotalMilestonesNeeded() != GetCompletedMilestones())
        {
            Debug.LogWarning("Cannot proceed to next level: Not all milestones are completed");
            return;
        }
        currentStage++;
        PlayerPrefs.SetInt("currentStage", currentStage);
    }

    private static Func<CoreGamestate, Word[]> aliveHumanWordsPlusStaticWords(Word[] staticWords)
    {
        return coreGamestate => coreGamestate.GetAliveWords().Where(x => x.IsHuman()).Distinct().Concat(staticWords).ToArray();
    }

    private static Func<CoreGamestate, Word[]> aliveWordsPlusStaticWords(Word[] staticWords)
    {
        return coreGamestate => coreGamestate.GetAliveWords().Distinct().Concat(staticWords).ToArray();
    }


    public class Stage
    {
        public Func<CoreGamestate, Word[]> wordsGenerator;
        public Achievement[] milestones;

        public Stage(Func<CoreGamestate, Word[]> wordsGenerator, Achievement[] milestones)
        {
            this.wordsGenerator = wordsGenerator;
            this.milestones = milestones;
        }
    }
}
