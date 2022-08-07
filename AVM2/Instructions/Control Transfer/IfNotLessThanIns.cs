using System;
using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class IfNotLessThanIns : Jumper
    {
        public IfNotLessThanIns()
            : base(OPCode.IfNLt)
        { }
        public IfNotLessThanIns(FlashReader input)
            : base(OPCode.IfNLt, input)
        { }

        public override bool? RunCondition(ASMachine machine)
        {
            dynamic right = machine.Values.Pop();
            dynamic left = machine.Values.Pop();
            //if (left == null || right == null) return null;

            return !(left < right);
        }
    }
}