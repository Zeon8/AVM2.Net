using System.Reflection;

namespace AVM2.Core.Native;

internal class ASNativeClass : ASBaseClass
{
    public override QName QName { get; }

    public override IASMethod[] Methods { get; }

    public override IASProperty[] Properties { get; }

    public override ASBaseClass BaseClass { get; }

    public Type Type { get; }

    public ASNativeClass(Type type, QName qName, ASBaseClass baseClass)
    {
        QName = qName;
        Type = type;
        
        Methods = Type.GetMethods().Select(method => new ASNativeMethod(method)).ToArray();
        Properties = Type.GetFields().Select(field => new ASNativeField(field)).ToArray();
        Properties = Properties.Union(Type.GetProperties().Select(property => new ASNativeProperty(property))).ToArray();
        BaseClass = baseClass;
    }

    public override ASObject Construct(params object[] args)
    {
        return new ASObject(this, Activator.CreateInstance(Type, args));
    }

    public override bool IsAssignableTo(ASBaseClass @class)
    {
        var nativeClass = (ASNativeClass)@class;
        return nativeClass.Type.IsAssignableFrom(Type);
    }
}
