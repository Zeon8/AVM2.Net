using AVM2.Core;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class AddIns : Computation
    {
        public AddIns()
            : base(OPCode.Add)
        { }

        protected override object Execute(dynamic left, dynamic right)
        {
            if(left is ASNativeObject nLeft && right is ASNativeObject nRight)
                return (dynamic)nLeft.Instance + (dynamic)nRight.Instance;
            else if(left is ASNativeObject nLeft2 )
                return (dynamic)nLeft2.Instance + right;
            else if(right is ASNativeObject nRight2 )
                return left + (dynamic)nRight2.Instance;
            else
                return left + right;
        }
    }
}