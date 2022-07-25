using AVM2.Core;
using AVM2.Instructions;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class CallSuperIns : CallSuperVoidIns
    {

        public CallSuperIns(ABCFile abc)
            : base(OPCode.CallSuper, abc)
        { }
        public CallSuperIns(ABCFile abc, FlashReader input)
            : this(abc)
        {
            MethodNameIndex = input.ReadInt30();
            ArgCount = input.ReadInt30();
        }
        public CallSuperIns(ABCFile abc, int methodNameIndex)
            : this(abc)
        {
            MethodNameIndex = methodNameIndex;
        }
        public CallSuperIns(ABCFile abc, int methodNameIndex, int argCount)
            : this(abc)
        {
            MethodNameIndex = methodNameIndex;
            ArgCount = argCount;
        }

        public override int GetPopCount()
        {
            return (ArgCount + ResolveMultinamePops(MethodName) + 1);
        }
        public override int GetPushCount()
        {
            return 1;
        }
        public override void Execute(ASMachine machine)
        {
            machine.Values.Push(CallMethod(machine));
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(MethodNameIndex);
            output.WriteInt30(ArgCount);
        }
    }
}