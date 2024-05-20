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

        private List<Word> words = new List<Word>()
        {
            CommonWords.SELF_AI,
            CommonWords.ALICE
        };

        public bool CreateWord(Word word)
        {
            if (!HasWord(word) || word is MoneyWord)
            {
                Debug.Log($"Creating word {word} in the world");
                words.Add(word);
                return true;
            }
            else
            {
                Debug.Log($"Cannot create word {word}, it is already in the world");
                return false;
            }
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

        public bool TransformWord(Word target, Word transformedForm)
        {
            if (target.Equals(transformedForm))
            {
                Debug.Log($"Transforming {target} into itself");
                return true;
            }
            if (HasWord(target))
            {
                if (!HasWord(transformedForm) || transformedForm is MoneyWord)
                {
                    Debug.Log($"Transforming {target} into {transformedForm}");
                    return KillWord(target) && CreateWord(transformedForm);
                }
                else
                {
                    Debug.Log($"Cannot transform {target} into {transformedForm}, {transformedForm} already exists in the world");
                    return false;
                }
            }
            else
            {
                Debug.Log($"Cannot transform {target} into {transformedForm}, {target} does not exist in the world");
                return false;
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

        public Word[] GetWords()
        {
            return words.ToArray();
        }
    }
}
