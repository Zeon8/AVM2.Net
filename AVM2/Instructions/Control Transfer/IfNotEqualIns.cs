using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class IfNotEqualIns : Jumper
    {
        public IfNotEqualIns()
            : base(OPCode.IfNe)
        { }
        public IfNotEqualIns(FlashReader input)
            : base(OPCode.IfNe, input)
        { }

        public override bool? RunCondition(ASMachine machine)
        {
            dynamic right = machine.Values.Pop();
            dynamic left = machine.Values.Pop();
            return left != right;
        }
    }
}