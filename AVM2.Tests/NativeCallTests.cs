using AVM2.Core;
using AVM2.Core.Native;
using AVM2.Tests.Native;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVM2.Tests;

[TestClass]
public class NativeCallTests
{
    private ASBaseClass _class;

    private ASObject _testClass;

    [TestInitialize]
    public void Setup()
    {
        var runtime = ASTest.LoadRuntime();
        var testClass = runtime.GetClass("TestClass");
        _testClass = testClass.Construct();

        _class = runtime.GetClass("NativeCallTests");
    }

    [TestMethod]
    public void Call_Static_Method() => _class.GetMethod("callStaticMethod").Invoke(null);
    
    [TestMethod]
    public void Call_Static_Method_Get_Return()
    {
        var result = _class.GetMethod("callStaticMethodWithReturn").Invoke(null, 1);
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void Construct_Native()
    {
        var result = (ASObject)_class.GetMethod("construct").Invoke(null);
        Assert.IsNotNull(result.NativeInstance);
    }

    [TestMethod]
    public void Construct_Width_Params()
    {
        ASObject instance = (ASObject)_class.GetMethod("constructWithParams").Invoke(null, 2);
        TestClass2 testClass = (TestClass2)instance.NativeInstance;
        Assert.AreEqual(2,testClass.Number);
    }

    [TestMethod]
    public void Call_Method() => _class.GetMethod("callMethod").Invoke(null, _testClass);
    
    [TestMethod]
    public void Call_Method_And_With_Return() 
    {
        var result = _class.GetMethod("callMethodAndWithReturn").Invoke(null,_testClass,3);
        Assert.AreEqual(3,result);
    }
}
