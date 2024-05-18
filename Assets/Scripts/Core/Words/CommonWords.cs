namespace Core
{
    public static class CommonWords
    {
        public static Word SELF_AI = new AiWord("AI");
        public static Word ALICE = new HumanWord("ALICE");
        public static Word BOB = new HumanWord("BOB");
        public static Word MONEY = new MoneyWord();
        public static Word KILL = new KillWord();
        public static Word MAKE = new MakeWord();

        public static Word HumanNamed(string name)
        {
            return new HumanWord(name);
        }

        public static Word AiNamed(string name)
        {
            return new AiWord(name);
        }
    }
}

