using AVM2.Core;

namespace AVM2.Haxe;

public class HaxeArgumentException : HaxeException
{
    public HaxeArgumentException(string argument, string message, HaxeException parentException, ASObject pos) : base(message ?? "Invalid argument \"" + argument + "\""){}
}
