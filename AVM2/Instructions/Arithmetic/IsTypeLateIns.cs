using AVM2.Core;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class IsTypeLateIns : ASInstruction
    {
        public IsTypeLateIns()
            : base(OPCode.IsTypeLate)
        { }

        public override int GetPopCount()
        {
            return 2;
        }
        public override int GetPushCount()
        {
            return 1;
        }
        public override void Execute(ASMachine machine)
        {
            var type = (ASBaseClass)machine.Values.Pop();
            var value = (ASObject)machine.Values.Pop();
            machine.Values.Push(value.Class.IsAssignableTo(type));
        }
    }
}