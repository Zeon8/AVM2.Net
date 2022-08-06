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
        var result = (ASNativeObject)_class.GetMethod("construct").Invoke(null);
        Assert.IsNotNull(result.Instance);
    }

    [TestMethod]
    public void Construct_Width_Params()
    {
        ASNativeObject instance = (ASNativeObject)_class.GetMethod("constructWithParams").Invoke(null, 2);
        TestClass2 testClass = (TestClass2)instance.Instance;
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

    [TestMethod]
    public void Get_Static_Property()
    {
        var value = _class.GetMethod("getStaticProperty").Invoke(null);
        Assert.AreEqual(TestClass.TestStaticProperty,value);
    }

    [TestMethod]
    public void Get_Property()
    {
        var testClass = new TestClass();
        var value = _class.GetMethod("getStaticProperty").Invoke(null,testClass);
        Assert.AreEqual(testClass.TestProperty,value);
    }

    [TestMethod]
    public void Set_Static_Property()
    {
        _class.GetMethod("setStaticProperty").Invoke(null,2);
        Assert.AreEqual(2, TestClass.TestStaticProperty);
    }

    [TestMethod]
    public void Set_Property()
    {
        _class.GetMethod("setProperty").Invoke(null,_testClass,2);
        var native = (ASNativeObject)_testClass;
        var testClass = (TestClass)native.Instance;
        Assert.AreEqual(2, testClass.TestProperty);
    }
}
