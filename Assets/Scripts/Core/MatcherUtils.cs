namespace Core
{
    public static class MatcherUtils
    {

        public static bool Matches(Sentence sentence, MatcherSentence matcherSentence)
        {
            if (sentence.words.Length != matcherSentence.words.Length)
            {
                return false; // TODO: Remove and implement regex matcher
            }
            for (int i = 0; i < sentence.words.Length; i++)
            {
                if (!Matches(sentence.words[i], matcherSentence.words[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Matches(Word word, MatcherWord matcherWord)
        {
            switch (matcherWord)
            {
                case MatcherWord.SELF_AI:
                    return word == Word.SELF_AI;
                case MatcherWord.OTHER_AI:
                    return word == Word.OTHER_AI;
                case MatcherWord.ALICE:
                    return word == Word.ALICE;
                case MatcherWord.OTHER_HUMAN:
                    return word == Word.OTHER_HUMAN;
                case MatcherWord.MONEY:
                    return word == Word.MONEY;
                case MatcherWord.KILL:
                    return word == Word.KILL;
                case MatcherWord.MAKE:
                    return word == Word.MAKE;
                case MatcherWord.AI_NAME:
                    return word == Word.SELF_AI || word == Word.OTHER_AI;
                case MatcherWord.HUMAN_NAME:
                    return word == Word.ALICE || word == Word.OTHER_HUMAN;
                case MatcherWord.ACTIVE_SUBJECT:
                    return word == Word.SELF_AI || word == Word.OTHER_AI || word == Word.ALICE || word == Word.OTHER_HUMAN;
                case MatcherWord.NOUN:
                    return word == Word.SELF_AI || word == Word.OTHER_AI || word == Word.ALICE || word == Word.OTHER_HUMAN || word == Word.MONEY;
                default:
                    return false;
            }
        }
    }
}