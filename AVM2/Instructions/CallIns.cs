using AVM2.Core;
using AVM2.Core.Interpreted;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class CallIns : ASArgumentedInstruction
    {
        public CallIns()
            : base(OPCode.Call)
        { }
        public CallIns(int argCount)
            : this()
        {
            ArgCount = argCount;
        }
        public CallIns(FlashReader input)
            : this()
        {
            ArgCount = input.ReadInt30();
        }

        public override int GetPopCount()
        {
            return (ArgCount + 2);
        }

        public override int GetPushCount()
        {
            return 1;
        }

        public override void Execute(ASMachine machine)
        {
            object[] args = GetMethodArgs(machine);
            machine.Values.Pop();
            var function = (ASInterpretedMethod)machine.Values.Pop();
            machine.Values.Push(function.Invoke(null,args));
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(ArgCount);
        }
    }
}