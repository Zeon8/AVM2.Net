using System.Diagnostics;

namespace AVM2.Core;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public abstract class ASBaseClass : IASObject
{
    public abstract QName QName { get; }
    public abstract IASMethod[] Methods { get; }
    public abstract IASProperty[] Properties { get; }
    public abstract ASBaseClass BaseClass { get; }

    public abstract ASObject Construct(params object[] args);

    object IASObject.this[string propertyName] 
    { 
        get => GetProperty(propertyName)?.GetValue(null);
        set => GetProperty(propertyName)?.SetValue(null, value);
    }

    public abstract bool IsAssignableTo(ASBaseClass @class);

    public IASMethod GetMethod(string name)
    {
        var method = Methods.FirstOrDefault(m => m.Name == name);
        if (method is null && BaseClass is not null)
            method = BaseClass.GetMethod(name);
        return method;
    }

    public IASProperty GetProperty(string name)
    {
        var field = Properties.FirstOrDefault(field => field.Name == name);
        if (field is null && BaseClass is not null)
            field = BaseClass.GetProperty(name);
        return field;
    }

    object IASObject.Invoke(string name, params object[] args)
    {
        IASMethod method = GetMethod(name);
        if (method is null)
            throw new AVM2Exception(QName.Name + " has no method " + name);
        return method.Invoke(null, args);
    }

    private string GetDebuggerDisplay() => QName.Name;
}