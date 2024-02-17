namespace MrKWatkins.Reflection.Tests.TestTypes.Types;

public static class PublicAccessibility;

internal static class InternalAccessibility;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class NestedAccessibility
{
    [SuppressMessage("ReSharper", "UnusedType.Local")]
    private static class Private;

    private protected static class PrivateProtected;

    internal static class Internal;

    protected internal static class ProtectedInternal;

    protected static class Protected;

    public static class Public;
}