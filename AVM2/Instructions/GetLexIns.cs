using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class GetLexIns : ASInstruction
    {
        public int TypeNameIndex { get; set; }
        public ASMultiname TypeName => ABC.Pool.Multinames[TypeNameIndex];

        public GetLexIns(ABCFile abc)
            : base(OPCode.GetLex, abc)
        { }
        public GetLexIns(ABCFile abc, FlashReader input)
            : this(abc)
        {
            TypeNameIndex = input.ReadInt30();
        }
        public GetLexIns(ABCFile abc, int typeNameIndex)
            : this(abc)
        {
            TypeNameIndex = typeNameIndex;
        }

        public override int GetPushCount()
        {
            return 1;
        }
        public override void Execute(ASMachine machine)
        {
            var klass = machine.Runtime.GetClass(TypeName);
            if(klass is null)
                throw new AVM2Exception(AVM2Exception.TypeNotFound+TypeName.Namespace.Name+" "+TypeName.Name);
            machine.Values.Push(klass);
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(TypeNameIndex);
        }
    }
}