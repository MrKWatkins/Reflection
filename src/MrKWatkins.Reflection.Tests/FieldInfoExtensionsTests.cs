using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Fields;

namespace MrKWatkins.Reflection.Tests;

public sealed class FieldInfoExtensionsTests
{
    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void GetAccessibility(FieldInfo field, Accessibility expected) => field.GetAccessibility().Should().Equal(expected);

    [TestCase(nameof(FieldModifiers.Const), true)]
    [TestCase(nameof(FieldModifiers.Instance), false)]
    [TestCase(nameof(FieldModifiers.InstanceReadOnly), false)]
    [TestCase(nameof(FieldModifiers.Static), false)]
    [TestCase(nameof(FieldModifiers.StaticReadOnly), false)]
    public void IsConst(string name, bool expected) => GetField<FieldModifiers>(name).IsConst().Should().Equal(expected);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsProtected(FieldInfo field, Accessibility visibility) =>
        field.IsProtected().Should().Equal(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublic(FieldInfo field, Accessibility visibility) =>
        field.IsPublic().Should().Equal(visibility == Accessibility.Public);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublicOrProtected(FieldInfo field, Accessibility visibility) =>
        field.IsPublicOrProtected().Should().Equal(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCase(nameof(FieldModifiers.Const), false)]
    [TestCase(nameof(FieldModifiers.Instance), false)]
    [TestCase(nameof(FieldModifiers.InstanceReadOnly), true)]
    [TestCase(nameof(FieldModifiers.Static), false)]
    [TestCase(nameof(FieldModifiers.StaticReadOnly), true)]
    public void IsReadOnly(string name, bool expected) => GetField<FieldModifiers>(name).IsReadOnly().Should().Equal(expected);

    [Pure]
    public static IEnumerable<TestCaseData> AccessibilityTestCases() =>
        typeof(FieldAccessibility)
            .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Select(field => new TestCaseData(field, Enum.Parse<Accessibility>(field.Name)).SetArgDisplayNames(field.Name));

    [Pure]
    private static FieldInfo GetField<T>(string name) =>
        typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic)
        ?? throw new InvalidOperationException($"Could not find field {name}.");
}