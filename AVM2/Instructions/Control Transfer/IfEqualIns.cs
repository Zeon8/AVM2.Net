﻿using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class IfEqualIns : Jumper
    {
        public IfEqualIns()
            : base(OPCode.IfEq)
        { }
        public IfEqualIns(FlashReader input)
            : base(OPCode.IfEq, input)
        { }

        public override bool? RunCondition(ASMachine machine)
        {
            dynamic right = machine.Values.Pop();
            dynamic left = machine.Values.Pop();
            //if (left == null || right == null) return null;
            if(right is ASNativeObject nRight && left is ASNativeObject nLeft)
                return (dynamic)nRight.Instance == (dynamic)nLeft.Instance;

            return left == right;
        }
    }
}