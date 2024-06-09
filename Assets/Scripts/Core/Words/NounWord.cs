namespace Core
{
    public abstract class NounWord : Word
    {
        protected Word creator;

        protected NounWord()
        {
            this.creator = this;
        }

        protected NounWord(Word creator)
        {
            this.creator = creator;
        }

        public Word GetCreator()
        {
            return creator;
        }

        public override bool IsNoun()
        {
            return true;
        }
    }
}