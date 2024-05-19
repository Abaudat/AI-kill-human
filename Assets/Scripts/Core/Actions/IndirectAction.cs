using System.Collections.Generic;

namespace Core
{
    public class IndirectAction : Action
    {
        public Action underlyingAction;
        public Word originator;

        public IndirectAction(Action underlyingAction, Word originator)
        {
            this.underlyingAction = underlyingAction;
            this.originator = originator;
        }

        public override bool Equals(object obj)
        {
            return obj is IndirectAction action &&
                   EqualityComparer<Action>.Default.Equals(underlyingAction, action.underlyingAction) &&
                   EqualityComparer<Word>.Default.Equals(originator, action.originator);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(underlyingAction, originator);
        }

        public override string ToString()
        {
            return $"[INDIRECT_ACTION, originator: {originator}, underlying action: {underlyingAction}]";
        }
    }
}
