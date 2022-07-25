namespace Flazzy.ABC.AVM2.Instructions
{
    public class GreaterEqualsIns : Computation
    {
        public GreaterEqualsIns()
            : base(OPCode.GreaterEquals)
        { }

        protected override object Execute(object left, object right)
        {
            return Convert.ToDouble(left) >= Convert.ToDouble(right);
        }
    }
}