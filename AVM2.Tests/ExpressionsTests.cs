using AVM2.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVM2.Tests;

[TestClass]
public class ExpressionsTests
{
    private ASBaseClass _class;

    [TestInitialize]
    public void Setup()
    {
        var runtime = ASTest.LoadRuntime();
        _class = runtime.GetClass("ExpressionsTests");
    }

    private void TestCase(string methodName,object a, object b, bool expected)
    {
        var result = _class.GetMethod(methodName).Invoke(null, a, b);
        Assert.AreEqual(expected,result);
    }

    [TestMethod]
    [DataRow(1,1,true)]
    [DataRow(1,2,false)]
    [DataRow(1,null,false)]
    [DataRow(null,null,true)]
    [DataRow("a","a",true)]
    [DataRow("a","b",false)]
    public void Equality(object a, object b, bool expected)
    {
        TestCase("equels",a,b,expected);
    }

    [TestMethod]
    [DataRow(2,1,true)]
    [DataRow(1,1,false)]
    [DataRow(1,2,false)]
    public void Greather_Than(object a, object b, bool expected)
    {
        TestCase("greaterThan",a,b,expected);
    }

    [TestMethod]
    [DataRow(1,1,true)]
    public void Greather_Equals_Than(object a, object b, bool expected)
    {
        TestCase("greaterEquelsThan",a,b,expected);
    }

    [TestMethod]
    [DataRow(1,2,true)]
    [DataRow(1,1,false)]
    [DataRow(2,1,false)]
    public void Less_Than(object a, object b, bool expected)
    {
        TestCase("lessThan",a,b,expected);
    }

    [TestMethod]
    [DataRow(1,1,true)]
    public void Less_Equels_Than(object a, object b, bool expected)
    {
        TestCase("lessEquelsThan",a,b,expected);
    }
}
