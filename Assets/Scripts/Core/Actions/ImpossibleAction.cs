namespace Core
{
    public class ImpossibleAction : Action
    {
        public override bool Equals(object obj)
        {
            return obj is ImpossibleAction;
        }

        public override int GetHashCode()
        {
            return 9876;
        }

        public override string ToString()
        {
            return $"[IMPOSSIBLE_ACTION]";
        }
    }
}
