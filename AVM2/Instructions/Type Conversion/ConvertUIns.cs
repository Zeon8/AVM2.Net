using System;
using AVM2.Core;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class ConvertUIns : ASInstruction
    {
        public ConvertUIns()
            : base(OPCode.Convert_u)
        { }

        public override int GetPopCount()
        {
            return 1;
        }
        public override int GetPushCount()
        {
            return 1;
        }
        public override void Execute(ASMachine machine)
        {
            object result = null;
            object value = machine.Values.Pop();
            if (value != null)
            {
                result = (uint)Convert.ToDouble(value);
            }
            machine.Values.Push(result);
        }
    }
}