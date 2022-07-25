namespace AVM2;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public class CustomNameAttribute : Attribute
{
    public string CustomName { get; }

    public CustomNameAttribute(string customName) => CustomName = customName;
}
