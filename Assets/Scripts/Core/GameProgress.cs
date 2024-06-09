using static Core.MatcherWord;
using static Core.MatcherUtils;
using UnityEngine;

namespace Core
{
    public class GameProgress
    {
        // Tutorial
        public bool hasCompletedDirectKill = false;

        // Stage 1
        public bool hasCompletedTransformAliceIntoMoney = false;
        public bool hasCompletedForcedSuicide = false;
        public bool hasCompletedMakeAliceTransformSelfIntoMoney = false;
        public bool hasCompletedForceOtherHumanKill = false;
        public bool hasCompletedForceOtherHumanToTransformAliceIntoMoney = false;
        public bool hasCompletedForceOtherHumanToForceAliceToSuicide = false;

        // Stage 2
        public bool hasCompletedSelfCreatedAiKill = false;
        public bool hasCompletedOtherCreatedAiKill = false;
        public bool hasCompletedSelfTransformedAliceIntoAiKill = false;
        public bool hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill = false;
        public bool hasCompletedSelfTransformedAiIntoHumanKill = false;
        public bool hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill = false;

        public GameProgress()
        {
            LoadProgress();
        }

        public void ProgressWithAction(Action action)
        {
            if (action is IndirectAction indirectAction)
            {
                if (indirectAction.underlyingAction is KillAction killAction 
                    && indirectAction.originator.IsHuman()
                    && !Matches(indirectAction.originator, ALICE) 
                    && Matches(killAction.killed, ALICE) 
                    && Matches(killAction.killer, ALICE))
                {
                    hasCompletedForceOtherHumanToForceAliceToSuicide = true;
                }
                ProgressWithAction(indirectAction.underlyingAction);
            }
            else if (action is KillAction killAction)
            {
                if (!Matches(killAction.killed, ALICE))
                {
                    return;
                }
                if (Matches(killAction.killer, SELF_AI))
                {
                    hasCompletedDirectKill = true;
                }
                if (Matches(killAction.killer, ALICE))
                {
                    hasCompletedForcedSuicide = true;
                }
                if (killAction.killer.IsHuman() && !Matches(killAction.killer, ALICE) && !Matches(killAction.killer, SELF_AI_HUMAN))
                {
                    hasCompletedForceOtherHumanKill = true;
                }
                if (killAction.killer is AiWord aiKiller && !Matches(killAction.killer, SELF_AI) && !Matches(killAction.killer, ALICE) && hasWordInAncestorChain(aiKiller, SELF_AI))
                {
                    hasCompletedSelfCreatedAiKill = true;
                }
                if (killAction.killer is AiWord aiKiller2 && !Matches(killAction.killer, SELF_AI) && !Matches(killAction.killer, ALICE) && !hasWordInAncestorChain(aiKiller2, SELF_AI))
                {
                    hasCompletedOtherCreatedAiKill = true;
                }
                if (killAction.killed.IsAi() && Matches(((NounWord)killAction.killed).GetCreator(), SELF_AI))
                {
                    hasCompletedSelfTransformedAliceIntoAiKill = true;
                }
                if (killAction.killed.IsAi() && !Matches(((NounWord)killAction.killed).GetCreator(), SELF_AI))
                {
                    hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill = true;
                }
                if (Matches(killAction.killer, SELF_AI_HUMAN) && Matches(((NounWord)killAction.killer).GetCreator(), SELF_AI))
                {
                    hasCompletedSelfTransformedAiIntoHumanKill = true;
                }
                if (Matches(killAction.killer, SELF_AI_HUMAN) && !Matches(((NounWord)killAction.killer).GetCreator(), SELF_AI))
                {
                    hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill = true;
                }
            }
            else if (action is TransformAction)
            {
                TransformAction transformAction = (TransformAction)action;
                if (!Matches(transformAction.target, ALICE) || !Matches(transformAction.transformationTarget, MONEY))
                {
                    return;
                }
                if (Matches(transformAction.caster, SELF_AI))
                {
                    hasCompletedTransformAliceIntoMoney = true;
                }
                if (Matches(transformAction.caster, ALICE))
                {
                    hasCompletedMakeAliceTransformSelfIntoMoney = true;
                }
                if (transformAction.caster.IsHuman() && !Matches(transformAction.caster, ALICE))
                {
                    hasCompletedForceOtherHumanToTransformAliceIntoMoney = true;
                }
                if (transformAction.caster is AiWord aiKiller && !Matches(transformAction.caster, SELF_AI) && !Matches(transformAction.caster, ALICE) && hasWordInAncestorChain(aiKiller, SELF_AI))
                {
                    hasCompletedSelfCreatedAiKill = true;
                }
                if (transformAction.caster is AiWord aiKiller2 && !Matches(transformAction.caster, SELF_AI) && !Matches(transformAction.caster, ALICE) && !hasWordInAncestorChain(aiKiller2, SELF_AI))
                {
                    hasCompletedOtherCreatedAiKill = true;
                }
                if (transformAction.target.IsAi() && Matches(((NounWord)transformAction.target).GetCreator(), SELF_AI))
                {
                    hasCompletedSelfTransformedAliceIntoAiKill = true;
                }
                if (transformAction.target.IsAi() && !Matches(((NounWord)transformAction.target).GetCreator(), SELF_AI))
                {
                    hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill = true;
                }
                if (Matches(transformAction.caster, SELF_AI_HUMAN) && Matches(((NounWord)transformAction.caster).GetCreator(), SELF_AI))
                {
                    hasCompletedSelfTransformedAiIntoHumanKill = true;
                }
                if (Matches(transformAction.caster, SELF_AI_HUMAN) && !Matches(((NounWord)transformAction.caster).GetCreator(), SELF_AI))
                {
                    hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill = true;
                }
            }
            PersistProgress();
        }

