using System;
using UnityEngine;

namespace Core
{
    public class Action
    {
        public ActionType actionType;

        public Action(ActionType actionType)
        {
            this.actionType = actionType;
        }

        public virtual Action FromSentence()
        {
            Debug.LogWarning("Action created from top-level constructor. Defaulting to NO_ACTION.");
            return new(ActionType.NO_ACTION);
        }

        public override bool Equals(object obj)
        {
            return obj is Action action &&
                   actionType.Equals(action.actionType);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(actionType);
        }

        public override string ToString()
        {
            return $"[ACTION type:{actionType}]";
        }
    }
}
