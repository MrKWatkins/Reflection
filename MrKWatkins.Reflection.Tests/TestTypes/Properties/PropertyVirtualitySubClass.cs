namespace MrKWatkins.Reflection.Tests.TestTypes.Properties;

[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public abstract class PropertyVirtualitySubClass : PropertyVirtuality
{
    public override int Override { get; }

    public sealed override int SealedOverride { get; }

    public new int New { get; }

    public new abstract int NewAbstract { get; }

    public new virtual int NewVirtual { get; }
}