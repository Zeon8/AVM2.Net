using AVM2.Core;

namespace AVM2.Haxe;

[Serializable]
public class ValueException : AVM2Exception
{
    public object Value { get; }

    public ValueException(object value, string message) : base(message)
    {
        Value = value;
    }
}
