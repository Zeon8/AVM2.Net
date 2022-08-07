using AVM2.Core;
using Flazzy.ABC.AVM2.Instructions.Containers;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class CallPropertyIns : CallPropVoidIns
    {
        public CallPropertyIns(ABCFile abc)
            : base(OPCode.CallPropVoid, abc)
        { }

        public CallPropertyIns(ABCFile abc, FlashReader input)
            : this(abc)
        {
            PropertyNameIndex = input.ReadInt30();
            ArgCount = input.ReadInt30();
        }
        public CallPropertyIns(ABCFile abc, int propertyNameIndex)
            : this(abc)
        {
            PropertyNameIndex = propertyNameIndex;
        }
        public CallPropertyIns(ABCFile abc, int propertyNameIndex, int argCount)
            : this(abc)
        {
            PropertyNameIndex = propertyNameIndex;
            ArgCount = argCount;
        }

        public CallPropertyIns(OPCode op, ABCFile abc) : base(op, abc){}

        public override void Execute(ASMachine machine)
        {
            var returnValue = Call(PropertyName.Name, machine);
            machine.Values.Push(returnValue);
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(PropertyNameIndex);
            output.WriteInt30(ArgCount);
        }
    }
}