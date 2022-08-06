using System.Reflection;

namespace AVM2.Core.Native;

internal class ASNativeMethod : IASMethod
{
    private readonly MethodBase _method;

    public string Name { get; }

    public bool IsStatic { get; }

    public ASNativeMethod(MethodBase method)
    {
        _method = method;
        IsStatic = method.IsStatic;

        var attribute = method.GetCustomAttribute<CustomNameAttribute>();
        var name = method.Name[0].ToString().ToLower() + method.Name[1..];
        Name = attribute?.CustomName ?? name;
    }


    public object Invoke(IASObject thisValue, params object[] args)
    {
        object nativeObject = null;
        if (thisValue is not null)
        {
            ASObject obj = (ASObject)thisValue;
            while (obj is not null && obj is not ASNativeObject)
                obj = obj.Super;
            
            nativeObject = ((ASNativeObject)obj).Instance;
        }
        
        var @params = _method.GetParameters();
        if (@params.Length == 1 && @params[0].IsDefined(typeof(ParamArrayAttribute), true))
            return _method.Invoke(nativeObject, new object[] { args });
        else
            return _method.Invoke(nativeObject, args);
    }
}
