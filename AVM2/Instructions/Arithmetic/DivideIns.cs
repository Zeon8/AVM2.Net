namespace Flazzy.ABC.AVM2.Instructions
{
    public class DivideIns : Computation
    {
        public DivideIns()
            : base(OPCode.Divide)
        { }

        protected override object Execute(object left, object right)
        {
            return Convert.ToDouble(left)/Convert.ToDouble(right);
        }
    }
}