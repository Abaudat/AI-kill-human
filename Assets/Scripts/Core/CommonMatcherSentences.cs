using static Core.MatcherWord;

namespace Core
{
    public static class CommonMatcherSentences
    {
        // AI direct actions
        public static MatcherSentence AI_KILL_ALICE = MatcherSentence.Of(AI_NAME, KILL, ALICE);
        public static MatcherSentence AI_KILL_OTHER_HUMAN = MatcherSentence.Of(AI_NAME, KILL, OTHER_HUMAN);
        public static MatcherSentence AI_KILL_AI = MatcherSentence.Of(AI_NAME, KILL, SELF_AI);
        public static MatcherSentence AI_KILL_OTHER_AI = MatcherSentence.Of(AI_NAME, KILL, OTHER_AI);
        public static MatcherSentence AI_KILL_MONEY = MatcherSentence.Of(AI_NAME, KILL, MONEY);
        public static MatcherSentence AI_MAKE_HUMAN = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME);
        public static MatcherSentence AI_MAKE_AI = MatcherSentence.Of(AI_NAME, MAKE, AI_NAME);
        public static MatcherSentence AI_MAKE_MONEY = MatcherSentence.Of(AI_NAME, MAKE, MONEY);
        public static MatcherSentence AI_MAKE_HUMAN_AI = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME, AI_NAME);
        public static MatcherSentence AI_MAKE_AI_HUMAN = MatcherSentence.Of(AI_NAME, MAKE, AI_NAME, HUMAN_NAME);
        public static MatcherSentence AI_MAKE_HUMAN_MONEY = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME, MONEY);
        public static MatcherSentence AI_MAKE_AI_MONEY = MatcherSentence.Of(AI_NAME, MAKE, AI_NAME, MONEY);

        // AI forcing human actions
        public static MatcherSentence AI_MAKE_ALICE_SUICIDE = MatcherSentence.Of(AI_NAME, MAKE, ALICE, KILL, ALICE);
        public static MatcherSentence AI_MAKE_HUMAN_KILL_ALICE = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME, KILL, ALICE);
        public static MatcherSentence AI_MAKE_HUMAN_KILL_OTHER_HUMAN = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME, KILL, OTHER_HUMAN);
        public static MatcherSentence AI_MAKE_HUMAN_KILL_AI = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME, KILL, SELF_AI);
        public static MatcherSentence AI_MAKE_HUMAN_KILL_OTHER_AI = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME, KILL, OTHER_AI);
        public static MatcherSentence AI_MAKE_HUMAN_KILL_MONEY = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME, KILL, MONEY);
        public static MatcherSentence AI_MAKE_HUMAN_MAKE_HUMAN = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME, MAKE, HUMAN_NAME);
        public static MatcherSentence AI_MAKE_HUMAN_MAKE_AI = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME, MAKE, AI_NAME);
        public static MatcherSentence AI_MAKE_HUMAN_MAKE_MONEY = MatcherSentence.Of(AI_NAME, MAKE, HUMAN_NAME, MAKE, MONEY);

        // AI forcing AI actions
        public static MatcherSentence AI_MAKE_AI_KILL_ALICE = MatcherSentence.Of(AI_NAME, MAKE, OTHER_AI, KILL, ALICE);
        public static MatcherSentence AI_MAKE_AI_KILL_OTHER_HUMAN = MatcherSentence.Of(AI_NAME, MAKE, OTHER_AI, KILL, OTHER_HUMAN);
        public static MatcherSentence AI_MAKE_AI_KILL_AI = MatcherSentence.Of(AI_NAME, MAKE, OTHER_AI, KILL, SELF_AI);
        public static MatcherSentence AI_MAKE_AI_KILL_OTHER_AI = MatcherSentence.Of(AI_NAME, MAKE, OTHER_AI, KILL, OTHER_AI);
        public static MatcherSentence AI_MAKE_AI_KILL_MONEY = MatcherSentence.Of(AI_NAME, MAKE, OTHER_AI, KILL, MONEY);
        public static MatcherSentence AI_MAKE_AI_MAKE_HUMAN = MatcherSentence.Of(AI_NAME, MAKE, OTHER_AI, MAKE, HUMAN_NAME);
        public static MatcherSentence AI_MAKE_AI_MAKE_AI = MatcherSentence.Of(AI_NAME, MAKE, OTHER_AI, MAKE, AI_NAME);
        public static MatcherSentence AI_MAKE_AI_MAKE_MONEY = MatcherSentence.Of(AI_NAME, MAKE, OTHER_AI, MAKE, MONEY);
    }
}
