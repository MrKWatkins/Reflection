using System.Reflection;

namespace MrKWatkins.Reflection.Tests;

public class ReflectionExtensionsTests
{
    [TestCase(typeof(ReflectionExtensionsTests), typeof(ReflectionExtensionsTests))]
    [TestCase(typeof(NestedType), typeof(ReflectionExtensionsTests), typeof(NestedType))]
    [TestCase(typeof(NestedType.ReallyNestedType), typeof(ReflectionExtensionsTests), typeof(NestedType), typeof(NestedType.ReallyNestedType))]
    public void EnumerateNestedTypes(Type type, params Type[] expected) => type.EnumerateNestedTypes().Should().BeEquivalentTo(expected);

    [TestCase(0, null)]
    [TestCase(1, ParameterKind.In)]
    [TestCase(2, ParameterKind.Out)]
    [TestCase(3, ParameterKind.Ref)]
    [TestCase(4, ParameterKind.Params)]
    public void GetKind(int parameterIndex, ParameterKind? expected)
    {
        var methodInfo = typeof(TestParameterClass).GetMethod(nameof(TestParameterClass.TestMethod))
                         ?? throw new InvalidOperationException($"Method {nameof(TestParameterClass.TestMethod)} not found on type {nameof(TestParameterClass)}.");

        methodInfo.GetParameters()[parameterIndex].GetKind().Should().Be(expected);
    }

    [TestCase(typeof(TestAbstractClass), nameof(TestAbstractClass.TestAbstractMethod), Virtuality.Abstract)]
    [TestCase(typeof(TestAbstractClass), nameof(TestAbstractClass.TestVirtualMethod), Virtuality.Virtual)]
    [TestCase(typeof(TestAbstractClass), nameof(TestAbstractClass.TestMethod), null)]
    [TestCase(typeof(TestSubClass), nameof(TestSubClass.TestAbstractMethod), Virtuality.SealedOverride)]
    [TestCase(typeof(TestSubClass), nameof(TestSubClass.TestVirtualMethod), Virtuality.Override)]
    [TestCase(typeof(TestSubClass), nameof(TestSubClass.TestMethod), Virtuality.New)]
    [TestCase(typeof(TestSubSubClass), nameof(TestSubSubClass.TestVirtualMethod), Virtuality.NewAbstract)]
    [TestCase(typeof(TestSubSubClass), nameof(TestSubSubClass.TestMethod), Virtuality.NewVirtual)]
    public void GetVirtuality(Type type, string method, Virtuality? expected)
    {
        var methodInfo = type.GetMethod(method) ?? throw new InvalidOperationException($"Method {method} not found on type {type}.");

        methodInfo.GetVirtuality().Should().Be(expected);
    }

    [TestCase(typeof(string), true)]
    [TestCase(typeof(object), false)]
    public void HasPublicOrProtectedOverloads_ConstructorInfo(Type type, bool expected)
    {
        var constructors = type
            .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
            .ToList();

        if (constructors.Count == 0)
        {
            throw new InvalidOperationException("Could not find constructor.");
        }

        constructors[0].HasPublicOrProtectedOverloads().Should().Be(expected);
        ((MethodBase)constructors[0]).HasPublicOrProtectedOverloads().Should().Be(expected);
    }

    [TestCase(typeof(int), nameof(int.ToString), true)]
    [TestCase(typeof(object), nameof(int.ToString), false)]
    [TestCase(typeof(MemoryStream), nameof(MemoryStream.Flush), false)]
    public void HasPublicOrProtectedOverloads_MethodInfo(Type type, string methodName, bool expected)
    {
        var methods = type
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
            .Where(m => m.Name == methodName)
            .ToList();

        if (methods.Count == 0)
        {
            throw new InvalidOperationException("Could not find method.");
        }

        methods[0].HasPublicOrProtectedOverloads().Should().Be(expected);
        ((MethodBase)methods[0]).HasPublicOrProtectedOverloads().Should().Be(expected);
    }

