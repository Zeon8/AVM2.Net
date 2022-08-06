namespace AVM2.Core;

public class ASNativeObject : ASObject
{
    public object Instance { get; }

    public ASNativeObject(ASBaseClass @class, object nativeInstance) : base(@class)
    {
        Instance = nativeInstance;
    }

    public override object this[string propertyName]
    {
        get => Class.GetProperty(propertyName).GetValue(this);
        set => Class.GetProperty(propertyName).SetValue(this,value);
    }
}
