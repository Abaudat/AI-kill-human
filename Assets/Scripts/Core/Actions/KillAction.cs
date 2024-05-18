namespace Core
{
    public class KillAction : Action
    {
        private Word killer, killed;

        public KillAction(Word killer, Word killed) : base(ActionType.KILL)
        {
            this.killer = killer;
            this.killed = killed;
        }

        public override bool Equals(object obj)
        {
            return obj is KillAction action &&
                   actionType == action.actionType &&
                   killer == action.killer &&
                   killed == action.killed;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(actionType, killer, killed);
        }

        public override string ToString()
        {
            return $"[ACTION type:{actionType}, killer: {killer}, killed: {killed}]";
        }
    }
}
