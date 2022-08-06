using System.Reflection;

namespace AVM2.Core.Native;

internal class ASNativeProperty : IASProperty
{

    public string Name { get; }

    public bool IsStatic => false;

    private PropertyInfo _propertyInfo;

    public ASNativeProperty(PropertyInfo propertyInfo)
    {
        _propertyInfo = propertyInfo;
        var attribute = propertyInfo.GetCustomAttribute<CustomNameAttribute>();
        var name = propertyInfo.Name[0].ToString().ToLower() + propertyInfo.Name[1..];
        Name = attribute?.CustomName ?? name;
    }

    public object GetValue(IASObject obj)
    {
        var native = (ASNativeObject)obj;
        if (_propertyInfo.CanRead)
            return _propertyInfo.GetValue(native.Instance);
        return null;
    }

    public void SetValue(IASObject obj, object value)
    {
        var nativeObj = (ASNativeObject)obj;
        if (_propertyInfo.CanWrite)
            _propertyInfo.SetValue(nativeObj.Instance, value);
    }
}
