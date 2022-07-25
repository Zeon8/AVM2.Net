﻿using AVM2.Core;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class TypeOfIns : ASInstruction
    {
        public TypeOfIns()
            : base(OPCode.TypeOf)
        { }

        public override int GetPopCount()
        {
            return 1;
        }
        public override int GetPushCount()
        {
            return 1;
        }
        public override void Execute(ASMachine machine)
        {
            object value = machine.Values.Pop();
            machine.Values.Push(null);
        }
    }
}