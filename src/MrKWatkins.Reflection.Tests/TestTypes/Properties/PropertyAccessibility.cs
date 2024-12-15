namespace MrKWatkins.Reflection.Tests.TestTypes.Properties;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class PropertyAccessibility
{
    public static string PublicGetPublicSet { get; set; } = null!;
    public static string PublicGetProtectedSet { get; protected set; } = null!;
    public static string PublicGetInternalSet { get; internal set; } = null!;
    public static string PublicGetProtectedInternalSet { get; protected internal set; } = null!;
    public static string PublicGetPrivateProtectedSet { get; private protected set; } = null!;
    public static string PublicGetPrivateSet { get; private set; } = null!;
    public static string PublicGetNoSet => null!;

    public static string ProtectedGetPublicSet { protected get; set; } = null!;
    protected static string ProtectedGetProtectedSet { get; set; } = null!;
    protected internal static string ProtectedGetProtectedInternalSet { protected get; set; } = null!;
    protected static string ProtectedGetPrivateProtectedSet { get; private protected set; } = null!;
    protected static string ProtectedGetPrivateSet { get; private set; } = null!;
    protected static string ProtectedGetNoSet => null!;

    public static string InternalGetPublicSet { get; internal set; } = null!;
    internal static string InternalGetInternalSet { get; set; } = null!;
    protected internal static string InternalGetProtectedInternalSet { internal get; set; } = null!;
    internal static string InternalGetPrivateProtectedSet { get; private protected set; } = null!;
    internal static string InternalGetPrivateSet { get; private set; } = null!;
    internal static string InternalGetNoSet => null!;

    public static string ProtectedInternalGetPublicSet { protected internal get; set; } = null!;
    protected internal static string ProtectedInternalGetProtectedSet { get; protected set; } = null!;
    protected internal static string ProtectedInternalGetInternalSet { get; internal set; } = null!;
    protected internal static string ProtectedInternalGetProtectedInternalSet { get; set; } = null!;
    protected internal static string ProtectedInternalGetPrivateProtectedSet { get; private protected set; } = null!;
    protected internal static string ProtectedInternalGetPrivateSet { get; private set; } = null!;
    protected internal static string ProtectedInternalGetNoSet => null!;

    public static string PrivateProtectedGetPublicSet { private protected get; set; } = null!;
    protected static string PrivateProtectedGetProtectedSet { private protected get; set; } = null!;
    internal static string PrivateProtectedGetInternalSet { private protected get; set; } = null!;
    protected internal static string PrivateProtectedGetProtectedInternalSet { private protected get; set; } = null!;
    private protected static string PrivateProtectedGetPrivateProtectedSet { get; set; } = null!;
    private protected static string PrivateProtectedGetPrivateSet { get; private set; } = null!;
    private protected static string PrivateProtectedGetNoSet => null!;

    public static string PrivateGetPublicSet { get; set; } = null!;
    protected static string PrivateGetProtectedSet { private get; set; } = null!;
    internal static string PrivateGetInternalSet { private get; set; } = null!;
    protected internal static string PrivateGetProtectedInternalSet { private get; set; } = null!;
    private protected static string PrivateGetPrivateProtectedSet { private get; set; } = null!;
    private static string PrivateGetPrivateSet { get; set; } = null!;
    private static string PrivateGetNoSet => null!;

    // ReSharper disable RedundantAssignment
    public static string NoGetPublicSet { set => value = null!; }
    protected static string NoGetProtectedSet { set => value = null!; }
    internal static string NoGetInternalSet { set => value = null!; }
    protected internal static string NoGetProtectedInternalSet { set => value = null!; }
    private protected static string NoGetPrivateProtectedSet { set => value = null!; }
    private static string NoGetPrivateSet { set => value = null!; }
    // ReSharper restore RedundantAssignment
}