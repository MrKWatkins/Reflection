namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class MethodAccessibility
{
    private static void Private()
    {
    }

    private protected static void PrivateProtected()
    {
    }

    internal static void Internal()
    {
    }

    protected internal static void ProtectedInternal()
    {
    }

    protected static void Protected()
    {
    }

    public static void Public()
    {
    }
}