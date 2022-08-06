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

    public object GetValue(IASObject obj)
    {
        if(IsStatic)
            return _fieldInfo.GetValue(null);
        var nativeObject = (ASNativeObject)obj;
        return _fieldInfo.GetValue(nativeObject.Instance);
    }

    public void SetValue(IASObject obj, object value)
    {
        if(IsStatic)
            _fieldInfo.SetValue(null, value);
        else
        {
            var nativeObject = (ASNativeObject)obj;
            _fieldInfo.SetValue(nativeObject.Instance, value);
        }
    }
}
