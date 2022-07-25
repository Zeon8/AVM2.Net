using AVM2.Core;
using AVM2.Instructions;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class CallSuperVoidIns : ASArgumentedInstruction
    {
        public int MethodNameIndex { get; set; }
        public ASMultiname MethodName => ABC.Pool.Multinames[MethodNameIndex];

        public CallSuperVoidIns(ABCFile abc)
            : base(OPCode.CallSuperVoid, abc)
        { }
        public CallSuperVoidIns(ABCFile abc, FlashReader input)
            : this(abc)
        {
            MethodNameIndex = input.ReadInt30();
            ArgCount = input.ReadInt30();
        }
        public CallSuperVoidIns(ABCFile abc, int methodNameIndex)
            : this(abc)
        {
            MethodNameIndex = methodNameIndex;
        }
        public CallSuperVoidIns(ABCFile abc, int methodNameIndex, int argCount)
            : this(abc)
        {
            MethodNameIndex = methodNameIndex;
            ArgCount = argCount;
        }

        public CallSuperVoidIns(OPCode op, ABCFile abc) : base(op, abc){}

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
            CallMethod(machine);
        }
        
        
        public object CallMethod(ASMachine machine)
        {
            object[] args = GetMethodArgs(machine);
            var instance = (ASObject)machine.Values.Pop();
            return instance.Super.Invoke(MethodName.Name, args);
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(MethodNameIndex);
            output.WriteInt30(ArgCount);
        }
    }
}