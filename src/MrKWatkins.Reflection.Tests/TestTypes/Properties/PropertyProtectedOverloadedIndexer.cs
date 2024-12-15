namespace MrKWatkins.Reflection.Tests.TestTypes.Properties;

[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class PropertyProtectedOverloadedIndexer
{
    public int this[int _] => 0;

    protected int this[string _] => 0;
}