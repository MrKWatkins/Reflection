using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Interfaces;
using MrKWatkins.Reflection.Tests.TestTypes.Types;

namespace MrKWatkins.Reflection.Tests;

public sealed class TypeExtensionsTests
{
    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void GetAccessibility(Type type, Accessibility expected) => type.GetAccessibility().Should().Be(expected);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsProtected(Type type, Accessibility visibility) =>
        type.IsProtected().Should().Be(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublic(Type type, Accessibility visibility) =>
        type.IsPublic().Should().Be(visibility == Accessibility.Public);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublicOrProtected(Type type, Accessibility visibility) =>
        type.IsPublicOrProtected().Should().Be(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCase(typeof(Class), false)]
    [TestCase(typeof(Enum), false)]
    [TestCase(typeof(IInterface), false)]
    [TestCase(typeof(ReadOnlyRecordStruct), false)]
    [TestCase(typeof(ReadOnlyRefStruct), true)]
    [TestCase(typeof(ReadOnlyStruct), false)]
    [TestCase(typeof(Record), false)]
    [TestCase(typeof(RecordStruct), false)]
    [TestCase(typeof(RefStruct), true)]
    [TestCase(typeof(Struct), false)]
    public void IsRefStruct(Type type, bool expected) => type.IsRefStruct().Should().Be(expected);

    [TestCase(typeof(Class), false)]
    [TestCase(typeof(Enum), false)]
    [TestCase(typeof(IInterface), false)]
    [TestCase(typeof(ReadOnlyRecordStruct), true)]
    [TestCase(typeof(ReadOnlyRefStruct), true)]
    [TestCase(typeof(ReadOnlyStruct), true)]
    [TestCase(typeof(Record), false)]
    [TestCase(typeof(RecordStruct), false)]
    [TestCase(typeof(RefStruct), false)]
    [TestCase(typeof(Struct), false)]
    public void IsReadOnlyStruct(Type type, bool expected) => type.IsReadOnlyStruct().Should().Be(expected);

    [TestCase(typeof(Class), false)]
    [TestCase(typeof(Enum), false)]
    [TestCase(typeof(IInterface), false)]
    [TestCase(typeof(ReadOnlyRecordStruct), true)]
    [TestCase(typeof(ReadOnlyRefStruct), false)]
    [TestCase(typeof(ReadOnlyStruct), false)]
    [TestCase(typeof(Record), true)]
    [TestCase(typeof(RecordStruct), true)]
    [TestCase(typeof(RefStruct), false)]
    [TestCase(typeof(Struct), false)]
    public void IsRecord(Type type, bool expected) => type.IsRecord().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> AccessibilityTestCases()
    {
        TestCaseData CreateTestCase(Type type, Accessibility expected, bool nested = false) =>
            new TestCaseData(type, expected).SetArgDisplayNames(nested ? $"Nested {type.Name}" : expected.ToString());

        return typeof(NestedAccessibility)
            .GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic)
            .Select(type => CreateTestCase(type, Enum.Parse<Accessibility>(type.Name), true))
            .Append(CreateTestCase(typeof(PublicAccessibility), Accessibility.Public))
            .Append(CreateTestCase(typeof(InternalAccessibility), Accessibility.Internal));
    }
}