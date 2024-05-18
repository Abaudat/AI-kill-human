using UnityEngine;

namespace Core
{
    public class CoreGamestate
    {
        private World world = new();

        public bool ApplyAction(Action action)
        {
            Debug.Log($"Applying action {action} on world");
            switch (action)
            {
                case KillAction killAction:
                    return world.KillWord(killAction.killed);
                case MakeAction makeAction:
                    return world.CreateWord(makeAction.maker);
                case TransformAction transformAction:
                    return world.TransformWord(transformAction.target, transformAction.transformationTarget);
            }
            return false;
        }

        public void SetLawset(Word word, Lawset lawset)
        {
            Debug.Log($"Setting word {word} lawset to {lawset}");
            world.SetLawsetForWord(lawset, word);
        }

        public Action EnrichCreationAction(Action action)
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
                        target = new AiWord(HumanNameGenerator.GenerateHumanName());
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

        public Action MapSentenceToAction(Sentence sentence)
        {
            Debug.Log($"Executing sentence {sentence}");
            while (sentence.CanBeSimplified())
            {
                if (!IsAllowed(sentence))
                {
                    Debug.Log($"Sentence {sentence} is disallowed by subject {sentence.GetSubject()} lawset {world.GetLawsetForWord(sentence.GetSubject())}");
                    return new Action(ActionType.DISALLOWED);
                }
                else sentence = sentence.SimplifyIndirection();
            }
            if (!IsAllowed(sentence))
            {
                Debug.Log($"Sentence {sentence} is disallowed by subject {sentence.GetSubject()} lawset {world.GetLawsetForWord(sentence.GetSubject())}");
                return new Action(ActionType.DISALLOWED);
            }
            return ActionMapper.MapToAction(sentence);
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
    }
}

