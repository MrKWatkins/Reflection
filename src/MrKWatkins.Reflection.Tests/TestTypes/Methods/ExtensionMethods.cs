namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

public static class ExtensionMethods
{
    public static void Normal(ConsoleColor _)
    {
    }

    public static void Extension(this ConsoleColor _)
    {
    }
}