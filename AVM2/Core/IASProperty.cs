namespace AVM2.Core;

public interface IASProperty : IASTrait
{
    object GetValue(ASObject obj);
    void SetValue(ASObject obj, object value);
}