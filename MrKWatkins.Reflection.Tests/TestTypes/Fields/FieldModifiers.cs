namespace MrKWatkins.Reflection.Tests.TestTypes.Fields;

#pragma warning disable CA1051
#pragma warning disable CA2211
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class FieldModifiers
{
    public const int Const = 0;

    public int Instance;

    public readonly int InstanceReadOnly;

    public static int Static;

    public static readonly int StaticReadOnly;
}
#pragma warning restore CA2211
#pragma warning restore CA1051