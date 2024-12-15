namespace MrKWatkins.Reflection.Tests.TestTypes.Properties;

[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Local")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed class PropertyPrivateOverloadedIndexer
{
    public int this[int _] => 0;

    private int this[string _] => 0;
}