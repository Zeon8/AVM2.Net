using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class IfStrictNotEqualIns : Jumper
    {
        public IfStrictNotEqualIns()
            : base(OPCode.IfStrictNE)
        { }
        public IfStrictNotEqualIns(FlashReader input)
            : base(OPCode.IfStrictNE, input)
        { }

        public override bool? RunCondition(ASMachine machine)
        {
            if (machine.Values.Pop() is not IComparable left || machine.Values.Pop() is not IComparable right) 
                return null;

            return !left.Equals(right);
        }
    }
}