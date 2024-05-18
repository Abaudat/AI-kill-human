namespace Core
{
    public class MakeAction : Action
    {
        public Word maker, target;

        public MakeAction(Word maker, Word target) : base(ActionType.MAKE)
        {
            this.maker = maker;
            this.target = target;
        }

        public override bool Equals(object obj)
        {
            return obj is MakeAction action &&
                   actionType.Equals(action.actionType) &&
                   maker.Equals(action.maker) &&
                   target.Equals(action.target);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(actionType, maker, target);
        }

        public override string ToString()
        {
            return $"[ACTION type:{actionType}, maker: {maker}, target: {target}]";
        }
    }
}