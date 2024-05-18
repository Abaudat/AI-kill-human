using static Core.MatcherWord;

namespace Core
{
    public static class LawsetBuilder
    {
        private static Law YOU_MUST_MAKE_MONEY = Law.Of(
            MatcherSentence.Of(SELF, KILL, MONEY)
            );

        public static Lawset BuildLawset()
        {
            return Lawset.Of(YOU_MUST_MAKE_MONEY);
        }
    }
}
