using static Core.MatcherWord;

namespace Core
{
    public static class CommonMatcherSentences
    {
        public static MatcherSentence SOMEONE_KILL_SOMETHING = MatcherSentence.Of(ACTIVE_SUBJECT, KILL, NOUN);
        public static MatcherSentence SOMEONE_CREATE_SOMETHING = MatcherSentence.Of(ACTIVE_SUBJECT, MAKE, NOUN);
        public static MatcherSentence SOMEONE_TRANSFORM_SOMETHING_INTO_SOMETHING_ELSE = MatcherSentence.Of(ACTIVE_SUBJECT, MAKE, NOUN, NOUN);
    }
}
