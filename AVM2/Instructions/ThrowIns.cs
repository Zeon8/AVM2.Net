using AVM2.Core;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class ThrowIns : ASInstruction
    {
        public ThrowIns()
            : base(OPCode.Throw)
        { }

        public override int GetPopCount()
        {
            return 1;
        }
        public override void Execute(ASMachine machine)
        {
            var exceptionObject = (ASObject)machine.Values.Pop();

            while (exceptionObject is not null && exceptionObject is not ASNativeObject)
                exceptionObject = exceptionObject.Super;
                
            var nativeException = (ASNativeObject)exceptionObject;
            throw (AVM2Exception)nativeException.Instance;
        }
    }
}