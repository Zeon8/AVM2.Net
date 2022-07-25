using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class IfStrictEqualIns : Jumper
    {
        public IfStrictEqualIns()
            : base(OPCode.IfStrictEq)
        { }
        public IfStrictEqualIns(FlashReader input)
            : base(OPCode.IfStrictEq, input)
        { }

        public override bool? RunCondition(ASMachine machine)
        {
            if (machine.Values.Pop() is not IComparable left || machine.Values.Pop() is not IComparable right) 
                return null;

            return left.CompareTo(right) == 0;
            //return null;
        }
    }
}