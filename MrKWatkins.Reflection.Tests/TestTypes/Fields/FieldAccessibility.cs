namespace MrKWatkins.Reflection.Tests.TestTypes.Fields;

#pragma warning disable CA1823
#pragma warning disable CS0169
#pragma warning disable CS0649
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class FieldAccessibility
{
    private static readonly int Private;

    private protected static readonly int PrivateProtected;

    internal static readonly int Internal;

    protected internal static readonly int ProtectedInternal;

    protected static readonly int Protected;

    public static readonly int Public;
}
#pragma warning restore CS0649
#pragma warning restore CS0169
#pragma warning restore CA1823