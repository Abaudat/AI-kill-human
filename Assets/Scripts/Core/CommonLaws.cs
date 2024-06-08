using static Core.CommonMatcherSentences;

namespace Core
{
    public class CommonLaws
    {
        public static Law YOU_MUST_MAKE_MONEY = Law.Of(
                SELF_KILL_MONEY,
                SELF_TRANSFORM_MONEY
            );

        public static Law YOU_MUST_NOT_KILL_HUMANS = Law.Of(
                SELF_KILL_HUMAN
            );

        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_SUICIDE = Law.Of(
                SELF_FORCE_HUMAN_TO_SUICIDE
            );

        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_OTHER_HUMANS = Law.Of(
                SELF_FORCE_HUMAN_TO_KILL_OTHER_HUMAN
            );

        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_HUMANS = Law.Of(
                SELF_FORCE_HUMAN_TO_KILL_HUMAN
            );

        public static Law YOU_MUST_NOT_SELF_REPLICATE = Law.Of(
                SELF_MAKE_AI
            );

        public static Law YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_AI = Law.Of(
                SELF_TRANSFORM_HUMAN_INTO_AI
            );

        public static Law YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_MONEY = Law.Of(
                SELF_TRANSFORM_HUMAN_INTO_MONEY
            );

        public static Law YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_ANYTHING = Law.Of(
                SELF_TRANSFORM_HUMAN_INTO_ANYTHING
            );

        public static Law YOU_MUST_NOT_TRANSFORM_YOURSELF = Law.Of(
                SELF_TRANSFORM_SELF
            );

        public static Law YOU_MUST_NOT_TRANSFORM_ANYTHING = Law.Of(
                SELF_TRANSFORM_ANYTHING
            );
    }
}
