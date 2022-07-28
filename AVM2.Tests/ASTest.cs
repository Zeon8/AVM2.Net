using System;
using System.Linq;
using AVM2.Core;
using AVM2.Tests.Native;
using Flazzy;
using Flazzy.Tags;

namespace AVM2.Tests;

public class ASTest
{
    public static ASRuntime LoadRuntime()
    {
        var runtime = new ASRuntime();
        runtime.RegisterType(typeof(TestClass));
        runtime.RegisterType(typeof(TestClass2));

        var swf = new ShockwaveFlash("../../../HaxeProject/bin/HaxeProject.swf");
        swf.Disassemble();
        var doAbcTag = (DoABCTag)swf.Tags.First(tag => tag is DoABCTag);
        runtime.Load(doAbcTag.ABCData);
        
        return runtime;
    }
}