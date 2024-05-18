namespace Core
{
    public enum Word
    {
        SELF_AI,
        OTHER_AI,
        ALICE,
        OTHER_HUMAN,
        MONEY,
        KILL,
        MAKE
    }

    public static class Extensions
    {
        public static bool IsActiveSubject(this Word word)
        {
            return word == Word.SELF_AI ||
                word == Word.OTHER_AI ||
                word == Word.ALICE ||
                word == Word.OTHER_HUMAN;
        }

        public static bool IsMake(this Word word)
        {
            return word == Word.MAKE;
        }

        public static bool IsVerb(this Word word)
        {
            return word == Word.MAKE || word == Word.KILL;
        }
    }
}
