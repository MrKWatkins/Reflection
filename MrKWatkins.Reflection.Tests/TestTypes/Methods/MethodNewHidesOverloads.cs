namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class MethodNewHidesOverloads
{
    public static void Overload()
    {
    }

    public static void Overload(int _)
    {
    }
}