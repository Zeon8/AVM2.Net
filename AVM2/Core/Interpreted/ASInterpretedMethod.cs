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

    public object Invoke(IASObject thisValue, params object[] args)
    {
        var body = _method.Body;
        var machine = new ASMachine(body.LocalCount, _runtime);
        SetupRegisters(thisValue, args, machine);

        var asCode = new ASCode(_method.ABC, body);
        return Execute(asCode,machine);
    }

    private object Execute(ASCode asCode, ASMachine machine)
    {
        ASInstruction jumperExit = null;
        ASException expectedException = null;
        foreach (ASInstruction instruction in asCode)
        {
            if (jumperExit is not null && instruction != jumperExit)
                continue;
            jumperExit = null;

            if (instruction is Jumper jumper)
            {
                bool? condition = jumper.RunCondition(machine);
                if (condition.HasValue && condition.Value == true)
                    jumperExit = asCode.JumpExits[jumper];
            }
            else if(instruction is LookUpSwitchIns lookUpSwitchIns)
            {
                int value = (int)machine.Values.Pop();
                if(value > 0 || value < lookUpSwitchIns.CaseOffsets.Count)
                    jumperExit = asCode.SwitchExits[lookUpSwitchIns][value];
            }
            else if (instruction is ReturnValueIns)
                return machine.Values.Pop();
            
            foreach (var item in asCode.FromOffsets)
            {
                if(instruction == item.Value)
                    expectedException = item.Key;
            }

            if(expectedException is null)
                instruction.Execute(machine);
            else
            {
                try { instruction.Execute(machine); }
                catch (AVM2Exception e)
                {
                    jumperExit = asCode.TargetOffsets[expectedException];
                    var asObject = new ASNativeObject(_runtime.GetClass(typeof(AVM2Exception)), e);
                    machine.Values.Push(asObject);
                    expectedException = null;
                }
            }
        }
        return null;
    }

    private static void SetupRegisters(IASObject thisValue, object[] args, ASMachine machine)
    {
        //Put "this" into register
        machine.Registers.Add(0, thisValue);

        //Put params into regiters
        for (int i = 0; i < args.Length; i++)
            machine.Registers[i + 1] = args[i];
    }

    internal void ReplaceASMethod(ASMethod method) => _method = method;
}