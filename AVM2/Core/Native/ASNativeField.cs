using System.Reflection;

namespace AVM2.Core.Native;

internal class ASNativeField : IASProperty
{
    private FieldInfo _fieldInfo;

    public string Name { get; }

    public bool IsStatic => _fieldInfo.IsStatic;

    public ASNativeField(FieldInfo fieldInfo)
    {
        _fieldInfo = fieldInfo;
        var attribute = fieldInfo.GetCustomAttribute<CustomNameAttribute>();
        var name = fieldInfo.Name[0].ToString().ToLower() + fieldInfo.Name[1..];
        Name = attribute?.CustomName ?? name;
    }

    public object GetValue(ASObject obj)
    {
        return _fieldInfo.GetValue(obj.NativeInstance);
    }

    public void SetValue(ASObject obj, object value)
    {
        _fieldInfo.SetValue(obj.NativeInstance, value);
    }
}
