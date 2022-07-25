namespace AVM2.Core;

internal interface IASObject
{
    object this[string propertyName] { get; set; }
    internal object Invoke(string name, params object[] args);
}