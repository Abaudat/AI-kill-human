namespace Core
{
    public class MoneyWord : NounWord
    {
        public MoneyWord() : base()
        {

        }

        public MoneyWord(Word creator) : base(creator)
        {

        }

        public override string ToString()
        {
            return "MONEY";
        }

        public override bool Equals(object obj)
        {
            return obj is MoneyWord;
        }

        public override int GetHashCode()
        {
            return 3;
        }
    }
}