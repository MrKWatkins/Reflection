namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class MethodProtectedOverload
{
    public static void Overload()
    {
    }

    protected static void Overload(int _)
    {
    }
}