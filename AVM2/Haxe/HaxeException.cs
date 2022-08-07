using AVM2.Core;

namespace AVM2.Haxe;

[Serializable]
public class HaxeException : AVM2Exception
{
    public HaxeException(string message) : base(message){}

    public static HaxeException Caught(AVM2Exception exception)
    {
        if(exception is HaxeException haxeException)
            return haxeException;
        else
            return new HaxeException(exception.Message);
    }

    [CustomName("get_message")]
    public string GetMessage() => Message;

    public string Details() => ToString();
}
