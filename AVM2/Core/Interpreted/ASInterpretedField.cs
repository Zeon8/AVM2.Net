using System.Diagnostics;
using Flazzy.ABC;
using Microsoft.VisualBasic;

namespace AVM2.Core.Interpreted;

public class ASInterpretedField : IASProperty
{
    public string Name { get; }

    public bool IsStatic { get; }

    private readonly ASTrait _field;

    private object _staticValue;

    private static readonly List<ASInterpretedField> _testFields = new();

    public ASInterpretedField(ASTrait field)
    {
        _field = field;
        IsStatic = _field.IsStatic;
        Name = _field.QName.Name;
        _testFields.Add(this);
    }

    public object GetValue(ASObject obj)
    {
        if(IsStatic)
            return _staticValue;
        return obj[Name];
    }

    public void SetValue(ASObject obj, object value)
    {
        if(IsStatic)
            _staticValue = value;
        else if(obj is not null)
            obj[Name] = value;
    }
}
