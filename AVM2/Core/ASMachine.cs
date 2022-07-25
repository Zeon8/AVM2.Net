namespace AVM2.Core;

public class ASMachine
{
    public readonly Stack<object> Values;
    public readonly Stack<object> Scopes;
    public readonly Dictionary<int, object> Registers;
    public readonly ASRuntime Runtime;

    public ASMachine(int localCount, ASRuntime runtime)
    {
        Values = new Stack<object>();
        Scopes = new Stack<object>();
        Registers = new Dictionary<int, object>(localCount);
        Runtime = runtime;
    }
}
