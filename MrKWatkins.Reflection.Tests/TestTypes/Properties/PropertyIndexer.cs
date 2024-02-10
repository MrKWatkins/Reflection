namespace MrKWatkins.Reflection.Tests.TestTypes.Properties;

[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed class PropertyIndexer
{
    public int Normal { get; set; }

    public int this[int _] => 0;
}