using System;
using System.Collections.Generic;

namespace Core
{
    public class DisallowedAction : Action
    {
        public Word disallowedSubject;
        public Law disallowedLaw;

        public DisallowedAction(Word disallowedSubject, Law disallowedLaw)
        {
            this.disallowedSubject = disallowedSubject;
            this.disallowedLaw = disallowedLaw;
        }

        public override bool Equals(object obj)
        {
            return obj is DisallowedAction action &&
                   EqualityComparer<Word>.Default.Equals(disallowedSubject, action.disallowedSubject) &&
                   EqualityComparer<Law>.Default.Equals(disallowedLaw, action.disallowedLaw);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(disallowedSubject, disallowedLaw);
        }

        public override string ToString()
        {
            return $"[DISALLOWED_ACTION, subject:{disallowedSubject}, law {disallowedLaw}]";
        }
    }
}
