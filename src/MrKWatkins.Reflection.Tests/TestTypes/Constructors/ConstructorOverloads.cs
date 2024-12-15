namespace MrKWatkins.Reflection.Tests.TestTypes.Constructors;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class ConstructorOverloads
{
    public ConstructorOverloads(int _)
    {
    }

    protected ConstructorOverloads(string _)
    {
    }

    private ConstructorOverloads(int _, string __)
    {
    }
}