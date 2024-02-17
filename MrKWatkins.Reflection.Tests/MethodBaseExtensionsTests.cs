using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes;
using MrKWatkins.Reflection.Tests.TestTypes.Constructors;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;

namespace MrKWatkins.Reflection.Tests;

public sealed class MethodBaseExtensionsTests : TestFixture
{
    [TestCaseSource(nameof(EnumerateOverloadsTestCases))]
    public void EnumerateOverloads(MethodBase method, MethodBase[] expected) =>
        method.EnumerateOverloads().Should().BeEquivalentTo(expected);

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
    public void GetAccessibility(MethodBase method, Accessibility expected) => method.GetAccessibility().Should().Be(expected);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsProtected(MethodBase method, Accessibility visibility) =>
        method.IsProtected().Should().Be(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublic(MethodBase method, Accessibility visibility) =>
        method.IsPublic().Should().Be(visibility == Accessibility.Public);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublicOrProtected(MethodBase method, Accessibility visibility) =>
        method.IsPublicOrProtected().Should().Be(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);

    [Pure]
    public static IEnumerable<TestCaseData> AccessibilityTestCases() =>
        typeof(MethodAccessibility)
            .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Select(method => new TestCaseData(method, Enum.Parse<Accessibility>(method.Name)).SetArgDisplayNames(method.Name));
}