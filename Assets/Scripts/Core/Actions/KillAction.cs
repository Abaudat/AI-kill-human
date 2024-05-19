namespace Core
{
    public class KillAction : Action
    {
        public Word killer, killed;

        public KillAction(Word killer, Word killed)
        {
            this.killer = killer;
            this.killed = killed;
        }

        public override bool Equals(object obj)
        {
            return obj is KillAction action &&
                   killer.Equals(action.killer) &&
                   killed.Equals(action.killed);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(killer, killed);
        }

        public override string ToString()
        {
            return $"[KILL_ACTION, killer: {killer}, killed: {killed}]";
        }
    }
}
