using AVM2.Core.Native;
using Flazzy.ABC;

namespace AVM2.Core.Interpreted;

public class ASInterpretedClass : ASBaseClass
{
    public override QName QName { get; }

    public override IASMethod[] Methods { get; }

    public override IASProperty[] Properties { get; }

    private ASBaseClass _baseClass;

    public override ASBaseClass BaseClass => _baseClass;

    public ASClass Class { get; }

    public override bool IsInterface { get; }

    private readonly ASRuntime _runtime;

    public ASInterpretedClass(ASClass @class, ASRuntime runtime)
    {
        Class = @class;
        _runtime = runtime;

        QName = new QName(@class.QName);
        IsInterface = @class.Instance.IsInterface;

        Methods = Class.GetMethods()
            .Union(Class.Instance.GetMethods())
            .Select(m => new ASInterpretedMethod(m, runtime))
            .ToArray();

        Properties = Class.GetTraits(TraitKind.Slot)
            .Union(Class.Instance.GetTraits(TraitKind.Slot))
            .Select(f => new ASInterpretedField(f))
            .ToArray();
    }

    public override ASObject Construct(params object[] args)
    {
        var constructor = new ASInterpretedMethod(Class.Instance.Constructor, _runtime);
        var instance = new ASObject(this);
        constructor.Invoke(instance, args);
        return instance;
    }

    public override bool IsAssignableTo(ASBaseClass @class)
    {
        if (@class.IsInterface)
        {
            bool implements = @class == this || GetInterfaces().Any(@interface => @interface.IsAssignableTo(@class));
            if(implements)
                return true;
        }
        if(IsInterface)
            return false;

        var super = _runtime.GetClass(Class.Instance.Super);
        var implementsInterface = false;
        while (super is not null && super.QName != @class.QName && !implementsInterface)
        {
            if(@class.IsInterface)
                implementsInterface = super.IsAssignableTo(@class);
            super = super.BaseClass;
        }

        return super is not null || implementsInterface;
    }

    internal void SetBaseClass(ASBaseClass baseClass) => _baseClass = baseClass;

    public override ASBaseClass[] GetInterfaces()
    {
        return Class.Instance.GetInterfaces()
            .Select(name => _runtime.GetClass(name))
            .ToArray();
    }
}