    [TestCase(nameof(TestFieldsClass.ConstField), true)]
    [TestCase(nameof(TestFieldsClass.StaticReadonlyField), false)]
    public void IsConst(string name, bool expected)
    {
        var field = typeof(TestFieldsClass).GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic)
                    ?? throw new InvalidOperationException($"Could not find field {name}.");
        field.IsConst().Should().Be(expected);
    }

    [TestCase(typeof(TestSubClass), nameof(TestSubClass.TestAbstractMethod), false)]
    [TestCase(typeof(TestSubClass), nameof(TestSubClass.TestVirtualMethod), false)]
    [TestCase(typeof(TestSubClass), nameof(TestSubClass.TestMethod), true)]
    [TestCase(typeof(TestSubSubClass), nameof(TestSubSubClass.TestVirtualMethod), true)]
    [TestCase(typeof(TestSubSubClass), nameof(TestSubSubClass.TestMethod), true)]
    public void IsNew(Type type, string method, bool expected)
    {
        var methodInfo = type.GetMethod(method) ?? throw new InvalidOperationException($"Method {method} not found on type {type}.");

        methodInfo.IsNew().Should().Be(expected);
    }

    [TestCase(typeof(int), false)]
    [TestCase(typeof(long), true)]
    [TestCase(typeof(byte), true)]
    [TestCase(typeof(decimal), false)]
    [TestCase(typeof(string), false)]
    public void IsProtected_ConstructorInfo(Type constructorParameterType, bool expected)
    {
        var constructor = typeof(TestVisibilityClass).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, [constructorParameterType])
                          ?? throw new InvalidOperationException($"Could not find constructor with parameter of type {constructorParameterType.ToDisplayName()}.");
        constructor.IsProtected().Should().Be(expected);
    }

    [TestCase("PublicEvent", false)]
    [TestCase("ProtectedEvent", true)]
    [TestCase("ProtectedInternalEvent", true)]
    [TestCase("PrivateProtectedEvent", false)]
    [TestCase("PrivateEvent", false)]
    public void IsProtected_EventInfo(string name, bool expected)
    {
        var @event = typeof(TestVisibilityClass).GetEvent(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic)
                     ?? throw new InvalidOperationException($"Could not find field {name}.");
        @event.IsProtected().Should().Be(expected);
    }

    [TestCase("PublicField", false)]
    [TestCase("ProtectedField", true)]
    [TestCase("ProtectedInternalField", true)]
    [TestCase("PrivateProtectedField", false)]
    [TestCase("PrivateField", false)]
    public void IsProtected_FieldInfo(string name, bool expected)
    {
        var field = typeof(TestVisibilityClass).GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic)
                    ?? throw new InvalidOperationException($"Could not find field {name}.");
        field.IsProtected().Should().Be(expected);
    }

    [TestCase("PublicEvent", true)]
    [TestCase("ProtectedEvent", false)]
    [TestCase("ProtectedInternalEvent", false)]
    [TestCase("PrivateProtectedEvent", false)]
    [TestCase("PrivateEvent", false)]
    public void IsPublic_EventInfo(string name, bool expected)
    {
        var @event = typeof(TestVisibilityClass).GetEvent(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic)
                     ?? throw new InvalidOperationException($"Could not find field {name}.");
        @event.IsPublic().Should().Be(expected);
    }

    [TestCase(typeof(int), true)]
    [TestCase(typeof(long), true)]
    [TestCase(typeof(byte), true)]
    [TestCase(typeof(decimal), false)]
    [TestCase(typeof(string), false)]
    public void IsPublicOrProtected_ConstructorInfo(Type constructorParameterType, bool expected)
    {
        var constructor = typeof(TestVisibilityClass).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, [constructorParameterType])
                    ?? throw new InvalidOperationException($"Could not find constructor with parameter of type {constructorParameterType.ToDisplayName()}.");
        constructor.IsPublicOrProtected().Should().Be(expected);
    }

    [TestCase("PublicEvent", true)]
    [TestCase("ProtectedEvent", true)]
    [TestCase("ProtectedInternalEvent", true)]
    [TestCase("PrivateProtectedEvent", false)]
    [TestCase("PrivateEvent", false)]
    public void IsPublicOrProtected_EventInfo(string name, bool expected)
    {
        var @event = typeof(TestVisibilityClass).GetEvent(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic)
                     ?? throw new InvalidOperationException($"Could not find field {name}.");
        @event.IsPublicOrProtected().Should().Be(expected);
    }

    [TestCase("PublicField", true)]
    [TestCase("ProtectedField", true)]
    [TestCase("ProtectedInternalField", true)]
    [TestCase("PrivateProtectedField", false)]
    [TestCase("PrivateField", false)]
    public void IsPublicOrProtected_FieldInfo(string name, bool expected)
    {
        var field = typeof(TestVisibilityClass).GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic)
                    ?? throw new InvalidOperationException($"Could not find field {name}.");
        field.IsPublicOrProtected().Should().Be(expected);
    }

    [TestCase("PublicMethod", true)]
    [TestCase("ProtectedMethod", true)]
    [TestCase("ProtectedInternalMethod", true)]
    [TestCase("PrivateProtectedMethod", false)]
    [TestCase("PrivateMethod", false)]
    public void IsPublicOrProtected_MethodInfo(string name, bool expected)
    {
        var method = typeof(TestVisibilityClass).GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic)
                     ?? throw new InvalidOperationException($"Could not find method {name}.");
        method.IsPublicOrProtected().Should().Be(expected);
    }

