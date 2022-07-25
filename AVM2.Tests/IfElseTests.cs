using AVM2.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVM2.Tests;

[TestClass]
public class IfElseTests
{
    private ASBaseClass _class;

    [TestInitialize]
    public void Setup()
    {
        var runtime = ASTest.LoadRuntime();
        _class = runtime.GetClass("IfElseTests");
    }

    [TestMethod]
    [DataRow(true,"true")]
    [DataRow(false,"false")]
    public void Simple_Condition(bool a, string expected)
    {
        var result = _class.GetMethod("ifTest").Invoke(null, a);
        Assert.AreEqual(expected,result);
    }

    public void TestCondition(string methodName, bool a, bool b, string expected)
    {
        var result = _class.GetMethod(methodName).Invoke(null, a,b);
        Assert.AreEqual(expected,result);
    }

    [TestMethod]
    [DataRow(true,true,"true")]
    [DataRow(true,false,"false")]
    [DataRow(false,true,"false")]
    [DataRow(false,false,"false")]
    public void And_Condition(bool a, bool b, string expected)
    {
        TestCondition("andCondition", a, b, expected);
    }

    [TestMethod]
    [DataRow(true,true,"true")]
    [DataRow(true,false,"true")]
    [DataRow(false,true,"true")]
    [DataRow(false,false,"false")]
    public void Or_Condition(bool a, bool b, string expected)
    {
        TestCondition("orCondition", a, b, expected);
    }

    [TestMethod]
    [DataRow(1,"one")]
    [DataRow(2,"two")]
    [DataRow(3,"other number")]
    public void ElseIf(int num, string expected)
    {
        var result = _class.GetMethod("elseIf").Invoke(null, num);
        Assert.AreEqual(expected,result);
    }


}
