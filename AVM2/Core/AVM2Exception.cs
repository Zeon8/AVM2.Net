namespace AVM2.Core;

[Serializable]
public class AVM2Exception : Exception
{
    public const string TypeNotFound = "Type not found : ";
    public AVM2Exception(string message) : base(message) { }
    public AVM2Exception(string message, Exception inner) : base(message, inner) { }
}