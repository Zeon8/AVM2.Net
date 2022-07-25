using AVM2;
using AVM2.Core;

public class Log
{
    public static void Trace(params object[] args)
    {
        for (int i = 0; i < args.Length-1; i++)
        {
            Console.Write(args[i] + " ");
        }
        var debug = (ASObject)args.Last();
        Console.WriteLine("\n"+debug["className"] + "." + debug["methodName"] + "\n  at " + debug["fileName"] + ":" + debug["lineNumber"]);
    }
}