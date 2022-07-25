using System.Linq;
using AVM2.Core;
using Flazzy;
using Flazzy.Tags;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVM2.Tests;

[TestClass]
public class ClassTests 
{
    private ASBaseClass _class;

    [TestInitialize]
    public void Setup()
    {
        var runtime = ASTest.LoadRuntime();
        _class = runtime.GetClass("ClassTests");
    }

    [TestMethod]
    public void Construct_Test()
    {
        var obj = _class.GetMethod("constructTest").Invoke(null);
        Assert.IsInstanceOfType(obj,typeof(ASObject));
    }

    private void CallAndAssertValue(string methodName, int value, int expectedValue)
    {
        var obj = _class.GetMethod(methodName).Invoke(null, value);
        Assert.AreEqual(expectedValue, obj);
    }

    [TestMethod]
    [DataRow(11)]
    public void Construct_Width_Args(int value) => CallAndAssertValue("constructWithArgs", value, value);

    [TestMethod]
    [DataRow(11)]
    public void Construct_Super_Width_Args(int value) => CallAndAssertValue("constructSuperWithArgs", value, value);

    [TestMethod]
    [DataRow(11)]
    public void Call_Method(int value) => CallAndAssertValue("callMethod", value, value);

    [TestMethod]
    [DataRow(11)]
    public void Call_Super_Method(int value) => CallAndAssertValue("callSuperMethod", value, value);
    
    [TestMethod]
    [DataRow(11)]
    public void Call_Overrided_Method(int value) => CallAndAssertValue("callOverridedMethod", value, value+1);
    
    [TestMethod]
    [DataRow(11)]
    public void Call_Overrided_Method_Calls_Super(int value) => CallAndAssertValue("callOverridedMethodCallsSuper", value, value*2);

    [TestMethod]
    [DataRow(11)]
    public void Call_Static_Method(int value) => CallAndAssertValue("callStaticMethod", value, value);
}
