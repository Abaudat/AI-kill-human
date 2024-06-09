namespace Core
{
    public class HumanWord : ActiveSubjectWord
    {
        public HumanWord(string name) : base(name)
        {
        }

        public HumanWord(string name, Word creator) : base(name, creator)
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
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }
    }
}