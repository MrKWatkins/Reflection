namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class MethodNewHidesOverloadsSubClass : MethodNewHidesOverloads
{
    public new static void Overload()
    {
    }
}