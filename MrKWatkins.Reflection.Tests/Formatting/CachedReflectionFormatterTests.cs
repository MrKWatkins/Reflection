using System.Reflection;
using System.Text;
using MrKWatkins.Reflection.Formatting;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;

namespace MrKWatkins.Reflection.Tests.Formatting;

public sealed class CachedReflectionFormatterTests : ReflectionFormatterTestFixture
{
    [Test]
    public void Constructor_ThrowsIfUnderlyingIsCachedReflectionFormatter()
    {
        var underlying = new TestReflectionFormatter();
        var cached = new CachedReflectionFormatter(underlying);

        FluentActions.Invoking(() => new CachedReflectionFormatter(cached)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Format_String()
    {
        var underlying = new TestReflectionFormatter();
        var cached = new CachedReflectionFormatter(underlying);

        var method1 = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Public));
        cached.Format(method1).Should().Be("Public");
        underlying.Calls.Should().BeEquivalentTo(new[] { method1 });

        cached.Format(method1).Should().Be("Public");
        underlying.Calls.Should().BeEquivalentTo(new[] { method1 });

        var method2 = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Internal));
        cached.Format(method2).Should().Be("Internal");
        underlying.Calls.Should().BeEquivalentTo(new[] { method1, method2 });
    }

    [Test]
    public void Format_StringBuilder()
    {
        var underlying = new TestReflectionFormatter();
        var cached = new CachedReflectionFormatter(underlying);

        var stringBuilder = new StringBuilder();

        var method1 = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Public));
        cached.Format(stringBuilder, method1);
        stringBuilder.ToString().Should().Be("Public");
        underlying.Calls.Should().BeEquivalentTo(new[] { method1 });

        cached.Format(stringBuilder, method1);
        stringBuilder.ToString().Should().Be("PublicPublic");
        underlying.Calls.Should().BeEquivalentTo(new[] { method1 });

        var method2 = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Internal));
        cached.Format(stringBuilder, method2);
        stringBuilder.ToString().Should().Be("PublicPublicInternal");
        underlying.Calls.Should().BeEquivalentTo(new[] { method1, method2 });
    }

    [Test]
    public void Format_TextWriter()
    {
        var underlying = new TestReflectionFormatter();
        var cached = new CachedReflectionFormatter(underlying);

        using var writer = new StringWriter();

        var method1 = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Public));
        cached.Format(writer, method1);
        writer.ToString().Should().Be("Public");
        underlying.Calls.Should().BeEquivalentTo(new[] { method1 });

        cached.Format(writer, method1);
        writer.ToString().Should().Be("PublicPublic");
        underlying.Calls.Should().BeEquivalentTo(new[] { method1 });

        var method2 = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Internal));
        cached.Format(writer, method2);
        writer.ToString().Should().Be("PublicPublicInternal");
        underlying.Calls.Should().BeEquivalentTo(new[] { method1, method2 });
    }

    private sealed class TestReflectionFormatter : IReflectionFormatter
    {
        private readonly List<MemberInfo> calls = new();

        public string Format(MemberInfo member)
        {
            calls.Add(member);
            return member.Name;
        }

        public IReadOnlyList<MemberInfo> Calls => calls;

        public void Format(StringBuilder output, MemberInfo member) => throw new NotSupportedException();

        public void Format(TextWriter output, MemberInfo member) => throw new NotSupportedException();
    }
}