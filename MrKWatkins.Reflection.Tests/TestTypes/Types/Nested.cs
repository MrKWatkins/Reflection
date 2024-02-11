namespace MrKWatkins.Reflection.Tests.TestTypes.Types;

[SuppressMessage("ReSharper", "UnusedTypeParameter")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed class Nested
{
    public static void GenericMethodOneParameter<T1>(T1 one)
    {
    }

    public static void GenericMethodTwoParameters<T1, T2>(T1 one, T2 two)
    {
    }

    public sealed class Child
    {
        public static void ChildGenericMethodOneParameter<T1>(T1 one)
        {
        }

        public static void ChildGenericMethodTwoParameters<T1, T2>(T1 one, T2 two)
        {
        }

        public sealed class GrandChild;
        public sealed class GrandChild<TG1>;
        public sealed class GrandChild<TG1, TG2>;
    }

    public sealed class Child<TC1>
    {
        public static void ChildGenericMethodOneParameter<T1>(TC1 childOne, T1 one)
        {
        }

        public static void ChildGenericMethodTwoParameters<T1, T2>(TC1 childOne, T1 one, T2 two)
        {
        }

        public sealed class GrandChild;
        public sealed class GrandChild<TG1>;
        public sealed class GrandChild<TG1, TG2>;
    }

    public sealed class Child<TC1, TC2>
    {
        public static void ChildGenericMethodOneParameter<T1>(TC1 childOne, TC2 childTwo, T1 one)
        {
        }

        public static void ChildGenericMethodTwoParameters<T1, T2>(TC1 childOne, TC2 childTwo, T1 one, T2 two)
        {
        }

        public sealed class GrandChild;
        public sealed class GrandChild<TG1>;
        public sealed class GrandChild<TG1, TG2>;
    }
}

[SuppressMessage("ReSharper", "UnusedTypeParameter")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed class Nested<T>
{
    public static void GenericMethodOneParameter<T1>(T nested, T1 one)
    {
    }

    public static void GenericMethodTwoParameters<T1, T2>(T nested, T1 one, T2 two)
    {
    }

    public sealed class Child
    {
        public static void ChildGenericMethodOneParameter<T1>(T nested, T1 one)
        {
        }

        public static void ChildGenericMethodTwoParameters<T1, T2>(T nested, T1 one, T2 two)
        {
        }

        public static class GrandChild;
        public static class GrandChild<TG1>;
        public static class GrandChild<TG1, TG2>;
    }

    public sealed class Child<TC1>
    {
        public static void ChildGenericMethodOneParameter<T1>(T nested, TC1 childOne, T1 one)
        {
        }

        public static void ChildGenericMethodTwoParameters<T1, T2>(T nested, TC1 childOne, T1 one, T2 two)
        {
        }

        public static class GrandChild;
        public static class GrandChild<TG1>;
        public static class GrandChild<TG1, TG2>;
    }

    public sealed class Child<TC1, TC2>
    {
        public static void ChildGenericMethodOneParameter<T1>(T nested, TC1 childOne, TC2 childTwo, T1 one)
        {
        }

        public static void ChildGenericMethodTwoParameters<T1, T2>(T nested, TC1 childOne, TC2 childTwo, T1 one, T2 two)
        {
        }

        public static class GrandChild;
        public static class GrandChild<TG1>;
        public static class GrandChild<TG1, TG2>;
    }
}