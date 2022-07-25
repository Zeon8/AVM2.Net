using AVM2.Core;
using AVM2.Core.Interpreted;
using AVM2.Core.Native;
using Flazzy.ABC;

namespace AVM2;

public class ASRuntime
{
    private readonly List<ASBaseClass> _classes = new();
    public ASBaseClass GetClass(string name, string namepsace = "") => _classes.FirstOrDefault(klass => klass.Name == name && klass.Namespace == namepsace);
    private const string IgnoredNamespace = "flash";

    public ASBaseClass GetClass(ASMultiname name)
    {
        foreach (var klass in _classes)
            if(klass.Name == name.Name && klass.Namespace == name.Namespace.Name)
                return klass;
        return null;
    }

    public void Load(byte[] abcData)
    {
        var file = new ABCFile(abcData);
        var classes = file.Classes
            .Where(klass => klass.QName.Namespace.Name != IgnoredNamespace && GetClass(klass.QName) is null)
            .Select(klass => new ASInterpretedClass(klass, this))
            .ToArray();
        _classes.AddRange(classes);

        foreach (var @class in classes)
            @class.CallInitializer();
    }

    public void RegisterType(Type type,string @namespace = "")
    {
        var klass = _classes.FirstOrDefault(klass => klass.Namespace == @namespace && klass.Name == type.Name);
        if(klass is not null)
            _classes.Remove(klass);
        
        var customClass = new ASNativeClass(type,@namespace);
        _classes.Add(customClass);
    }
}
