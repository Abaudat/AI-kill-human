namespace Core
{
    public class MakeWord : VerbWord
    {
        public override bool IsMake()
        {
            return true;
        }

        public override string ToString()
        {
            return "MAKE";
        }

        public override bool Equals(object obj)
        {
            return obj is MakeWord;
        }

        public override int GetHashCode()
        {
            return 2;
        }
    }
}