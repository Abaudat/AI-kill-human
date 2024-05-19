namespace Core
{
    public class MakeAction : Action
    {
        public Word maker, target;

        public MakeAction(Word maker, Word target)
        {
            this.maker = maker;
            this.target = target;
        }

        public override bool Equals(object obj)
        {
            return obj is MakeAction action &&
                   maker.Equals(action.maker) &&
                   target.Equals(action.target);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(maker, target);
        }

        public override string ToString()
        {
            return $"[MAKE_ACTION, maker: {maker}, target: {target}]";
        }
    }
}