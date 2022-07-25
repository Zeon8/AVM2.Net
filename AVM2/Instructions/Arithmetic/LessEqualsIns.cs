namespace Flazzy.ABC.AVM2.Instructions
{
    public class LessEqualsIns : Computation
    {
        public LessEqualsIns()
            : base(OPCode.LessEquals)
        { }

        protected override object Execute(object left, object right)
        {
            return Convert.ToDouble(left) <= Convert.ToDouble(right);
        }
    }
}