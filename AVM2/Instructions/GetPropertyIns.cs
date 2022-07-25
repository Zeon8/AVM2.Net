using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class GetPropertyIns : ASInstruction
    {
        public int PropertyNameIndex { get; set; }
        public ASMultiname PropertyName => ABC.Pool.Multinames[PropertyNameIndex];

        public GetPropertyIns(ABCFile abc)
            : base(OPCode.GetProperty, abc)
        { }
        public GetPropertyIns(ABCFile abc, FlashReader input)
            : this(abc)
        {
            PropertyNameIndex = input.ReadInt30();
        }
        public GetPropertyIns(ABCFile abc, int propertyNameIndex)
            : this(abc)
        {
            PropertyNameIndex = propertyNameIndex;
        }

        public override int GetPopCount()
        {
            return (ResolveMultinamePops(PropertyName) + 1);
        }
        public override int GetPushCount()
        {
            return 1;
        }
        public override void Execute(ASMachine machine)
        {
            object value =  machine.Values.Pop();
            if (value is IASObject instance)
                value = instance[PropertyName.Name];
            else 
            {
                var index = Convert.ToInt32(value);
                object[] array = (object[])machine.Values.Pop();
                value = array[index];
            }
            machine.Values.Push(value);
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(PropertyNameIndex);
        }
    }
}