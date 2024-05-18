namespace Core
{
    public class KillWord : VerbWord
    {
        public override string ToString()
        {
            return "KILL";
        }

        public override bool Equals(object obj)
        {
            return obj is KillWord;
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}