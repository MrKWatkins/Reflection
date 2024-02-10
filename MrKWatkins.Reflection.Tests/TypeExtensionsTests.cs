using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Types;

namespace MrKWatkins.Reflection.Tests;

public sealed class TypeExtensionsTests
{
    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void GetAccessibility(Type type, Accessibility expected) => type.GetAccessibility().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> GetAccessibilityTestCases()
    {
        TestCaseData CreateTestCase(Type type, Accessibility expected, bool nested = false) =>
            new TestCaseData(type, expected).SetArgDisplayNames(nested ? $"Nested {type.Name}" : expected.ToString());

        return typeof(NestedAccessibility)
            .GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic)
            .Select(type => CreateTestCase(type, Enum.Parse<Accessibility>(type.Name), true))
            .Append(CreateTestCase(typeof(PublicAccessibility), Accessibility.Public))
            .Append(CreateTestCase(typeof(InternalAccessibility), Accessibility.Internal));
    }

    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void IsProtected(Type type, Accessibility visibility) =>
        type.IsProtected().Should().Be(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void IsPublic(Type type, Accessibility visibility) =>
        type.IsPublic().Should().Be(visibility == Accessibility.Public);

    [TestCaseSource(nameof(GetAccessibilityTestCases))]
    public void IsPublicOrProtected(Type type, Accessibility visibility) =>
        type.IsPublicOrProtected().Should().Be(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);
}