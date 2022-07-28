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

    private readonly ASRuntime _runtime;

    public ASInterpretedClass(ASClass @class, ASRuntime runtime)
    {
        Class = @class;
        _runtime = runtime;
        
        QName = new QName(@class.QName);

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
        var super = _runtime.GetClass(Class.Instance.Super);
        while (super is not null && super.QName != @class.QName)
            super = _runtime.GetClass(super.BaseClass.QName);
        return super is not null;
    }

    internal void SetBaseClass(ASBaseClass baseClass) => _baseClass = baseClass;
}