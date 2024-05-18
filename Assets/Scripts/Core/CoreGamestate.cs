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

