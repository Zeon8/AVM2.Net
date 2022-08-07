using System.Reflection;
using AVM2;
using AVM2.Core;
using AVM2.Core.Interpreted;
using Flazzy.ABC;

namespace AVM2.Native;
public static class ASObjectExstensions
{

    public static T ToNative<T>(this ASObject asObject)
    {
        var interpretedClass = (ASInterpretedClass)asObject.Class;
        /*var dynamicType = new DynamicType(interpretedClass.Class, typeof(T));
        Type type = dynamicType.Compile();*/
        var dynamicEmit = new DynamicEmit(interpretedClass.Class, typeof(T));
        Type type = dynamicEmit.Build();
        var instance = Activator.CreateInstance(type);
        type.GetField("_instance",BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance,asObject);
        return (T)instance;
    }
}
