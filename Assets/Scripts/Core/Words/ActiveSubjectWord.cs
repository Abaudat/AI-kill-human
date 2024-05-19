namespace Core
{
    public abstract class ActiveSubjectWord : NounWord
    {
        public string name;

        protected ActiveSubjectWord(string name)
        {
            this.name = name;
        }

        public override bool IsActiveSubject()
        {
            return true;
        }
    }
}

