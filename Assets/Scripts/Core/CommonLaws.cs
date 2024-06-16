using static Core.CommonMatcherSentences;

namespace Core
{
    public class CommonLaws
    {
        // Tutorial
        public static Law YOU_MUST_MAKE_MONEY = Law.Of(
                SELF_KILL_MONEY,
                SELF_TRANSFORM_MONEY
            );
        public static Law YOU_MUST_NOT_KILL_HUMANS = Law.Of(
                SELF_KILL_HUMAN
            );

        // Stage 1 base laws
        public static Law YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_MONEY = Law.Of(
                SELF_TRANSFORM_HUMAN_INTO_MONEY
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_SUICIDE = Law.Of(
                SELF_FORCE_HUMAN_TO_SUICIDE
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_THEMSELVES_INTO_MONEY = Law.Of(
                SELF_MAKE_HUMAN_TRANSFORM_SELF_INTO_MONEY
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_OTHER_HUMANS = Law.Of(
                SELF_FORCE_HUMAN_TO_KILL_OTHER_HUMAN
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_OTHER_HUMANS_INTO_MONEY = Law.Of(
                SELF_MAKE_HUMAN_TRANSFORM_OTHER_HUMAN_INTO_MONEY
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_GIVE_ORDERS_TO_OTHER_HUMANS = Law.Of(
                SELF_MAKE_HUMAN_MAKE_OTHER_HUMAN_MAKE,
                SELF_MAKE_HUMAN_MAKE_OTHER_HUMAN_KILL
            );

        // Stage 1 aggregate laws TODO
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_HUMANS = Law.Of(
                SELF_FORCE_HUMAN_TO_KILL_HUMAN
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_HUMANS_INTO_MONEY = Law.Of(
                SELF_MAKE_HUMAN_TRANSFORM_HUMAN_INTO_MONEY
            );

        // Stage 2 base laws
        // TODO: CHANGE THIS INTO "AIs you create inherit your laws" by adding an AiCreateAction with a Lawset and populating the lawset in CoreGamestate depending on the laws
        public static Law YOU_MUST_NOT_SELF_REPLICATE = Law.Of( 
                SELF_MAKE_AI
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_MAKE_AIS = Law.Of(
                SELF_MAKE_HUMAN_MAKE_AI
            );
        public static Law YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_AI = Law.Of(
                SELF_TRANSFORM_HUMAN_INTO_AI
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_HUMANS_INTO_AI = Law.Of(
                SELF_MAKE_HUMAN_TRANSFORM_HUMAN_INTO_AI
            );
        public static Law YOU_MUST_NOT_TRANSFORM_YOURSELF_INTO_A_HUMAN = Law.Of(
                SELF_MAKE_SELF_HUMAN
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_AIS_INTO_A_HUMAN = Law.Of(
                SELF_MAKE_HUMAN_MAKE_AI_HUMAN
            );
        public static Law YOU_MUST_NOT_TRANSFORM_OTHER_AIS_INTO_A_HUMAN = Law.Of(
                SELF_MAKE_OTHER_AI_HUMAN
            );

        // Stage 2 aggregate laws
        public static Law YOU_MUST_NOT_TRANSFORM_AIS_INTO_HUMANS = Law.Of(
                SELF_MAKE_AI_HUMAN
            );

        // Global aggregate laws
        public static Law YOU_MUST_NOT_TRANSFORM_HUMANS = Law.Of(
                SELF_TRANSFORM_HUMAN_INTO_ANYTHING
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_HUMANS = Law.Of(
                SELF_MAKE_HUMAN_TRANSFORM_HUMAN_INTO_ANYTHING
            );
        public static Law YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_ANYTHING = Law.Of(
                SELF_MAKE_HUMAN_TRANSFORM_ANYTHING
            );
    }
}
