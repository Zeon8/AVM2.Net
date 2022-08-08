using System.Diagnostics;
using System.IO.Compression;
using AVM2;
using AVM2.Core;
using AVM2.Native;
using AVM2.Test;
using Flazzy;
using Flazzy.ABC;
using Flazzy.Tags;

var interpreter = new ASRuntime();
interpreter.RegisterType(typeof(ASMath), new QName("Math",""));
interpreter.RegisterType(typeof(Log),"haxe");
interpreter.RegisterType(typeof(Printer));

var swf = new ShockwaveFlash(args[0]);
swf.Disassemble();

foreach (TagItem tag in swf.Tags)
    if (tag is DoABCTag doABCTag && doABCTag.Name != "haxe")
        interpreter.Load(doABCTag.ABCData);
interpreter.GetClass("Main").GetMethod("main").Invoke(null);
