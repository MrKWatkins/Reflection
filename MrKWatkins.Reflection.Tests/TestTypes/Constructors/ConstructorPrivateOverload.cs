namespace MrKWatkins.Reflection.Tests.TestTypes.Constructors;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed class ConstructorPrivateOverload
{
    public ConstructorPrivateOverload()
    {
    }

    private ConstructorPrivateOverload(int _)
    {
    }
}