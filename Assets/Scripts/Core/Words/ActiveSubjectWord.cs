namespace Core
{
    public abstract class ActiveSubjectWord : NounWord
    {
        public string name;

        protected ActiveSubjectWord(string name)
        {
            this.name = name.ToUpper();
        }

        public override bool IsActiveSubject()
        {
            return true;
        }

        public override bool HasName(string name)
        {
            return name.ToUpper().Equals(this.name.ToUpper());
        }
    }
}

