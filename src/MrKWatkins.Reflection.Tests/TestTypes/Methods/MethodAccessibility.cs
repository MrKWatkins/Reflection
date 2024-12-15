namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class MethodAccessibility
{
    private static void Private() => throw new NotSupportedException();

    private protected static void PrivateProtected() => throw new NotSupportedException();

    internal static void Internal() => throw new NotSupportedException();

    protected internal static void ProtectedInternal() => throw new NotSupportedException();

    protected static void Protected() => throw new NotSupportedException();

    public static void Public() => throw new NotSupportedException();
}