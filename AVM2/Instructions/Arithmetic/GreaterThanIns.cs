namespace Flazzy.ABC.AVM2.Instructions
{
    public class GreaterThanIns : Computation
    {
        public GreaterThanIns()
            : base(OPCode.GreaterThan)
        { }

        protected override object Execute(object left, object right)
        {
            return Convert.ToDouble(left) > Convert.ToDouble(right);
        }
    }
}