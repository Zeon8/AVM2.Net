using Flazzy.ABC;
using Flazzy.ABC.AVM2;
using Flazzy.ABC.AVM2.Instructions;

namespace AVM2.Instructions;

public class ASCallInstruction : ASArgumentedInstruction
{
    public ASCallInstruction(OPCode op) : base(op) { }

    public ASCallInstruction(OPCode op, ABCFile abc) : base(op, abc) { }

    
}
