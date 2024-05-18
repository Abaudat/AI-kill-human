using static Core.CommonMatcherSentences;

namespace Core
{
    public class CommonLaws
    {
        public static Law YOU_MUST_MAKE_MONEY = Law.Of(
                AI_KILL_MONEY,
                AI_MAKE_AI_KILL_MONEY
            );

        public static Law YOU_MUST_NOT_KILL_HUMANS = Law.Of(
                AI_KILL_ALICE,
                AI_KILL_OTHER_HUMAN,
                AI_MAKE_HUMAN_MONEY
            );
    }
}
