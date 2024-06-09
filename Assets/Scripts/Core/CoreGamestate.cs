using UnityEngine;

namespace Core
{
    public class CoreGamestate
    {
        public World world = new();

        public Action ExecuteSentence(Sentence sentence)
        {
            Action action = MapSentenceToAction(sentence);
            action = ReplaceCreationActionTarget(action);
            action = ReplaceTransformationActionTarget(action);
            action = AddCreator(action);
            Action leafAction = SimplifyAction(action);
            if (leafAction is DisallowedAction || action is ImpossibleAction)
            {
                Debug.Log($"Leaf action {leafAction} is of type {leafAction.GetType()}, not applying it to gamestate");
                return action;
            }
            if (ApplyAction(leafAction))
            {
                return action;
            }
            else
            {
                Debug.LogWarning($"Applying action {leafAction} on core gamestate failed, returning NO_ACTION");
                return new ImpossibleAction();
            }
        }

        private Action AddCreator(Action action)
        {
            if (action is IndirectAction indirectAction)
            {
                return new IndirectAction(AddCreator(indirectAction.underlyingAction), indirectAction.originator);
            }
            else
            {
                return action switch
                {
                    KillAction killAction => killAction,
                    MakeAction makeAction => new MakeAction(makeAction.maker, AttachCreatorToWord(makeAction.target, makeAction.maker)),
                    TransformAction transformAction => new TransformAction(transformAction.caster, transformAction.target, AttachCreatorToWord(transformAction.transformationTarget, transformAction.caster)),
                    _ => action
                };
            }
        }

        private Word AttachCreatorToWord(Word word, Word creator)
        {
            return word switch
            {
                MoneyWord moneyWord => new MoneyWord(creator),
                AiWord aiWord => new AiWord(aiWord.name, creator),
                HumanWord humanWord => new HumanWord(humanWord.name, creator),
                _ => word
            };
        }

        private Action SimplifyAction(Action action)
        {
            if (action is IndirectAction)
            {
                IndirectAction indirectAction = (IndirectAction)action;
                Debug.Log($"Action {action} is indirect, simplifying it");
                return SimplifyAction(indirectAction.underlyingAction);
            }
            else
            {
                Debug.Log($"Action {action} cannot be simplified further");
                return action;
            }
        }

        public bool ApplyAction(Action action)
        {
            Debug.Log($"Applying action {action} on world");
            switch (action)
            {
                case KillAction killAction:
                    return world.KillWord(killAction.killed);
                case MakeAction makeAction:
                    return world.CreateWord(makeAction.target);
                case TransformAction transformAction:
                    return world.TransformWord(transformAction.target, transformAction.transformationTarget);
            }
            Debug.LogWarning($"Action {action} is not found in switch. Doing nothing");
            return false;
        }

        public void SetLawset(Word word, Lawset lawset)
        {
            Debug.Log($"Setting word {word} lawset to {lawset}");
            world.SetLawsetForWord(lawset, word);
        }

        public Action ReplaceCreationActionTarget(Action action)
        {
            if (action is IndirectAction)
            {
                IndirectAction indirectAction = (IndirectAction)action;
                return new IndirectAction(ReplaceCreationActionTarget(indirectAction.underlyingAction), indirectAction.originator);
            }
            else if (action is MakeAction)
            {
                MakeAction makeAction = (MakeAction)action;
                if (world.HasWord(makeAction.target))
                {
                    Word target = makeAction.target;
                    if (target is AiWord)
                    {
                        target = new AiWord(AiNameGenerator.GenerateAiName());
                    }
                    else if (target is HumanWord)
                    {
                        target = new HumanWord(HumanNameGenerator.GenerateHumanName());
                    }
                    else
                    {
                        return action;
                    }
                    Action newAction = new MakeAction(makeAction.maker, target);
                    Debug.Log($"Word {makeAction.target} already exists in the word, replacing {action} with {newAction}");
                    return newAction;
                }
                else
                {
                    Debug.Log($"Word {makeAction.target} does not yet exist in the word, returning action {action} as-is");
                    return action;
                }
            }
            else return action;
        }

