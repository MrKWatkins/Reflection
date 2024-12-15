namespace MrKWatkins.Reflection.Tests.TestTypes.Properties;

[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public abstract class PropertyModifiers
{
    public int Normal { get; set; }

    public static int Static { get; set; }

    public int InitSetter { get; init; }

    public required int Required { get; init; }
}