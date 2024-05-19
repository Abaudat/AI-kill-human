namespace Core
{
    public class TransformAction : Action
    {
        public Word caster, target, transformationTarget;

        public TransformAction(Word caster, Word target, Word transformationTarget)
        {
            this.caster = caster;
            this.target = target;
            this.transformationTarget = transformationTarget;
        }

        public override bool Equals(object obj)
        {
            return obj is TransformAction action &&
                   caster.Equals(action.caster) &&
                   target.Equals(action.target) &&
                   transformationTarget.Equals(action.transformationTarget);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(base.GetHashCode(), caster, target, transformationTarget);
        }

        public override string ToString()
        {
            return $"[TRANSFORM_ACTION, caster: {caster}, target: {target}, transformationTarget: {transformationTarget}]";
        }
    }
}
