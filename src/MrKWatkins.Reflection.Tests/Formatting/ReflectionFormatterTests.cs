using System.Reflection;
using System.Text;
using MrKWatkins.Reflection.Formatting;
using MrKWatkins.Reflection.Tests.TestTypes;
using MrKWatkins.Reflection.Tests.TestTypes.Events;
using MrKWatkins.Reflection.Tests.TestTypes.Fields;
using MrKWatkins.Reflection.Tests.TestTypes.Properties;

namespace MrKWatkins.Reflection.Tests.Formatting;

public sealed class ReflectionFormatterTests : ReflectionFormatterTestFixture
{
    [TestCaseSource(nameof(FormatTestCases))]
    public void Format_MemberInfo(MemberInfo member, string expected) => new TestReflectionFormatter().Format(member).Should().Be(expected);

    [TestCaseSource(nameof(FormatTestCases))]
    public void Format_StringBuilder_MemberInfo(MemberInfo member, string expected)
    {
        var output = new StringBuilder();
        new TestReflectionFormatter().Format(output, member);
        output.ToString().Should().Be(expected);
    }

    [TestCaseSource(nameof(FormatTestCases))]
    public void Format_TextWriter_MemberInfo(MemberInfo member, string expected)
    {
        using var output = new StringWriter();
        new TestReflectionFormatter().Format(output, member);
        output.ToString().Should().Be(expected);
    }

    [Test]
    public void Format_ThrowsForUnsupportedMemberType() =>
        new TestReflectionFormatter().Invoking(f => f.Format(new UnsupportedMethodBase())).Should().Throw<NotSupportedException>();

    public static IEnumerable<TestCaseData> FormatTestCases()
    {
        yield return CreateTestCase(GetConstructor(typeof(Dictionary<,>), [typeof(int)]), "ConstructorInfo");
        yield return CreateTestCase(GetEvent<EventAccessibility>(nameof(EventAccessibility.Public)), "EventInfo");
        yield return CreateTestCase(GetField<FieldModifiers>(nameof(FieldModifiers.Const)), "FieldInfo");
        yield return CreateTestCase(GetMethod<object>(nameof(ToString)), "MethodInfo");
        yield return CreateTestCase(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), "PropertyInfo");
        yield return CreateTestCase(typeof(string), "Type");
    }

    [Test]
    public void FormatNamespace_MemberInfo() => new TestReflectionFormatter().FormatNamespace(typeof(ReflectionFormatterTests)).Should().Be("Namespace");

    [Test]
    public void FormatNamespace_String() => new TestReflectionFormatter().FormatNamespace("Some.Namespace").Should().Be("Namespace");

    [Test]
    public void FormatNamespace_StringBuilder_MemberInfo()
    {
        var output = new StringBuilder();
        new TestReflectionFormatter().FormatNamespace(output, typeof(ReflectionFormatterTests));
        output.ToString().Should().Be("Namespace");
    }

    [Test]
    public void FormatNamespace_StringBuilder_String()
    {
        var output = new StringBuilder();
        new TestReflectionFormatter().FormatNamespace(output, "Some.Namespace");
        output.ToString().Should().Be("Namespace");
    }

    [Test]
    public void FormatNamespace_TextWriter_MemberInfo()
    {
        using var output = new StringWriter();
        new TestReflectionFormatter().FormatNamespace(output, typeof(ReflectionFormatterTests));
        output.ToString().Should().Be("Namespace");
    }

    [Test]
    public void FormatNamespace_TextWriter_String()
    {
        using var output = new StringWriter();
        new TestReflectionFormatter().FormatNamespace(output, "Some.Namespace");
        output.ToString().Should().Be("Namespace");
    }

    private sealed class TestReflectionFormatter : ReflectionFormatter
    {
        public override void FormatNamespace(TextWriter output, string @namespace) => output.Write("Namespace");
        protected override void Format(TextWriter output, ConstructorInfo constructor) => output.Write(nameof(ConstructorInfo));
        protected override void Format(TextWriter output, EventInfo @event) => output.Write(nameof(EventInfo));
        protected override void Format(TextWriter output, FieldInfo field) => output.Write(nameof(FieldInfo));
        protected override void Format(TextWriter output, MethodInfo method) => output.Write(nameof(MethodInfo));
        protected override void Format(TextWriter output, PropertyInfo property) => output.Write(nameof(PropertyInfo));
        protected override void Format(TextWriter output, Type type) => output.Write(nameof(Type));
    }
}