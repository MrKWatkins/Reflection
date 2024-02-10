using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;

namespace MrKWatkins.Reflection.Tests;

public sealed class MethodBaseExtensionsTests
{
    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void GetAccessibility(MethodBase method, Accessibility expected) => method.GetAccessibility().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> GetAccessibilityTestCases() =>
        typeof(MethodAccessibility)
            .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Select(method => new TestCaseData(method, Enum.Parse<Accessibility>(method.Name)).SetArgDisplayNames(method.Name));

    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void IsProtected(MethodBase method, Accessibility visibility) =>
        method.IsProtected().Should().Be(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void IsPublic(MethodBase method, Accessibility visibility) =>
        method.IsPublic().Should().Be(visibility == Accessibility.Public);

    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void IsPublicOrProtected(MethodBase method, Accessibility visibility) =>
        method.IsPublicOrProtected().Should().Be(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);
}