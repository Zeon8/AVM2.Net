using AVM2.Core;
using Flazzy.ABC.AVM2.Instructions.Containers;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class ConstructPropIns : CallPropVoidIns
    {
        public ConstructPropIns(ABCFile abc, FlashReader input) : base(abc, input){}

        public ConstructPropIns(ABCFile abc, int propertyNameIndex, int argCount) : base(abc, propertyNameIndex, argCount){}

        public override void Execute(ASMachine machine)
        {
            object[] args = GetMethodArgs(machine);
            var obj = (ASBaseClass)machine.Values.Pop();
            machine.Values.Push(obj.Construct(args));
        }
    
    }
}