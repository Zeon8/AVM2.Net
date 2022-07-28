using System.Reflection;
using AVM2.Core.Interpreted;

namespace AVM2.Core.Native;

internal class ASNativeClass : ASBaseClass
{
    public override QName QName { get; }

    public override IASMethod[] Methods { get; }

    public override IASProperty[] Properties { get; }

    public override ASBaseClass BaseClass { get; }

    public Type Type { get; }

    public override bool IsInterface => Type.IsInterface;

    private ASRuntime _runtime;

    public ASNativeClass(Type type, QName qName, ASRuntime runtime)
    {
        QName = qName;
        Type = type;
        Methods = Type.GetMethods().Select(method => new ASNativeMethod(method)).ToArray();
        Properties = Type.GetFields().Select(field => new ASNativeField(field)).ToArray();
        Properties = Properties.Union(Type.GetProperties().Select(property => new ASNativeProperty(property))).ToArray();
        BaseClass = runtime.Classes.FirstOrDefault(klass => klass is ASNativeClass nativeClass && nativeClass.Type == type.BaseType);
        _runtime = runtime;
    }

    public override ASObject Construct(params object[] args)
    {
        return new ASObject(this, Activator.CreateInstance(Type, args));
    }

    public override bool IsAssignableTo(ASBaseClass @class)
    {
        if(@class is ASNativeClass nativeClass)
            return nativeClass.Type.IsAssignableFrom(Type);
        return false;
    }

    public override ASBaseClass[] GetInterfaces()
    {
        return Type.GetInterfaces()
            .Select(type => _runtime.GetClass(type))
            .ToArray();
    }
}
