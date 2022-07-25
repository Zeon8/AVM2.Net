using System.Collections.Generic;
using AVM2.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVM2.Tests;

[TestClass]
public class ObjectsTests
{
    private ASBaseClass _class;

    [TestInitialize]
    public void Setup()
    {
        var runtime = ASTest.LoadRuntime();
        _class = runtime.GetClass("ObjectsTests");
    }

    [TestMethod]
    public void Create_Array()
    {
        var result = _class.GetMethod("createArray").Invoke(null);
        Assert.IsInstanceOfType(result, typeof(object[]));
    }

    [TestMethod]
    public void Get_Array_Element()
    {
        var result = _class.GetMethod("getArrayElement").Invoke(null);
        Assert.AreEqual(result, 1);
    }

    [TestMethod]
    public void Set_Array_Element()
    {
        var result = _class.GetMethod("setArrayElement").Invoke(null);
        Assert.AreEqual(result, 2);
    }
    

    [TestMethod]
    public void Create_Object()
    {
        var result = _class.GetMethod("createObject").Invoke(null);
        Assert.IsInstanceOfType(result, typeof(ASObject));
    }

    [TestMethod]
    public void Get_Object_Value()
    {
        var result = _class.GetMethod("getObjectValue").Invoke(null);
        Assert.AreEqual(1,result);
    }

    [TestMethod]
    public void Set_Object_Value()
    {
        var result = _class.GetMethod("setObjectValue").Invoke(null,2);
        Assert.AreEqual(2,result);
    }
}
