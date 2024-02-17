namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public abstract class MethodVirtuality
{
    public void Normal()
    {
    }

    public abstract void Abstract();

    public virtual void Virtual()
    {
    }

    public abstract void Override();

    public abstract void SealedOverride();

    public void New()
    {
    }

    public void NewAbstract()
    {
    }

    public void NewVirtual()
    {
    }

    public void NewSubSubClass()
    {
    }
}