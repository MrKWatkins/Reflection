namespace MrKWatkins.Reflection.Tests.TestTypes.Events;

#pragma warning disable CS0414
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class EventAccessibility
{
    public event EventHandler Public = null!;

    protected event EventHandler Protected = null!;

    protected internal event EventHandler ProtectedInternal = null!;

    private protected event EventHandler PrivateProtected = null!;

    private event EventHandler Private = null!;
}
#pragma warning restore CS0414