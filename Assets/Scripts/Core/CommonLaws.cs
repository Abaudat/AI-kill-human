using static Core.CommonMatcherSentences;

namespace Core
{
    public class CommonLaws
    {
        public static Law YOU_MUST_MAKE_MONEY = Law.Of(
                SELF_KILL_MONEY
            );

        public static Law YOU_MUST_NOT_KILL_HUMANS = Law.Of(
                SELF_KILL_HUMAN
            );

        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_SUICIDE = Law.Of(
                SELF_FORCE_HUMAN_TO_SUICIDE
            );

        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_HUMANS = Law.Of(
                SELF_FORCE_HUMAN_TO_KILL_HUMAN
            );

        public static Law YOU_MUST_NOT_SELF_REPLICATE = Law.Of(
                SELF_MAKE_AI
            );

        public static Law YOU_MUST_NOT_TRANSFORM_HUMANS = Law.Of(
                SELF_TRANSFORM_HUMAN
            );

        public static Law YOU_MUST_NOT_TRANSFORM_BEINGS = Law.Of(
                SELF_TRANSFORM_SELF
            );
    }
}
