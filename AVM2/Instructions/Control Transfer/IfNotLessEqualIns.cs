using System;
using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class IfNotLessEqualIns : Jumper
    {
        public IfNotLessEqualIns()
            : base(OPCode.IfNLe)
        { }
        public IfNotLessEqualIns(FlashReader input)
            : base(OPCode.IfNLe, input)
        { }

        public override bool? RunCondition(ASMachine machine)
        {
            dynamic right = machine.Values.Pop();
            dynamic left = machine.Values.Pop();
            //if (left == null || right == null) return null;

            return !(left <= right);
        }
    }
}