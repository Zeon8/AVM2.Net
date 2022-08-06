using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVM2.Tests.Native;

public class TestClass
{
    public int TestProperty = 1;

    public static int TestStaticProperty = 1;

    public static void TestStatic(){}

    public static int TestStaticWithReturn(int number) => number;

    public void Test(){}

    public int TestWithReturn(int number) => number;
}

public class TestClass2 {
    public int Number;

    public TestClass2(int number)
    {
        Number = number;
    }
}
