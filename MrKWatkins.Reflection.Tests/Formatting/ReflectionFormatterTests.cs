using System.Reflection;
using MrKWatkins.Reflection.Formatting;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;

namespace MrKWatkins.Reflection.Tests.Formatting;

public sealed class ReflectionFormatterTests : ReflectionFormatterTestFixture
{
    [Test]
    public void Format_MethodBase_ThrowsByDefault()
    {
        MethodBase method = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Public));

        new TestReflectionFormatter().Invoking(f => f.Format(method)).Should().Throw<NotImplementedException>();
    }

    private sealed class TestReflectionFormatter : ReflectionFormatter
    {
        protected override void Format(TextWriter output, EventInfo @event) => throw new NotSupportedException();
        protected override void Format(TextWriter output, FieldInfo field) => throw new NotSupportedException();
        protected override void Format(TextWriter output, PropertyInfo property) => throw new NotSupportedException();
        protected override void Format(TextWriter output, Type type) => throw new NotSupportedException();
    }
}