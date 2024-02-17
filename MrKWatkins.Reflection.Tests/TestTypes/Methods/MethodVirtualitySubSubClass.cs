namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public abstract class MethodVirtualitySubSubClass : MethodVirtualitySubClass
{
    public new void New()
    {
    }

    public new void NewSubSubClass()
    {
    }
}