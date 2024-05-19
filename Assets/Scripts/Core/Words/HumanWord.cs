namespace Core
{
    public class HumanWord : ActiveSubjectWord
    {
        public HumanWord(string name) : base(name)
        {
        }

        public override bool IsHuman()
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is HumanWord && ((HumanWord)obj).name.Equals(this.name);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(name, GetType());
        }

        public override string ToString()
        {
            return $"Human({name.ToUpper()})";
        }
    }
}