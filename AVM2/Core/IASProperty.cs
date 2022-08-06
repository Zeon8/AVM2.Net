namespace AVM2.Core;

public interface IASProperty : IASTrait
{
    object GetValue(IASObject obj);
    void SetValue(IASObject obj, object value);
}