namespace AVM2.Core;

public class ASInstanceField
{
    private readonly IASProperty _field;
    private readonly ASObject _instance;

    public ASInstanceField(IASProperty field, ASObject instance)
    {
        _field = field;
        _instance = instance;
    }

    public void GetValue() => _field.GetValue(_instance);
    public void SetValue(object value) => _field.SetValue(_instance, value);
}