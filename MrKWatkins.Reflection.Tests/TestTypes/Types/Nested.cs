namespace MrKWatkins.Reflection.Tests.TestTypes.Types;

[SuppressMessage("ReSharper", "UnusedTypeParameter")]
public static class Nested
{
    public static class Child
    {
        public static class GrandChild;
        public static class GrandChild<TG1>;
        public static class GrandChild<TG1, TG2>;
    }

    public static class Child<TC1>
    {
        public static class GrandChild;
        public static class GrandChild<TG1>;
        public static class GrandChild<TG1, TG2>;
    }

    public static class Child<TC1, TC2>
    {
        public static class GrandChild;
        public static class GrandChild<TG1>;
        public static class GrandChild<TG1, TG2>;
    }
}

[SuppressMessage("ReSharper", "UnusedTypeParameter")]
public static class Nested<T>
{
    public static class Child
    {
        public static class GrandChild;
        public static class GrandChild<TG1>;
        public static class GrandChild<TG1, TG2>;
    }

    public static class Child<TC1>
    {
        public static class GrandChild;
        public static class GrandChild<TG1>;
        public static class GrandChild<TG1, TG2>;
    }

    public static class Child<TC1, TC2>
    {
        public static class GrandChild;
        public static class GrandChild<TG1>;
        public static class GrandChild<TG1, TG2>;
    }
}