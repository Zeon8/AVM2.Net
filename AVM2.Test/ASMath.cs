namespace AVM2.Test;

[CustomName("Math")]
public class ASMath
{
    public static double Floor(double value) => Math.Floor(value);

    public static double Random() => new Random().NextDouble();
}
