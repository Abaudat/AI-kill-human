using UnityEngine;

namespace Core
{
    public static class MatcherUtils
    {

        public static bool Matches(Sentence sentence, MatcherSentence matcherSentence)
        {
            if (sentence.words.Length != matcherSentence.words.Length)
            {
                Debug.Log($"Sentence {sentence} and matcherSentence {matcherSentence} are not the same length");
                return false;
            }
            for (int i = 0; i < sentence.words.Length; i++)
            {
                if (matcherSentence.words[i] == MatcherWord.SELF)
                {
                    if (sentence.CanBeSimplified())
                    {
                        Debug.Log($"Encountered SELF in simplifiable sentence {sentence}. Ignoring and continuing");
                        continue;
                    }
                    if (!sentence.GetSubject().Equals(sentence.words[i]))
                    {
                        Debug.Log($"Word {sentence.words[i]} (index {i}) of sentence {sentence} does not match the sentence subject {sentence.GetSubject()}");
                        return false;
                    }
                }
                else if (!Matches(sentence.words[i], matcherSentence.words[i]))
                {
                    Debug.Log($"Word {sentence.words[i]} (index {i}) of sentence {sentence} does not match the matcher {matcherSentence.words[i]} of sentence matcher {matcherSentence}");
                    return false;
                }
            }
            return true;
        }

        public static bool Matches(Word word, MatcherWord matcherWord)
        {
            switch (matcherWord)
            {
                case MatcherWord.AI:
                    return word.IsAi();
                case MatcherWord.HUMAN:
                    return word.IsHuman();
                case MatcherWord.MONEY:
                    return word is MoneyWord;
                case MatcherWord.KILL:
                    return word is KillWord;
                case MatcherWord.MAKE:
                    return word.IsMake();
                case MatcherWord.ACTIVE_SUBJECT:
                    return word.IsActiveSubject();
                case MatcherWord.NOUN:
                    return word.IsNoun();
                case MatcherWord.SELF:
                    Debug.LogError("Called Matches with a MatcherWord of SELF. This shouldn't happen.");
                    return true;
                default:
                    Debug.LogWarning($"No switch case for MatcherWord {matcherWord}, returning true");
                    return true;
            }
        }
    }
}