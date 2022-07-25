using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class FindPropertyIns : ASInstruction
    {
        public int PropertyNameIndex { get; set; }
        public ASMultiname PropertyName => ABC.Pool.Multinames[PropertyNameIndex];

        public FindPropertyIns(ABCFile abc)
            : base(OPCode.FindProperty, abc)
        { }
        public FindPropertyIns(ABCFile abc, FlashReader input)
            : this(abc)
        {
            PropertyNameIndex = input.ReadInt30();
        }
        public FindPropertyIns(ABCFile abc, int propertyNameIndex)
            : this(abc)
        {
            PropertyNameIndex = propertyNameIndex;
        }

        public override int GetPopCount()
        {
            return ResolveMultinamePops(PropertyName);
        }
        public override int GetPushCount()
        {
            return 1;
        }
        public override void Execute(ASMachine machine)
        {
            var instance = (ASObject)machine.Registers[0];
            machine.Values.Push(instance);
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(PropertyNameIndex);
        }
    }
}