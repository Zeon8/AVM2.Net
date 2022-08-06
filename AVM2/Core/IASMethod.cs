namespace AVM2.Core;

public interface IASMethod : IASTrait
{
    object Invoke(IASObject thisValue, params object[] args);
}
