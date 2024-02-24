namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class NewHidesOverloads
{
    public static void Overload()
    {
    }

    public static void Overload(int _)
    {
    }
}