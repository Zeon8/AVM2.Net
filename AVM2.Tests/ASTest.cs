using System;
using System.Linq;
using AVM2.Core;
using Flazzy;
using Flazzy.Tags;

namespace AVM2.Tests;

public class ASTest
{
    public static ASRuntime LoadRuntime(Action<ASRuntime> onRegister = null)
    {
        var runtime = new ASRuntime();
        onRegister?.Invoke(runtime);
        var swf = new ShockwaveFlash("../../../HaxeProject/bin/HaxeProject.swf");
        swf.Disassemble();
        var doAbcTag = (DoABCTag)swf.Tags.First(tag => tag is DoABCTag);
        runtime.Load(doAbcTag.ABCData);
        return runtime;
    }
}