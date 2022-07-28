using Flazzy.ABC;

namespace AVM2.Core;

public struct QName
{
    public string Name { get; }

    public string Namespace { get; }

    public QName(string name, string @namespace = "")
    {
        Name = name;
        Namespace = @namespace;
    }

    public QName(ASMultiname asName)
    {
        Name = asName.Name;
        Namespace = asName.Namespace.Name;
    }

    public static bool operator ==(QName a, QName b) => a.Equals(b);
    public static bool operator !=(QName a, QName b) => !a.Equals(b);
    
    public static bool operator ==(QName a, ASMultiname b)
    {
        var @namespace = b.Namespace?.Name ?? "";
        return a.Name == b.Name && a.Namespace == @namespace;
    }

    public static bool operator !=(QName a, ASMultiname b) => !(a == b);

    public override bool Equals(object obj)
    {
        return obj is QName name &&
               Name == name.Name &&
               Namespace == name.Namespace;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Namespace);
    }
}