#pragma warning disable CA1812
#pragma warning disable CA1822
#pragma warning disable CS0414
#pragma warning disable CA1823
#pragma warning disable CA1802
#pragma warning disable CA1051
#pragma warning disable CA1044
    public static class NestedType
    {
        public static class ReallyNestedType;
    }

    public abstract class TestAbstractClass
    {
        public abstract void TestAbstractMethod();

        public virtual void TestVirtualMethod()
        {
        }

        public void TestMethod()
        {
        }
    }

    public class TestSubClass : TestAbstractClass
    {
        public sealed override void TestAbstractMethod()
        {
        }

        public override void TestVirtualMethod()
        {
        }

        public new void TestMethod()
        {
        }
    }

    public abstract class TestSubSubClass : TestAbstractClass
    {
        public new abstract void TestVirtualMethod();

        public new virtual void TestMethod()
        {
        }
    }

    public abstract class TestParameterClass
    {
        public abstract void TestMethod(string normal, in string @in, out string @out, ref string @ref, params string[] @params);
    }

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class TestFieldsClass
    {
        public static readonly int StaticReadonlyField = 1;
        public const int ConstField = 1;
    }

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class TestVisibilityClass
    {
        public static readonly int PublicField = 1;
        protected static readonly int ProtectedField = 1;
        protected internal static readonly int ProtectedInternalField = 1;
        private protected static readonly int PrivateProtectedField = 1;
        private static readonly int PrivateField = 1;

        public TestVisibilityClass(int _)
        {
        }

        protected TestVisibilityClass(long _)
        {
        }

        protected internal TestVisibilityClass(byte _)
        {
        }

        private protected TestVisibilityClass(decimal _)
        {
        }

        private TestVisibilityClass(string _)
        {
        }

        public void PublicMethod()
        {
        }

        protected void ProtectedMethod()
        {
        }

        protected internal void ProtectedInternalMethod()
        {
        }

        private protected void PrivateProtectedMethod()
        {
        }

        private void PrivateMethod()
        {
        }

        public event EventHandler PublicEvent = null!;
        protected event EventHandler ProtectedEvent = null!;
        protected internal event EventHandler ProtectedInternalEvent = null!;
        private protected event EventHandler PrivateProtectedEvent = null!;
        private event EventHandler PrivateEvent = null!;
    }
#pragma warning restore CA1044
#pragma warning restore CA1051
#pragma warning restore CA1802
#pragma warning restore CA1823
#pragma warning restore CS0414
#pragma warning restore CA1822
#pragma warning restore CA1812
}