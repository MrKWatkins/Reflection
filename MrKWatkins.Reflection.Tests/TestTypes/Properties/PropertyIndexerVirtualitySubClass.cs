namespace MrKWatkins.Reflection.Tests.TestTypes.Properties;

[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public abstract class PropertyIndexerVirtualitySubClass : PropertyIndexerVirtuality
{
    public override int this[int @override] => throw new NotSupportedException();

    public sealed override int this[uint sealedOverride] => throw new NotSupportedException();

    public new int this[long @new] => throw new NotSupportedException();

    public new abstract int this[ulong newAbstract] { get; }

    public new virtual int this[string newVirtual] => throw new NotSupportedException();
}