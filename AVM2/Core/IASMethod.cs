namespace AVM2.Core;

public interface IASMethod : IASTrait
{
    object Invoke(ASObject thisValue, params object[] args);
}
