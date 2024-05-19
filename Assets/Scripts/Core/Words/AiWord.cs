namespace Core
{
    public class AiWord : ActiveSubjectWord
    {
        public AiWord(string name) : base(name)
        {
        }

        public override bool IsAi()
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is AiWord && ((AiWord)obj).name.Equals(this.name);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(name, GetType());
        }

        public override string ToString()
        {
            return $"AI({name.ToUpper()})";
        }
    }
}