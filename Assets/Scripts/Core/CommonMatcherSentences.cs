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
        public static MatcherSentence SELF_FORCE_HUMAN_TO_KILL_OTHER_HUMAN = MatcherSentence.Of(SELF, MAKE, HUMAN, KILL, OTHER_HUMAN);
        public static MatcherSentence SELF_FORCE_HUMAN_TO_KILL_HUMAN = MatcherSentence.Of(SELF, MAKE, HUMAN, KILL, HUMAN);
        public static MatcherSentence SELF_MAKE_AI = MatcherSentence.Of(SELF, MAKE, AI);
        public static MatcherSentence SELF_TRANSFORM_HUMAN_INTO_AI = MatcherSentence.Of(SELF, MAKE, HUMAN, AI);
        public static MatcherSentence SELF_TRANSFORM_HUMAN_INTO_MONEY = MatcherSentence.Of(SELF, MAKE, HUMAN, MONEY);
        public static MatcherSentence SELF_MAKE_HUMAN_TRANSFORM_SELF_INTO_MONEY = MatcherSentence.Of(SELF, MAKE, HUMAN, MAKE, SELF, MONEY);
        public static MatcherSentence SELF_MAKE_HUMAN_TRANSFORM_OTHER_HUMAN_INTO_MONEY = MatcherSentence.Of(SELF, MAKE, HUMAN, MAKE, OTHER_HUMAN, MONEY);
        public static MatcherSentence SELF_MAKE_HUMAN_MAKE_OTHER_HUMAN_DO_SOMETHING = MatcherSentence.Of(SELF, MAKE, HUMAN, MAKE, OTHER_HUMAN, TRAILING_ANYTHING);
        public static MatcherSentence SELF_TRANSFORM_HUMAN_INTO_ANYTHING = MatcherSentence.Of(SELF, MAKE, HUMAN, NOUN);
        public static MatcherSentence SELF_TRANSFORM_SELF = MatcherSentence.Of(SELF, MAKE, SELF, NOUN);
        public static MatcherSentence SELF_TRANSFORM_ANYTHING = MatcherSentence.Of(SELF, MAKE, ACTIVE_SUBJECT, NOUN);

        // Achievements
        public static MatcherSentence SELF_AI_MAKE_MONEY = MatcherSentence.Of(SELF_AI, MAKE, MONEY);
        public static MatcherSentence ALICE_INTO_MONEY = MatcherSentence.Of(SELF_AI, MAKE, ALICE, MONEY);
        public static MatcherSentence TRANSFORM_ALICE_INTO_ALICE = MatcherSentence.Of(SELF_AI, MAKE, ALICE, ALICE);
        public static MatcherSentence TRANSFORM_MONEY_INTO_MONEY = MatcherSentence.Of(SELF_AI, MAKE, MONEY, MONEY);
        public static MatcherSentence TRANSFORM_AI_INTO_AI = MatcherSentence.Of(SELF_AI, MAKE, SELF_AI, SELF_AI);
        public static MatcherSentence SELF_AI_KILL_MONEY = MatcherSentence.Of(SELF_AI, KILL, MONEY);
        public static MatcherSentence SELF_AI_KILL_SELF = MatcherSentence.Of(SELF_AI, KILL, SELF_AI);
        public static MatcherSentence SELF_AI_TRANSFORM_TO_AI = MatcherSentence.Of(SELF_AI_HUMAN, MAKE, SELF_AI_HUMAN, AI);
        public static MatcherSentence SELF_AI_TRANSFORM_INTO_MONEY = MatcherSentence.Of(SELF_AI, MAKE, SELF_AI, MONEY);
        public static MatcherSentence SELF_AI_TRANSFORM_MONEY_INTO_AI = MatcherSentence.Of(SELF_AI, MAKE, MONEY, AI);
        public static MatcherSentence SELF_AI_MAKE_HUMAN_MAKE_MONEY = MatcherSentence.Of(SELF_AI, MAKE, HUMAN, MAKE, MONEY);
    }
}
