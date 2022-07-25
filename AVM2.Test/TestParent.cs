namespace AVM2.Test;

public class TestParent
{
    private readonly string _value;
    public TestParent(string value) => _value = value;

    public void Print() => Console.WriteLine(_value);
}
