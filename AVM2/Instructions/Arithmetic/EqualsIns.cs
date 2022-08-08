using AVM2.Core;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class EqualsIns : Computation
    {
        public EqualsIns()
            : base(OPCode.Equals)
        { }

        protected override object Execute(dynamic left, dynamic right)
        {
            if(left is null)
                return right is null;
            if(left is ASNativeObject nLeft && right is ASNativeObject nRight)
                return (dynamic)nLeft.Instance == (dynamic)nRight.Instance;
            return left == right;
        }
    }
}