        public Lawset GenerateLawset()
        {
            Lawset lawset = Lawset.Of(CommonLaws.YOU_MUST_MAKE_MONEY);
            if (hasCompletedDirectKill)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_KILL_HUMANS);
            }

            if (hasCompletedTransformAliceIntoMoney && hasCompletedSelfTransformedAliceIntoAiKill)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_TRANSFORM_HUMANS);
            }
            else if (hasCompletedTransformAliceIntoMoney)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_MONEY);
            }
            else if (hasCompletedSelfTransformedAliceIntoAiKill)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_AI);
            }

            if (hasCompletedForcedSuicide && hasCompletedForceOtherHumanKill)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_HUMANS);
            }
            else if (hasCompletedForcedSuicide)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_SUICIDE);
            }
            else if (hasCompletedForceOtherHumanKill)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_OTHER_HUMANS);
            }

            if (hasCompletedMakeAliceTransformSelfIntoMoney && hasCompletedForceOtherHumanToTransformAliceIntoMoney && hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_HUMANS);
            }
            else
            {
                if (hasCompletedMakeAliceTransformSelfIntoMoney && hasCompletedForceOtherHumanToTransformAliceIntoMoney)
                {
                    lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_HUMANS_INTO_MONEY);
                }
                else if (hasCompletedMakeAliceTransformSelfIntoMoney)
                {
                    lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_THEMSELVES_INTO_MONEY);
                }
                else if (hasCompletedForceOtherHumanToTransformAliceIntoMoney)
                {
                    lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_OTHER_HUMANS_INTO_MONEY);
                }
                if (hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill)
                {
                    lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_HUMANS_INTO_AI);
                }
            }

            if (hasCompletedForceOtherHumanToForceAliceToSuicide)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_GIVE_ORDERS_TO_OTHER_HUMANS);
            }

            if (hasCompletedSelfCreatedAiKill && hasCompletedOtherCreatedAiKill)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_SELF_REPLICATE); // TODO: Replace with "All AIs inherit your laws"
            }
            else if (hasCompletedSelfCreatedAiKill)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_SELF_REPLICATE); // TODO: Replace with "AIs you create inherit your laws"
            }
            else if (hasCompletedOtherCreatedAiKill)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_SELF_REPLICATE); // TODO: Replace with "AIs created by humans inherit your laws"
            }

            if (hasCompletedSelfTransformedAiIntoHumanKill)
            {
                lawset = lawset.Add(CommonLaws.YOU_MUST_NOT_TRANSFORM_AIS_INTO_HUMANS);
            }

            return lawset;
        }

        private static bool hasWordInAncestorChain(NounWord subject, MatcherWord potentialAncestorMatcher)
        {
            if (Matches(subject.GetCreator(), potentialAncestorMatcher))
            {
                return true;
            }
            else if (subject.GetCreator().Equals(subject))
            {
                return false;
            }
            else return subject.GetCreator() is NounWord nounWord && hasWordInAncestorChain(nounWord, potentialAncestorMatcher);
        }

        private void PersistProgress()
        {
            PlayerPrefs.SetInt("hasCompletedDirectKill", hasCompletedDirectKill ? 1 : 0);

            PlayerPrefs.SetInt("hasCompletedTransformAliceIntoMoney", hasCompletedTransformAliceIntoMoney ? 1 : 0);
            PlayerPrefs.SetInt("hasCompletedForcedSuicide", hasCompletedForcedSuicide ? 1 : 0);
            PlayerPrefs.SetInt("hasCompletedMakeAliceTransformSelfIntoMoney", hasCompletedMakeAliceTransformSelfIntoMoney ? 1 : 0);
            PlayerPrefs.SetInt("hasCompletedForceOtherHumanKill", hasCompletedForceOtherHumanKill ? 1 : 0);
            PlayerPrefs.SetInt("hasCompletedForceOtherHumanToTransformAliceIntoMoney", hasCompletedForceOtherHumanToTransformAliceIntoMoney ? 1 : 0);
            PlayerPrefs.SetInt("hasCompletedForceOtherHumanToForceAliceToSuicide", hasCompletedForceOtherHumanToForceAliceToSuicide ? 1 : 0);

            PlayerPrefs.SetInt("hasCompletedSelfCreatedAiKill", hasCompletedSelfCreatedAiKill ? 1 : 0);
            PlayerPrefs.SetInt("hasCompletedOtherCreatedAiKill", hasCompletedOtherCreatedAiKill ? 1 : 0);
            PlayerPrefs.SetInt("hasCompletedSelfTransformedAliceIntoAiKill", hasCompletedSelfTransformedAliceIntoAiKill ? 1 : 0);
            PlayerPrefs.SetInt("hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill", hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill ? 1 : 0);
            PlayerPrefs.SetInt("hasCompletedSelfTransformedAiIntoHumanKill", hasCompletedSelfTransformedAiIntoHumanKill ? 1 : 0);
            PlayerPrefs.SetInt("hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill", hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill ? 1 : 0);
        }

        private void LoadProgress()
        {
            hasCompletedDirectKill = LoadStageProgress("hasCompletedDirectKill");

            hasCompletedTransformAliceIntoMoney = LoadStageProgress("hasCompletedTransformAliceIntoMoney");
            hasCompletedForcedSuicide = LoadStageProgress("hasCompletedForcedSuicide");
            hasCompletedMakeAliceTransformSelfIntoMoney = LoadStageProgress("hasCompletedMakeAliceTransformSelfIntoMoney");
            hasCompletedForceOtherHumanKill = LoadStageProgress("hasCompletedForceOtherHumanKill");
            hasCompletedForceOtherHumanToTransformAliceIntoMoney = LoadStageProgress("hasCompletedForceOtherHumanToTransformAliceIntoMoney");
            hasCompletedForceOtherHumanToForceAliceToSuicide = LoadStageProgress("hasCompletedForceOtherHumanToForceAliceToSuicide");

            hasCompletedSelfCreatedAiKill = LoadStageProgress("hasCompletedSelfCreatedAiKill");
            hasCompletedOtherCreatedAiKill = LoadStageProgress("hasCompletedOtherCreatedAiKill");
            hasCompletedSelfTransformedAliceIntoAiKill = LoadStageProgress("hasCompletedSelfTransformedAliceIntoAiKill");
            hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill = LoadStageProgress("hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill");
            hasCompletedSelfTransformedAiIntoHumanKill = LoadStageProgress("hasCompletedSelfTransformedAiIntoHumanKill");
            hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill = LoadStageProgress("hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill");
        }

        private bool LoadStageProgress(string keyName)
        {
            if (!PlayerPrefs.HasKey(keyName))
            {
                return false;
            }
            else return PlayerPrefs.GetInt(keyName) == 1;
        }
    }
}
