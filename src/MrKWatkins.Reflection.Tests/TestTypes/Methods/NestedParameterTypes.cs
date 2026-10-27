namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

public static class NestedParameterTypes
{
    public delegate void Handler();

    public sealed class Child;

    public static void UseHandler(Handler handler) { }

    public static void UseChild(Child child) { }

    public static void UseArray(Handler[] handlers) { }

    public static void UseContainer(IReadOnlyList<Handler> handlers) { }

    public static void UseReference(ref Handler handler) { }
}