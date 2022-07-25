namespace Flazzy.ABC.AVM2.Instructions
{
    public class AddIIns : Computation
    {
        public AddIIns()
            : base(OPCode.Add_i)
        { }

        protected override object Execute(object left, object right)
        {
            return Convert.ToInt32(left) + Convert.ToInt32(right);
        }
    }
}