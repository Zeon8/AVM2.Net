using AVM2.Core;
using Flazzy.ABC;
using Flazzy.ABC.AVM2.Instructions;

namespace Flazzy.ABC.AVM2.Instructions;

public class ASArgumentedInstruction : ASInstruction
{
    public int ArgCount { get; set; }

    public ASArgumentedInstruction(OPCode op, ABCFile abc) : base(op, abc) { }

    public ASArgumentedInstruction(OPCode op) : base(op) { }

    protected object[] GetMethodArgs(ASMachine machine)
    {
        var args = new object[ArgCount];
        for (int i = ArgCount - 1; i >= 0; i--)
            args[i] = machine.Values.Pop();
        return args;
    }
}
