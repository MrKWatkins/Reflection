namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed class MethodPrivateOverload
{
    public static void Overload()
    {
    }

    private static void Overload(int _)
    {
    }
}