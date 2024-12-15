namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed class MethodNullableReferenceTypes
{
    public static string? NullableReturn() => null;

    public static string NonNullableReturn() => "";
}