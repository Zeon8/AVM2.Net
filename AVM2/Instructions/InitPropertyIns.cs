using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class InitPropertyIns : ASInstruction
    {
        public int PropertyNameIndex { get; set; }
        public ASMultiname PropertyName => ABC.Pool.Multinames[PropertyNameIndex];

        public InitPropertyIns(ABCFile abc)
            : base(OPCode.InitProperty, abc)
        { }
        public InitPropertyIns(ABCFile abc, FlashReader input)
            : this(abc)
        {
            PropertyNameIndex = input.ReadInt30();
        }
        public InitPropertyIns(ABCFile abc, int propertyNameIndex)
            : this(abc)
        {
            PropertyNameIndex = propertyNameIndex;
        }

        public override int GetPopCount()
        {
            return (1 + ResolveMultinamePops(PropertyName) + 1);
        }
        public override void Execute(ASMachine machine)
        {
            object value = machine.Values.Pop();
            object obj = machine.Values.Pop();
            if(obj is IASObject instance)
                instance[PropertyName.Name] = value;
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(PropertyNameIndex);
        }
    }
}