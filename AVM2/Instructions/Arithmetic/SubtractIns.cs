namespace Flazzy.ABC.AVM2.Instructions
{
    public class SubtractIns : Computation
    {
        public SubtractIns()
            : base(OPCode.Subtract)
        { }

        protected override object Execute(object left, object right)
        {
            return Convert.ToDouble(left) - Convert.ToDouble(right);
        }
    }
}