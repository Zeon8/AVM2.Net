using AVM2.Core;

namespace AVM2.Haxe;

public class HaxeNotImplementedException : HaxeException
{
    public HaxeNotImplementedException(string message = "Not implemented", AVM2Exception previous = null, ASObject pos = null) : base(message){}
}
