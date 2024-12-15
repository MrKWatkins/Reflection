namespace MrKWatkins.Reflection.Tests.TestTypes.Properties;

[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public abstract class PropertyIndexerVirtuality
{
    public int this[byte normal] => throw new NotSupportedException();

    public abstract int this[short @abstract] { get; }

    public virtual int this[ushort @virtual] => throw new NotSupportedException();

    public abstract int this[int @override] { get; }

    public abstract int this[uint sealedOverride] { get; }

    public int this[long @new] => throw new NotSupportedException();

    public int this[ulong newAbstract] => throw new NotSupportedException();

    public int this[string newVirtual] => throw new NotSupportedException();
}