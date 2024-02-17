namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class MethodOverloads
{
    public void Overload(int _)
    {
    }

    protected void Overload(string _)
    {
    }

    private void Overload(int _, string __)
    {
    }
}