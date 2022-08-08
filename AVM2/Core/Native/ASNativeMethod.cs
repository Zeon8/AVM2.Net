using System.Reflection;

namespace AVM2.Core.Native;

internal class ASNativeMethod : IASMethod
{
    private readonly MethodBase _method;

    public string Name { get; }

    private ASRuntime _runtime;

    public bool IsStatic { get; }

    public ASNativeMethod(MethodBase method, ASRuntime runtime)
    {
        _method = method;
        IsStatic = method.IsStatic;

        var attribute = method.GetCustomAttribute<CustomNameAttribute>();
        var name = method.Name[0].ToString().ToLower() + method.Name[1..];
        Name = attribute?.CustomName ?? name;
        _runtime = runtime;
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

        object[] nativeArgs = UnwrapParameters(args);

        var @params = _method.GetParameters();
        if (@params.Length == 1 && @params[0].IsDefined(typeof(ParamArrayAttribute), true))
            return InvokeMethod(nativeObject, new object[] { nativeArgs });
        return InvokeMethod(nativeObject, nativeArgs);
    }

    private object InvokeMethod(object nativeObject, object[] nativeArgs)
    {
        var result = _method.Invoke(nativeObject, nativeArgs);
        if(result is not object[] && result is not null && !result.GetType().IsPrimitive)
            return new ASNativeObject(_runtime.GetClass(result.GetType()), result);
        return result;
    }

    /// Unwraps parameters, for example ASNativeObject(string) becomes string
    private object[] UnwrapParameters(object[] parameters)
    {
        var nativeParameters = new List<object>();
        foreach (var parameter in parameters)
        {
            if(parameter is ASNativeObject nativeObject)
                nativeParameters.Add(nativeObject.Instance);
            else
                nativeParameters.Add(parameter);
        }
        return nativeParameters.ToArray();
    }
}
