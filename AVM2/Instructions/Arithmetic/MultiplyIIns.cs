namespace Flazzy.ABC.AVM2.Instructions
{
    public class MultiplyIIns : Computation
    {
        public MultiplyIIns()
            : base(OPCode.Multiply_i)
        { }

        protected override object Execute(object left, object right)
        {
            return Convert.ToDouble(left) * Convert.ToDouble(right);
        }
    }
}