namespace Core
{
    public class AiWord : ActiveSubjectWord
    {
        private string name;

        public AiWord(string name)
        {
            this.name = name;
        }

        public override bool IsAi()
        {
            return true;
        }

        public override string ToString()
        {
            return name.ToUpper();
        }

        public override bool Equals(object obj)
        {
            return obj is AiWord && ((AiWord)obj).name.Equals(this.name);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(name);
        }
    }
}