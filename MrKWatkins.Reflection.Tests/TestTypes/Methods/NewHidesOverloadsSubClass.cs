namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class NewHidesOverloadsSubClass : NewHidesOverloads
{
    public new static void Overload()
    {
    }
}