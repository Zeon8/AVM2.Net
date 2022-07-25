using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class FindPropStrictIns : ASInstruction
    {
        public int PropertyNameIndex { get; set; }
        public ASMultiname PropertyName => ABC.Pool.Multinames[PropertyNameIndex];

        public FindPropStrictIns(ABCFile abc)
            : base(OPCode.FindPropStrict, abc)
        { }
        public FindPropStrictIns(ABCFile abc, FlashReader input)
            : this(abc)
        {
            PropertyNameIndex = input.ReadInt30();
        }
        public FindPropStrictIns(ABCFile abc, int propertyNameIndex)
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
            object value;
            var instance = (ASObject)machine.Registers[0];
            if(instance is not null 
                && (instance.Class.GetProperty(PropertyName.Name) is not null 
                || instance.Class.GetMethod(PropertyName.Name) is not null))
                value = instance;
            else
                value = machine.Runtime.GetClass(PropertyName);
            
            if(value is null)
                throw new AVM2Exception(AVM2Exception.TypeNotFound+PropertyName.Name);
            machine.Values.Push(value);
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(PropertyNameIndex);
        }
    }
}