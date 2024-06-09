using UnityEngine;

namespace Core
{
    public static class MatcherUtils
    {

        public static bool Matches(Sentence sentence, MatcherSentence matcherSentence)
        {
            if (sentence.isEmpty())
            {
                return true;
            }
            if (matcherSentence.ContainsTrailingAnything())
            {
                int trailingAnythingIndex = matcherSentence.TrailingAnythingIndex();
                return Matches(sentence.UpTo(trailingAnythingIndex), matcherSentence.TrimTrailingAnything());
            }
            if (sentence.words.Length != matcherSentence.words.Length)
            {
                Debug.Log($"Sentence {sentence} and matcherSentence {matcherSentence} are not the same length");
                return false;
            }
            if (sentence.CanBeSimplified())
            {
                Debug.Log($"Sentence {sentence} can be simplified, recursing");
                return Matches(sentence.GetIndirectionPart(), matcherSentence.GetIndirectionPart())
                    && Matches(sentence.SimplifyIndirection(), matcherSentence.SimplifyIndirection());
            }
            else
            {
                Debug.Log($"Sentence {sentence} cannot be simplified, matching it against {matcherSentence}");
                return MatchesWordForWord(sentence, matcherSentence);
            }
        }

        private static bool MatchesWordForWord(Sentence sentence, MatcherSentence matcherSentence)
        {
            for (int i = 0; i < sentence.words.Length; i++)
            {
                if (matcherSentence.words[i] == MatcherWord.SELF)
                {
                    if (!sentence.GetSubject().Equals(sentence.words[i]))
                    {
                        Debug.Log($"Word {sentence.words[i]} (index {i}) of sentence {sentence} does not match the sentence subject {sentence.GetSubject()}");
                        return false;
                    }
                }
                else if (matcherSentence.words[i] == MatcherWord.OTHER_HUMAN)
                {
                    if (!sentence.words[i].IsHuman() || sentence.GetSubject().Equals(sentence.words[i]))
                    {
                        Debug.Log($"Word {sentence.words[i]} (index {i}) of sentence {sentence} does not match the sentence subject {sentence.GetSubject()}");
                        return false;
                    }
                }
                else if (matcherSentence.words[i] == MatcherWord.OTHER_AI)
                {
                    if (!sentence.words[i].IsAi() || sentence.GetSubject().Equals(sentence.words[i]))
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
                case MatcherWord.SELF_AI:
                    return word.HasName("AI");
                case MatcherWord.SELF_AI_HUMAN:
                    return word.IsHuman() && word.HasName("AI");
                case MatcherWord.HUMAN:
                    return word.IsHuman();
                case MatcherWord.ALICE:
                    return word.HasName("Alice");
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
                case MatcherWord.OTHER_HUMAN:
                    Debug.LogError("Called Matches with a MatcherWord of OTHER_HUMAN. This shouldn't happen.");
                    return true;
                case MatcherWord.TRAILING_ANYTHING:
                    Debug.LogWarning("Called Matches with a MatcherWord of TRAILING_ANYTHING. TRAILING_ANYTHING should not exist somewhere else than the end of a sentence.");
                    return false;
                default:
                    Debug.LogWarning($"No switch case for MatcherWord {matcherWord}, returning true");
                    return true;
            }
        }
    }
}