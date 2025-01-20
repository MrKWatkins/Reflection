using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Interfaces;
using MrKWatkins.Reflection.Tests.TestTypes.Types;

namespace MrKWatkins.Reflection.Tests;

public sealed class TypeExtensionsTests
{
    [TestCaseSource(nameof(EnumerateNestedTypesTestCases))]
    public void EnumerateNestedTypes(Type type, Type[] expected) => type.EnumerateNestedTypes().Should().SequenceEqual(expected);

    public static IEnumerable<TestCaseData> EnumerateNestedTypesTestCases()
    {
        yield return new TestCaseData(typeof(Nested), new[] { typeof(Nested) });
        yield return new TestCaseData(typeof(Nested.Child), new[] { typeof(Nested), typeof(Nested.Child) });
        yield return new TestCaseData(typeof(Nested.Child<>), new[] { typeof(Nested), typeof(Nested.Child<>) });
        yield return new TestCaseData(typeof(Nested.Child<int>), new[] { typeof(Nested), typeof(Nested.Child<int>) });
        yield return new TestCaseData(typeof(Nested.Child.GrandChild), new[] { typeof(Nested), typeof(Nested.Child), typeof(Nested.Child.GrandChild) });
        yield return new TestCaseData(typeof(Nested.Child<>.GrandChild), new[] { typeof(Nested), typeof(Nested.Child<>).MakeGenericType(typeof(Nested.Child<>.GrandChild).GetGenericArguments()), typeof(Nested.Child<>.GrandChild) });
        yield return new TestCaseData(typeof(Nested.Child<int>.GrandChild), new[] { typeof(Nested), typeof(Nested.Child<int>), typeof(Nested.Child<int>.GrandChild) });
        yield return new TestCaseData(typeof(Nested.Child.GrandChild<>), new[] { typeof(Nested), typeof(Nested.Child), typeof(Nested.Child.GrandChild<>) });
        yield return new TestCaseData(typeof(Nested.Child<>.GrandChild<>), new[] { typeof(Nested), typeof(Nested.Child<>).MakeGenericType(typeof(Nested.Child<>.GrandChild<>).GetGenericArguments()[0]), typeof(Nested.Child<>.GrandChild<>) });
        yield return new TestCaseData(typeof(Nested.Child<int>.GrandChild<int>), new[] { typeof(Nested), typeof(Nested.Child<int>), typeof(Nested.Child<int>.GrandChild<int>) });
    }

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void GetAccessibility(Type type, Accessibility expected) => type.GetAccessibility().Should().Equal(expected);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsProtected(Type type, Accessibility visibility) =>
        type.IsProtected().Should().Equal(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublic(Type type, Accessibility visibility) =>
        type.IsPublic().Should().Equal(visibility == Accessibility.Public);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublicOrProtected(Type type, Accessibility visibility) =>
        type.IsPublicOrProtected().Should().Equal(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);

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
    public void IsRefStruct(Type type, bool expected) => type.IsRefStruct().Should().Equal(expected);

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
    public void IsReadOnlyStruct(Type type, bool expected) => type.IsReadOnlyStruct().Should().Equal(expected);

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
    public void IsRecord(Type type, bool expected) => type.IsRecord().Should().Equal(expected);

    [TestCase(typeof(Class), false)]
    [TestCase(typeof(Static), true)]
    public void IsStatic(Type type, bool expected) => type.IsStatic().Should().Equal(expected);

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