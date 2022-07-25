using Flazzy.ABC;

namespace AVM2.Core.Interpreted;

public class ASInterpretedClass : ASBaseClass
{
    public override string Name => _class.QName.Name;

    public override string Namespace => _class.QName.Namespace.Name;

    public override IASMethod[] Methods { get; }

    public override IASProperty[] Properties { get; }

    public override ASBaseClass BaseClass => _runtime.GetClass(_class.Instance.Super);

    private ASClass _class;

    private readonly ASRuntime _runtime;

    private const int InitializerStartInstructionIndex = 9;

    public ASInterpretedClass(ASClass @class, ASRuntime runtime)
    {
        _class = @class;
        _runtime = runtime;

        Methods = _class.GetMethods()
            .Union(_class.Instance.GetMethods())
            .Select(m => new ASInterpretedMethod(m, runtime))
            .ToArray();

        Properties = _class.GetTraits(TraitKind.Slot)
            .Union(_class.Instance.GetTraits(TraitKind.Slot))
            .Select(f => new ASInterpretedField(f))
            .ToArray();

    }

    internal void CallInitializer()
    {
        ABCFile abcFile = _class.ABC;
        ASScript script = abcFile.Scripts.First(script => script.QName == _class.QName);
        var method = script.Initializer;
        var asCode = new ASCode(abcFile, method.Body);
        ASInterpretedMethod.Execute(asCode, new ASMachine(0, _runtime), InitializerStartInstructionIndex);
    }

    public override ASObject Construct(params object[] args)
    {
        var constructor = new ASInterpretedMethod(_class.Instance.Constructor, _runtime);
        var instance = new ASObject(this);
        constructor.Invoke(instance, args);
        return instance;
    }

    public override bool IsAssignableTo(ASBaseClass @class)
    {
        var super = _runtime.GetClass(_class.Instance.Super);
        while (super is not null && super.Name != @class.Name && @class.Namespace != super.Namespace)
            super = _runtime.GetClass(super.BaseClass.Name, super.BaseClass.Namespace);
        return super is not null;
    }
}