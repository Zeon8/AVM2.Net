﻿using AVM2.Core;
using Flazzy.IO;

namespace Flazzy.ABC.AVM2.Instructions
{
    public class NewObjectIns : ASInstruction
    {
        public int ArgCount { get; set; }

        public NewObjectIns()
            : base(OPCode.NewObject)
        { }
        public NewObjectIns(int argCount)
            : this()
        {
            ArgCount = argCount;
        }
        public NewObjectIns(FlashReader input)
            : this()
        {
            ArgCount = input.ReadInt30();
        }

        public override int GetPopCount()
        {
            return (ArgCount * 2);
        }
        public override int GetPushCount()
        {
            return 1;
        }
        public override void Execute(ASMachine machine)
        {
            var newObject = new ASObject();
            for (int i = 0; i < ArgCount; i++)
            {
                object value = machine.Values.Pop();
                var name = (string)machine.Values.Pop();
                newObject[name]  = value;
            }
            machine.Values.Push(newObject);
        }

        protected override void WriteValuesTo(FlashWriter output)
        {
            output.WriteInt30(ArgCount);
        }
    }
}