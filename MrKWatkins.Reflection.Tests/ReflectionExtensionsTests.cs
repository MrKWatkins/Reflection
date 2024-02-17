using System.Reflection;

namespace MrKWatkins.Reflection.Tests;

public class ReflectionExtensionsTests
{
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

#pragma warning disable CA1812
#pragma warning disable CA1822
#pragma warning disable CS0414
#pragma warning disable CA1823
#pragma warning disable CA1802
#pragma warning disable CA1051
#pragma warning disable CA1044

    public abstract class TestParameterClass
    {
        public abstract void TestMethod(string normal, in string @in, out string @out, ref string @ref, params string[] @params);
    }

#pragma warning restore CA1044
#pragma warning restore CA1051
#pragma warning restore CA1802
#pragma warning restore CA1823
#pragma warning restore CS0414
#pragma warning restore CA1822
#pragma warning restore CA1812
}