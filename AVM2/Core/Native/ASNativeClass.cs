using System.Reflection;

namespace AVM2.Core.Native;

public class ASNativeClass : ASBaseClass
{
    public override string Name { get; }

    public override string Namespace { get; }

    public override IASMethod[] Methods { get; }

    public override IASProperty[] Properties { get; }

    public override ASBaseClass BaseClass { get; }

    private Type _type;

    public ASNativeClass(Type type, string @namespace = "")
    {
        _type = type;
        Methods = _type.GetMethods().Select(method => new ASNativeMethod(method)).ToArray();
        Properties = _type.GetFields().Select(field => new ASNativeField(field)).ToArray();
        Properties = Properties.Union(_type.GetProperties().Select(property => new ASNativeProperty(property))).ToArray();
        Namespace = @namespace;
        if (_type.BaseType is not null)
            BaseClass = new ASNativeClass(_type.BaseType);

        var attribute = _type.GetCustomAttribute<CustomNameAttribute>();
        Name = attribute?.CustomName ?? _type.Name;
    }

    public override ASObject Construct(params object[] args)
    {
        return new ASObject(this, Activator.CreateInstance(_type, args));
    }

    public override bool IsAssignableTo(ASBaseClass @class)
    {
        var nativeClass = (ASNativeClass)@class;
        return nativeClass._type.IsAssignableFrom(_type);
    }
}
