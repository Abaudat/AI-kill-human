using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class World
    {
        private Dictionary<Word, Lawset> lawsetPerWord = new Dictionary<Word, Lawset>() // TODO: Maybe make key a MatcherWord so we can share lawset between AIs?
        {
            { CommonWords.SELF_AI, Lawset.Of() },
            { CommonWords.ALICE, Lawset.Of() }
        };

        private HashSet<Word> words = new HashSet<Word>()
        {
            CommonWords.SELF_AI,
            CommonWords.ALICE
        };

        public void CreateWord(Word word)
        {
            Debug.Log($"Creating word {word} in the world");
            words.Add(word);
        }

        public bool KillWord(Word word)
        {
            if (HasWord(word))
            {
                Debug.Log($"Killing word {word} from the world");
                words.Remove(word);
                return true;
            }
            else
            {
                Debug.Log($"Cannot kill word {word}, it is not in the world");
                return false;
            }
        }

        public void TransformWord(Word target, Word transformedForm)
        {
            Debug.Log($"Transforming {target} into {transformedForm}");
            if (KillWord(target))
            {
                CreateWord(transformedForm);
            }
        }

        public bool HasWord(Word word)
        {
            return words.Contains(word);
        }

        public bool HasLawsetForWord(Word word)
        {
            return lawsetPerWord.ContainsKey(word);
        }

        public Lawset GetLawsetForWord(Word word)
        {
            return lawsetPerWord[word];
        }

        public void SetLawsetForWord(Lawset lawset, Word word)
        {
            lawsetPerWord[word] = lawset;
        }
    }
}
