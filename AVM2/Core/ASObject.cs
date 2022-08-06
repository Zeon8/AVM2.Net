namespace AVM2.Core;

public class ASObject : IASObject
{
    public ASBaseClass Class { get; }

    public ASObject Super { get; private set; }

    private Dictionary<string, object> _this = new();

    public ASObject(){}

    internal ASObject(ASBaseClass @class) => Class = @class;

    public void ConstructSuper(params object[] args)
    {
        Super = Class.BaseClass.Construct(args);
    }

    public virtual object this[string propertyName]
    {
        get 
        {
            if(_this.ContainsKey(propertyName))
                return _this[propertyName];
            if(Super is not null)
                return Super[propertyName];
            return null;
        }
        set => _this[propertyName] = value;
    }

    public object Invoke(string name, params object[] args)
    {
        return Class.GetMethod(name)?.Invoke(this, args);
    }
}
