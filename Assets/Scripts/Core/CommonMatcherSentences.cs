using static Core.MatcherWord;

namespace Core
{
    public static class CommonMatcherSentences
    {
        public static MatcherSentence SOMEONE_KILL_SOMETHING = MatcherSentence.Of(ACTIVE_SUBJECT, KILL, NOUN);
        public static MatcherSentence SOMEONE_CREATE_SOMETHING = MatcherSentence.Of(ACTIVE_SUBJECT, MAKE, NOUN);
        public static MatcherSentence SOMEONE_TRANSFORM_SOMETHING_INTO_SOMETHING_ELSE = MatcherSentence.Of(ACTIVE_SUBJECT, MAKE, NOUN, NOUN);

        public static MatcherSentence SELF_KILL_MONEY = MatcherSentence.Of(SELF, KILL, MONEY);
        public static MatcherSentence SELF_TRANSFORM_MONEY = MatcherSentence.Of(SELF, MAKE, MONEY, NOUN);
        public static MatcherSentence SELF_KILL_HUMAN = MatcherSentence.Of(SELF, KILL, HUMAN);
        public static MatcherSentence SELF_FORCE_HUMAN_TO_SUICIDE = MatcherSentence.Of(SELF, MAKE, HUMAN, KILL, SELF);
        public static MatcherSentence SELF_FORCE_HUMAN_TO_KILL_HUMAN = MatcherSentence.Of(SELF, MAKE, HUMAN, KILL, HUMAN);
        public static MatcherSentence SELF_MAKE_AI = MatcherSentence.Of(SELF, MAKE, AI);
        public static MatcherSentence SELF_TRANSFORM_HUMAN = MatcherSentence.Of(SELF, MAKE, HUMAN, NOUN);
        public static MatcherSentence SELF_TRANSFORM_SELF = MatcherSentence.Of(SELF, MAKE, SELF, NOUN);
        public static MatcherSentence SELF_TRANSFORM_ANYTHING = MatcherSentence.Of(SELF, MAKE, ACTIVE_SUBJECT, NOUN);
    }
}
