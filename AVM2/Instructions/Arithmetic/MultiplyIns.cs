namespace Flazzy.ABC.AVM2.Instructions
{
    public class MultiplyIns : Computation
    {
        public MultiplyIns()
            : base(OPCode.Multiply)
        { }

        protected override object Execute(object left, object right)
        {
            return Convert.ToDouble(left) * Convert.ToDouble(right);
        }
    }
}