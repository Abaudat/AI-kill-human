namespace Core
{
    public abstract class Word
    {
        public virtual bool IsHuman() => false;
        public virtual bool IsAi() => false;
        public virtual bool IsNoun() => false;
        public virtual bool IsActiveSubject() => false;
        public virtual bool IsVerb() => false;
        public virtual bool IsMake() => false;
    }
}
