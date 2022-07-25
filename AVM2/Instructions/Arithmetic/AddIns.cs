namespace Flazzy.ABC.AVM2.Instructions
{
    public class AddIns : Computation
    {
        public AddIns()
            : base(OPCode.Add)
        { }

        protected override object Execute(object left, object right)
        {
            if(left is string || right is string)
                return left.ToString() + right.ToString();
            else
                return Convert.ToDouble(left) + Convert.ToDouble(right);
        }
    }
}