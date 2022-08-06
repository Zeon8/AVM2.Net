using System.Collections.ObjectModel;
using AVM2.Core;
using AVM2.Core.Interpreted;
using AVM2.Core.Native;
using Flazzy.ABC;

namespace AVM2;

public class ASRuntime
{
    private readonly List<ASBaseClass> _classes = new();

    public ASBaseClass[] Classes => _classes.ToArray();

    public ASRuntime()
    {
        RegisterType(typeof(ASObject), new QName("Object",""));
        RegisterType(typeof(AVM2Exception), new QName("Error",""));
        RegisterType(typeof(ASObject), new QName("Boot","flash"));
    }

    public ASBaseClass GetClass(Type type) => _classes.FirstOrDefault(klass => klass is ASNativeClass nativeClass && nativeClass.Type == type);
    public ASBaseClass GetClass(string name, string @namespace = "") => GetClass(new QName(name, @namespace));

    public ASBaseClass GetClass(QName qName) => _classes.FirstOrDefault(klass => klass.QName == qName);

    internal ASBaseClass GetClass(ASMultiname name)
    {
        foreach (var klass in _classes)
            if(klass.QName == name)
                return klass;
        return null;
    }

    public void Load(byte[] abcData)
    {
        var file = new ABCFile(abcData);
        var classes = file.Classes
            .Where(klass => klass.QName.Namespace.Name != "flash" && GetClass(klass.QName) is null)
            .Select(klass => new ASInterpretedClass(klass,this))
            .ToArray();
        _classes.AddRange(classes);

        foreach (var script in file.Scripts)
            if(script.QName.Namespace.Name != "flash")
            {
                var asMethod = new ASInterpretedMethod(script.Initializer, this);
                asMethod.Invoke(null);
            }
    }

    public void HotReload(byte[] abcData)
    {
        var file = new ABCFile(abcData);
        foreach (var klass in file.Classes)
        {
            if (GetClass(klass.QName) is ASInterpretedClass interpretedClass)
            {
                foreach (var method in klass.GetMethods())
                {
                    if (interpretedClass.GetMethod(method.Trait.QName.Name) is ASInterpretedMethod interpretedMethod)
                        interpretedMethod.ReplaceASMethod(method);
                }
            }
        }

    }

    public ASBaseClass RegisterType(Type type, string @namespace = "")
    {
        return RegisterType(type, new QName(type.Name, @namespace));
    }

    public ASBaseClass RegisterType(Type type, QName qName)
    {
        var customClass = new ASNativeClass(type,qName,this);
        _classes.Add(customClass);
        return customClass;
    }
}
