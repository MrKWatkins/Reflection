using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Fields;

namespace MrKWatkins.Reflection.Tests;

public sealed class FieldInfoExtensionsTests
{
    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void GetAccessibility(FieldInfo field, Accessibility expected) => field.GetAccessibility().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> GetAccessibilityTestCases() =>
        typeof(FieldAccessibility)
            .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Select(field => new TestCaseData(field, Enum.Parse<Accessibility>(field.Name)).SetArgDisplayNames(field.Name));

    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void IsProtected(FieldInfo field, Accessibility visibility) =>
        field.IsProtected().Should().Be(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void IsPublic(FieldInfo field, Accessibility visibility) =>
        field.IsPublic().Should().Be(visibility == Accessibility.Public);

    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void IsPublicOrProtected(FieldInfo field, Accessibility visibility) =>
        field.IsPublicOrProtected().Should().Be(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);
}