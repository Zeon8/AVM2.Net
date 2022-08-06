using AVM2.Core;
using AVM2.Tests.Native;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVM2.Tests;

[TestClass]
public class NativeBaseCallTests
{
    private ASBaseClass _class;

    [TestInitialize]
    public void Setup()
    {
        var runtime = ASTest.LoadRuntime();
        _class = runtime.GetClass("NativeSuperCallTests");
    }

    [TestMethod]
    public void Construct()
    {
        var obj = (ASObject)_class.GetMethod("construct").Invoke(null);
        Assert.IsNotNull(obj.Super);
    }

    [TestMethod]
    public void Construct_Width_Params()
    {
        var obj = (ASObject)_class.GetMethod("constructWithParams").Invoke(null,1);
        var superNative = (ASNativeObject)obj.Super;
        var testClass = (TestClass2)superNative.Instance;
        Assert.AreEqual(1, testClass.Number);
    }

    [TestMethod]
    public void Call_Method() => _class.GetMethod("callMethod").Invoke(null);

    [TestMethod]
    public void Call_Base_Method() => _class.GetMethod("callSuperMethod").Invoke(null);
}
