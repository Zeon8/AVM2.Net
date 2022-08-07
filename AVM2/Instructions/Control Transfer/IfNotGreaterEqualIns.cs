using System;
using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class IfNotGreaterEqualIns : Jumper
    {
        public IfNotGreaterEqualIns()
            : base(OPCode.IfNGe)
        { }
        public IfNotGreaterEqualIns(FlashReader input)
            : base(OPCode.IfNGe, input)
        { }

        public override bool? RunCondition(ASMachine machine)
        {
            dynamic right = machine.Values.Pop();
            dynamic left = machine.Values.Pop();
            //if (left == null || right == null) return null;

            return !(left >= right);
        }
    }
}