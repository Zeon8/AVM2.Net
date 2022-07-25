namespace Flazzy.ABC.AVM2.Instructions
{
    public class LessThanIns : Computation
    {
        public LessThanIns()
            : base(OPCode.LessThan)
        { }

        protected override object Execute(object left, object right)
        {
            return Convert.ToDouble(left) < Convert.ToDouble(right);
        }
    }
}