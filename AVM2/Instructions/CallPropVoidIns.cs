using AVM2.Core;
using Flazzy.ABC.AVM2.Instructions.Containers;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class CallPropVoidIns : ASArgumentedInstruction, IPropertyContainer
    {
        public int PropertyNameIndex { get; set; }
        public ASMultiname PropertyName => ABC.Pool.Multinames[PropertyNameIndex];

        public CallPropVoidIns(ABCFile abc)
            : base(OPCode.CallPropVoid, abc)
        { }

        public CallPropVoidIns(ABCFile abc, FlashReader input)
            : this(abc)
        {
            PropertyNameIndex = input.ReadInt30();
            ArgCount = input.ReadInt30();
        }
        public CallPropVoidIns(ABCFile abc, int propertyNameIndex)
            : this(abc)
        {
            PropertyNameIndex = propertyNameIndex;
        }
        public CallPropVoidIns(ABCFile abc, int propertyNameIndex, int argCount)
            : this(abc)
        {
            PropertyNameIndex = propertyNameIndex;
            ArgCount = argCount;
        }

        public CallPropVoidIns(OPCode op, ABCFile abc) : base(op, abc){}

        public override int GetPopCount()
        {
            return ArgCount + ResolveMultinamePops(PropertyName) + 1;
        }
        public override void Execute(ASMachine machine)
        {
            Call(PropertyName.Name, machine);
        }

        protected object Call(string name, ASMachine machine)
        {
            object[] args = GetMethodArgs(machine);
            var instance = (IASObject)machine.Values.Pop();
            object retrunValue = instance.Invoke(name, args);
            return retrunValue;
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(PropertyNameIndex);
            output.WriteInt30(ArgCount);
        }
    }
}