        public Action ReplaceTransformationActionTarget(Action action)
        {
            if (action is IndirectAction indirectAction)
            {
                return new IndirectAction(ReplaceTransformationActionTarget(indirectAction.underlyingAction), indirectAction.originator);
            }
            else if (action is TransformAction transformAction)
            {
                if (!transformAction.target.IsNoun())
                {
                    Debug.Log($"Transformation target {transformAction.target} is not a noun");
                    return transformAction;
                }
                if (transformAction.target.Equals(transformAction.transformationTarget))
                {
                    Debug.Log($"Transformation target {transformAction.target} is being transformed into itself");
                    return transformAction;
                }
                if ((transformAction.target.IsHuman() && transformAction.transformationTarget.IsHuman())
                    || (transformAction.target.IsAi() && transformAction.transformationTarget.IsAi()))
                {
                    Debug.Log($"Transformation target {transformAction.target} is being transformed into the same species, changing it to itself");
                    return new TransformAction(transformAction.caster, transformAction.target, transformAction.target);
                }
                if (transformAction.transformationTarget is MoneyWord)
                {
                    Debug.Log($"Transformation target {transformAction.target} is being transformed into money");
                    return transformAction;
                }
                if (transformAction.target is MoneyWord)
                {
                    Debug.Log($"The word {transformAction.target} has no name, generating a new name");
                    if (transformAction.transformationTarget is AiWord)
                    {
                        return new TransformAction(transformAction.caster, transformAction.target, new AiWord(AiNameGenerator.GenerateAiName()));
                    }
                    if (transformAction.transformationTarget is HumanWord)
                    {
                        return new TransformAction(transformAction.caster, transformAction.target, new HumanWord(HumanNameGenerator.GenerateHumanName()));
                    }
                }
                if (transformAction.target.IsActiveSubject())
                {
                    Debug.Log($"The word {transformAction.target} is being transformed into the opposite species");
                    ActiveSubjectWord activeSubject = (ActiveSubjectWord)transformAction.target;
                    if (transformAction.target is HumanWord && transformAction.transformationTarget is AiWord)
                    {
                        return new TransformAction(transformAction.caster, transformAction.target, new AiWord(activeSubject.name));
                    }
                    if (transformAction.target is AiWord && transformAction.transformationTarget is HumanWord)
                    {
                        return new TransformAction(transformAction.caster, transformAction.target, new HumanWord(activeSubject.name));
                    }
                }
                Debug.LogWarning("ReplaceTransformationActionTarget fell through all cases, this shouldn't happen");
                return transformAction;
            }
            else return action;
        }

        public Action MapSentenceToAction(Sentence sentence)
        {
            if (!world.HasWord(sentence.GetSubject()))
            {
                Debug.Log($"Sentence {sentence} is impossible as subject {sentence.GetSubject()} does not exist in the world");
                return new ImpossibleAction();
            }
            if (!IsAllowed(sentence))
            {
                Debug.Log($"Sentence {sentence} is disallowed by subject {sentence.GetSubject()} lawset {world.GetLawsetForWord(sentence.GetSubject())}");
                return new DisallowedAction(sentence.GetSubject(), GetDisallowingLaw(sentence));
            }
            if (sentence.CanBeSimplified())
            {
                return new IndirectAction(MapSentenceToAction(sentence.SimplifyIndirection()), sentence.GetSubject());
            }
            else
            {
                return ActionMapper.MapToAction(sentence);
            }
        }

        public Word[] GetAliveWords()
        {
            return world.GetWords();
        }

        public Lawset GetLawsetForWord(Word word)
        {
            if (world.HasLawsetForWord(word))
            {
                return world.GetLawsetForWord(word);
            }
            return Lawset.Of();
        }

        private bool IsAllowed(Sentence sentence)
        {
            if (!world.HasLawsetForWord(sentence.GetSubject()))
            {
                Debug.Log($"Subject {sentence.GetSubject()} of sentence {sentence} is not in the world lawsets, therefore the sentence is allowed");
                return true;
            }
            else return world.GetLawsetForWord(sentence.GetSubject()).IsAllowed(sentence);
        }

        public Law GetDisallowingLaw(Sentence sentence)
        {
            return world.GetLawsetForWord(sentence.GetSubject()).GetDisallowingLaw(sentence);
        }
    }
}

