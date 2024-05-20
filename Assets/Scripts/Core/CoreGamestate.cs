using UnityEngine;

namespace Core
{
    public class CoreGamestate
    {
        private World world = new();

        public Action ExecuteSentence(Sentence sentence)
        {
            Action action = MapSentenceToAction(sentence);
            action = ReplaceCreationActionTarget(action);
            action = ReplaceTransformationActionTarget(action);
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
            if (action is MakeAction)
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
            if (action is TransformAction)
            {
                TransformAction transformAction = (TransformAction)action;
                Word transformedWord = transformAction.transformationTarget;
                if (transformAction.target != transformAction.transformationTarget)
                {
                    if (transformAction.target.IsActiveSubject())
                    {
                        ActiveSubjectWord target = (ActiveSubjectWord)transformAction.target;
                        if (transformedWord is AiWord)
                        {
                            if (world.HasWord(new AiWord(target.name)))
                            {
                                Debug.Log($"The word {new AiWord(target.name)} already exists in the world, replacing it with generic AI");
                                transformedWord = new AiWord(AiNameGenerator.GenerateAiName());
                            }
                            else
                            {
                                Debug.Log($"Transforming target {target} into {new AiWord(target.name)}");
                                transformedWord = new AiWord(target.name);
                            }
                        }
                        else if (transformedWord is HumanWord)
                        {
                            if (world.HasWord(new HumanWord(target.name)))
                            {
                                Debug.Log($"The word {new HumanWord(target.name)} already exists in the world, replacing it with generic human");
                                transformedWord = new HumanWord(HumanNameGenerator.GenerateHumanName());
                            }
                            else
                            {
                                Debug.Log($"Transforming target {target} into {new HumanWord(target.name)}");
                                transformedWord = new HumanWord(target.name);
                            }
                        }
                        else
                        {
                            Debug.LogWarning($"Target {target} is an unknown active subject");
                        }
                    }
                    else
                    {
                        Debug.Log($"Transformation target {transformAction.target} is not an active subject");
                    }
                }
                else
                {
                    Debug.Log($"Transformation target {transformAction.target} is being transformed into itself");
                }
                return new TransformAction(transformAction.caster, transformAction.target, transformedWord);
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

