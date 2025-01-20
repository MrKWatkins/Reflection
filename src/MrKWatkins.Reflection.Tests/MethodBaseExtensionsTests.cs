using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes;
using MrKWatkins.Reflection.Tests.TestTypes.Constructors;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;
using MrKWatkins.Reflection.Tests.TestTypes.Types;

namespace MrKWatkins.Reflection.Tests;

public sealed class MethodBaseExtensionsTests : TestFixture
{
    [TestCaseSource(nameof(EnumerateOverloadsTestCases))]
    public void EnumerateOverloads(MethodBase method, MethodBase[] expected) =>
        method.EnumerateOverloads().Should().SequenceEqual(expected);

    [Test]
    public void EnumerateOverloads_ThrowsForUnsupportedMethodBaseType() =>
        new UnsupportedMethodBase().Invoking(m => m.EnumerateOverloads()).Should().Throw<NotSupportedException>();

    [Pure]
    public static IEnumerable<TestCaseData> EnumerateOverloadsTestCases()
    {
        var constructors = typeof(ConstructorOverloads).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var method in constructors)
        {
            yield return new TestCaseData(method, constructors.Where(m => m != method).ToArray());
        }

        var methods = GetMethods<MethodOverloads>(nameof(MethodOverloads.Overload));
        foreach (var method in methods)
        {
            yield return new TestCaseData(method, methods.Where(m => m != method).ToArray());
        }
    }

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void GetAccessibility(MethodBase method, Accessibility expected) => method.GetAccessibility().Should().Equal(expected);


    [TestCaseSource(nameof(HasPublicOrProtectedOverloadsTestCases))]
    public void HasPublicOrProtectedOverloads(MethodBase method, bool expected) => method.HasPublicOrProtectedOverloads().Should().Equal(expected);

    [Test]
    public void HasPublicOrProtectedOverloads_ThrowsForUnsupportedMethodBaseType() =>
        new UnsupportedMethodBase().Invoking(m => m.HasPublicOrProtectedOverloads()).Should().Throw<NotSupportedException>();

    [Pure]
    public static IEnumerable<TestCaseData> HasPublicOrProtectedOverloadsTestCases()
    {
        yield return new TestCaseData(GetConstructor<Class>(0), false);
        yield return new TestCaseData(GetMethod<MethodVirtuality>(nameof(MethodVirtuality.Normal)), false);

        var publicOverloadedConstructor = GetConstructors<ConstructorPublicOverload>();
        yield return new TestCaseData(publicOverloadedConstructor[0], true);
        yield return new TestCaseData(publicOverloadedConstructor[1], true);

        var protectedOverloadedConstructor = GetConstructors<ConstructorProtectedOverload>();
        yield return new TestCaseData(protectedOverloadedConstructor[0], true);
        yield return new TestCaseData(protectedOverloadedConstructor[1], true);

        var privateOverloadedConstructor = GetConstructors<ConstructorPrivateOverload>();
        yield return new TestCaseData(privateOverloadedConstructor[0], false);
        yield return new TestCaseData(privateOverloadedConstructor[1], true);

        var publicOverloadedMethod = GetMethods<MethodPublicOverload>(nameof(MethodPublicOverload.Overload));
        yield return new TestCaseData(publicOverloadedMethod[0], true);
        yield return new TestCaseData(publicOverloadedMethod[1], true);

        var protectedOverloadedMethod = GetMethods<MethodProtectedOverload>(nameof(MethodProtectedOverload.Overload));
        yield return new TestCaseData(protectedOverloadedMethod[0], true);
        yield return new TestCaseData(protectedOverloadedMethod[1], true);

        var privateOverloadedMethod = GetMethods<MethodPrivateOverload>(nameof(MethodPrivateOverload.Overload));
        yield return new TestCaseData(privateOverloadedMethod[0], false);
        yield return new TestCaseData(privateOverloadedMethod[1], true);
    }

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsProtected(MethodBase method, Accessibility visibility) =>
        method.IsProtected().Should().Equal(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublic(MethodBase method, Accessibility visibility) =>
        method.IsPublic().Should().Equal(visibility == Accessibility.Public);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublicOrProtected(MethodBase method, Accessibility visibility) =>
        method.IsPublicOrProtected().Should().Equal(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);

    [Pure]
    public static IEnumerable<TestCaseData> AccessibilityTestCases() =>
        typeof(MethodAccessibility)
            .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Select(method => new TestCaseData(method, Enum.Parse<Accessibility>(method.Name)).SetArgDisplayNames(method.Name));
}