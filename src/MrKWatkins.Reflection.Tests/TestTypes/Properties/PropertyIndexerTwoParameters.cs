namespace MrKWatkins.Reflection.Tests.TestTypes.Properties;

[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed class PropertyIndexerTwoParameters
{
    public int this[int _, string __] => 0;
}