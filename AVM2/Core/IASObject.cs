using Flazzy.ABC;

namespace AVM2.Core;

public interface IASObject
{
    object this[string propertyName] { get; set; }
    object Invoke(string name, params object[] args);
}