using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Events;
using MrKWatkins.Reflection.Tests.TestTypes.Fields;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;
using MrKWatkins.Reflection.Tests.TestTypes.Properties;
using MrKWatkins.Reflection.Tests.TestTypes.Types;

namespace MrKWatkins.Reflection.Tests;

public sealed class MemberInfoExtensionsTests : TestFixture
{
    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void GetAccessibility(MemberInfo member, Accessibility expected) => member.GetAccessibility().Should().Equal(expected);

    [Test]
    public void GetAccessibility_UnsupportedMemberInfoThrows() =>
        new UnsupportedMemberInfo().Invoking(m => m.GetAccessibility()).Should().Throw<NotSupportedException>().That
            .Should().HaveMessage("MemberInfos of type UnsupportedMemberInfo are not supported.");

    [TestCaseSource(nameof(GetNamespaceTestCases))]
    public void GetNamespace(MemberInfo member, string? expected) => member.GetNamespace().Should().Equal(expected);

    [TestCaseSource(nameof(GetNamespaceTestCases))]
    public void GetNamespaceOrThrow(MemberInfo member, string? expected)
    {
        if (expected != null)
        {
            member.GetNamespaceOrThrow().Should().Equal(expected);
        }
        else
        {
            member.Invoking(m => m.GetNamespaceOrThrow()).Should().Throw<ArgumentException>();
        }
    }

    [Pure]
    [SuppressMessage("Design", "CA1024:Use properties where appropriate")]
    public static IEnumerable<TestCaseData> GetNamespaceTestCases()
    {
        yield return new TestCaseData(typeof(MethodAccessibility), "MrKWatkins.Reflection.Tests.TestTypes.Methods");
        yield return new TestCaseData(GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Public)), "MrKWatkins.Reflection.Tests.TestTypes.Methods");
        yield return new TestCaseData(typeof(GlobalType), null);
        yield return new TestCaseData(GetMethod<GlobalType>(nameof(GlobalType.Method)), null);
        yield return new TestCaseData(new GlobalMethodInfo(GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Public))), null);
    }

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsProtected(MemberInfo member, Accessibility visibility) =>
        member.IsProtected().Should().Equal(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublic(MemberInfo member, Accessibility visibility) =>
        member.IsPublic().Should().Equal(visibility == Accessibility.Public);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublicOrProtected(MemberInfo member, Accessibility visibility) =>
        member.IsPublicOrProtected().Should().Equal(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);

    [Pure]
    public static IEnumerable<TestCaseData> AccessibilityTestCases()
    {
        TestCaseData CreateTestCase(string type, MemberInfo member, Accessibility expected) => new TestCaseData(member, expected).SetArgDisplayNames(type);

        const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        yield return CreateTestCase("Constructor", typeof(NestedAccessibility).GetConstructor(Type.EmptyTypes)!, Accessibility.Public);
        yield return CreateTestCase("Event", typeof(EventAccessibility).GetEvent(nameof(EventAccessibility.Public), bindingFlags)!, Accessibility.Public);
        yield return CreateTestCase("Field", typeof(FieldAccessibility).GetField(nameof(FieldAccessibility.ProtectedInternal), bindingFlags)!, Accessibility.ProtectedInternal);
        yield return CreateTestCase("Method", typeof(MethodAccessibility).GetMethod(nameof(MethodAccessibility.ProtectedInternal), bindingFlags)!, Accessibility.ProtectedInternal);
        yield return CreateTestCase("Property", typeof(PropertyAccessibility).GetProperty(nameof(PropertyAccessibility.InternalGetNoSet), bindingFlags)!, Accessibility.Internal);
        yield return CreateTestCase("Type", typeof(InternalAccessibility), Accessibility.Internal);
    }

    [TestCase(typeof(string), "String")]
    [TestCase(typeof(int), "Int32")]
    [TestCase(typeof(List<string>), "List<String>")]
    [TestCase(typeof(Dictionary<string, int>), "Dictionary<String, Int32>")]
    public void ToDisplayName(Type type, string expected) => type.ToDisplayName().Should().Equal(expected);

    private sealed class UnsupportedMemberInfo : MemberInfo
    {
        public override object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();
        public override object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();
        public override bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();
        public override Type DeclaringType => throw new NotSupportedException();
        public override MemberTypes MemberType => throw new NotSupportedException();
        public override string Name => throw new NotSupportedException();
        public override Type ReflectedType => throw new NotSupportedException();
    }
}