namespace Flazzy.ABC.AVM2.Instructions
{
    public class EqualsIns : Computation
    {
        public EqualsIns()
            : base(OPCode.Equals)
        { }

        protected override object Execute(object left, object right)
        {
            if(left is null)
                return right is null;
            return left.Equals(right);
        }
    }
}