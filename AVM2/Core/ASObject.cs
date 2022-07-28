namespace AVM2.Core;

public class ASObject : IASObject
{
    public object NativeInstance { get; }

    public ASBaseClass Class { get; }

    public ASObject Super { get; set; }

    private Dictionary<string, object> _this = new();

    public ASObject(){}

    internal ASObject(ASBaseClass @class, object instance = null)
    {
        Class = @class;
        NativeInstance = instance;
    }

    public object this[string propertyName]
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
