namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public abstract class MethodVirtualitySubClass : MethodVirtuality
{
    public override void Override()
    {
    }

    public sealed override void SealedOverride()
    {
    }

    public new void New()
    {
    }

    public new abstract void NewAbstract();

    public new virtual void NewVirtual()
    {
    }
}