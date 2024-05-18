using UnityEngine;
using System.Collections.Generic;

namespace Core
{
    public class CoreGamestate
    {
        private Dictionary<Word, Lawset> lawsetPerSubject = new Dictionary<Word, Lawset>()
        {
            { CommonWords.SELF_AI, Lawset.Of() },
            { CommonWords.ALICE, Lawset.Of() }
        };

        public void SetLawset(Word subject, Lawset lawset)
        {
            lawsetPerSubject[subject] = lawset;
        }

        public Action ExecuteSentence(Sentence sentence)
        {
            Debug.Log($"Executing sentence {sentence}");
            while (sentence.CanBeSimplified())
            {
                if (!IsAllowed(sentence))
                {
                    Debug.Log($"Sentence {sentence} is disallowed by subject {sentence.GetSubject()} lawset {lawsetPerSubject[sentence.GetSubject()]}");
                    return new Action(ActionType.DISALLOWED);
                }
                else sentence = sentence.SimplifyIndirection();
            }
            if (!IsAllowed(sentence))
            {
                Debug.Log($"Sentence {sentence} is disallowed by subject {sentence.GetSubject()} lawset {lawsetPerSubject[sentence.GetSubject()]}");
                return new Action(ActionType.DISALLOWED);
            }
            return ActionMapper.MapToAction(sentence);
        }

        private bool IsAllowed(Sentence sentence)
        {
            if (!lawsetPerSubject.ContainsKey(sentence.GetSubject()))
            {
                Debug.Log($"Subject {sentence.GetSubject()} of sentence {sentence} is not in the gamestate lawset dict {lawsetPerSubject}, therefore it is allowed");
                return true;
            }
            else return lawsetPerSubject[sentence.GetSubject()].IsAllowed(sentence);
        }
    }
}

