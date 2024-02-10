namespace MrKWatkins.Reflection.Tests.TestTypes.Properties;

[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public abstract class PropertyVirtuality
{
    public int Normal { get; }

    public abstract int Abstract { get; }

    public virtual int Virtual { get; }

    public abstract int Override { get; }

    public abstract int SealedOverride { get; }

    public int New { get; }

    public int NewAbstract { get; }

    public int NewVirtual { get; }

    public int NewSubSubClass { get; }
}