using Flazzy.ABC;
using Flazzy.ABC.AVM2;
using Flazzy.ABC.AVM2.Instructions;

namespace AVM2.Core.Interpreted;

public class ASInterpretedMethod : IASMethod
{
    public string Name { get; }

    public bool IsStatic => throw new NotImplementedException();

    private ASMethod _method;
    private readonly ASRuntime _runtime;

    public ASInterpretedMethod(ASMethod method, ASRuntime runtime)
    {
        _method = method;
        Name = _method.Name ?? _method.Trait?.QName.Name ?? _method.Container?.QName.Name;
        _runtime = runtime;
    }

    public object Invoke(ASObject thisValue, params object[] args)
    {
        var body = _method.Body;
        var machine = new ASMachine(body.LocalCount, _runtime);
        SetupRegisters(thisValue, args, machine);

        var asCode = new ASCode(_method.ABC, body);
        return Execute(asCode,machine);
    }

    internal void ReplaceASMethod(ASMethod method) => _method = method;

    internal static object Execute(ASCode asCode, ASMachine machine, int startIndex = 0)
    {
        ASInstruction jumperExit = null;
        for (int i = startIndex; i < asCode.Count; i++)
        {
            ASInstruction instruction = asCode[i];
            if (jumperExit is not null && instruction != jumperExit)
                continue;
            jumperExit = null;

            if (instruction is Jumper jumper)
            {
                bool? condition = jumper.RunCondition(machine);
                if (condition.HasValue && condition.Value == true)
                    jumperExit = asCode.JumpExits[jumper];
            }
            else if (instruction is ReturnValueIns)
                return machine.Values.Pop();

            instruction.Execute(machine);
        }
        return null;
    }

    private static void SetupRegisters(ASObject thisValue, object[] args, ASMachine machine)
    {
        //Put "this" into register
        machine.Registers.Add(0, thisValue);

        //Put params into regiters
        for (int i = 0; i < args.Length; i++)
            machine.Registers[i + 1] = args[i];
    }
}