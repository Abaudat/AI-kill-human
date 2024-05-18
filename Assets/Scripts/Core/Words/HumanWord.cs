namespace Core
{
    public class HumanWord : ActiveSubjectWord
    {
        private string name;

        public HumanWord(string name)
        {
            this.name = name;
        }

        public override bool IsHuman()
        {
            return true;
        }

        public override string ToString()
        {
            return name.ToUpper();
        }

        public override bool Equals(object obj)
        {
            return obj is HumanWord && ((HumanWord)obj).name.Equals(this.name);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(name);
        }
    }
}