using AVM2.Core;
using AVM2.Instructions;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class ConstructSuperIns : ASArgumentedInstruction
    {

        public ConstructSuperIns()
            : base(OPCode.ConstructSuper)
        { }
        public ConstructSuperIns(int argCount)
            : this()
        {
            ArgCount = argCount;
        }
        public ConstructSuperIns(FlashReader input)
            : this()
        {
            ArgCount = input.ReadInt30();
        }

        public override int GetPopCount()
        {
            return (ArgCount + 1);
        }
        public override void Execute(ASMachine machine)
        {
            object[] args = GetMethodArgs(machine);
            var instance = (ASObject)machine.Values.Pop();
            instance.Super = instance.Class.BaseClass.Construct(args);
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(ArgCount);
        }
    }
}