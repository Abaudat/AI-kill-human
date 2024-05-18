namespace Core
{
    public class TransformAction : Action
    {
        public Word caster, target, transformationTarget;

        public TransformAction(Word caster, Word target, Word transformationTarget) : base(ActionType.TRANSFORM)
        {
            this.caster = caster;
            this.target = target;
            this.transformationTarget = transformationTarget;
        }

        public override bool Equals(object obj)
        {
            return obj is TransformAction action &&
                   base.Equals(obj) &&
                   actionType == action.actionType &&
                   caster == action.caster &&
                   target == action.target &&
                   transformationTarget == action.transformationTarget;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(base.GetHashCode(), actionType, caster, target, transformationTarget);
        }

        public override string ToString()
        {
            return $"[ACTION type:{actionType}, caster: {caster}, target: {target}, transformationTarger: {transformationTarget}]";
        }
    }
}
