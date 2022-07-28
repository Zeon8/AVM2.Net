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


    public object Invoke(ASObject thisValue, params object[] args)
    {
        object instance = null;
        if (thisValue is not null)
        {
            instance = thisValue.NativeInstance;
            while (instance is null && thisValue is not null)
            {
                thisValue = thisValue.Super;
                instance = thisValue.NativeInstance;
            }
        }
        var @params = _method.GetParameters();
        if (@params.Length == 1 && @params[0].IsDefined(typeof(ParamArrayAttribute), true))
            return _method.Invoke(instance, new object[] { args });
        else
            return _method.Invoke(instance, args);
    }
}
