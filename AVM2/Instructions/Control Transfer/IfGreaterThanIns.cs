using System;
using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class IfGreaterThanIns : Jumper
    {
        public IfGreaterThanIns()
            : base(OPCode.IfGt)
        { }
        public IfGreaterThanIns(FlashReader input)
            : base(OPCode.IfGt, input)
        { }

        public override bool? RunCondition(ASMachine machine)
        {
            dynamic right = machine.Values.Pop();
            dynamic  left = machine.Values.Pop();
            //if (left == null || right == null) return null;

            return left > right;
        }
    }
}