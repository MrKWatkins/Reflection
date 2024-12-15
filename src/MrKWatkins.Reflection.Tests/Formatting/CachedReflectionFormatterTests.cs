using System.Reflection;
using System.Text;
using MrKWatkins.Reflection.Formatting;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;
using MrKWatkins.Reflection.Tests.TestTypes.Properties;

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
        underlying.FormatCalls.Should().BeEquivalentTo([method1]);

        cached.Format(method1).Should().Be("Public");
        underlying.FormatCalls.Should().BeEquivalentTo([method1]);

        var method2 = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Internal));
        cached.Format(method2).Should().Be("Internal");
        underlying.FormatCalls.Should().BeEquivalentTo([method1, method2]);
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
        underlying.FormatCalls.Should().BeEquivalentTo([method1]);

        cached.Format(stringBuilder, method1);
        stringBuilder.ToString().Should().Be("PublicPublic");
        underlying.FormatCalls.Should().BeEquivalentTo([method1]);

        var method2 = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Internal));
        cached.Format(stringBuilder, method2);
        stringBuilder.ToString().Should().Be("PublicPublicInternal");
        underlying.FormatCalls.Should().BeEquivalentTo([method1, method2]);
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
        underlying.FormatCalls.Should().BeEquivalentTo([method1]);

        cached.Format(writer, method1);
        writer.ToString().Should().Be("PublicPublic");
        underlying.FormatCalls.Should().BeEquivalentTo([method1]);

        var method2 = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Internal));
        cached.Format(writer, method2);
        writer.ToString().Should().Be("PublicPublicInternal");
        underlying.FormatCalls.Should().BeEquivalentTo([method1, method2]);
    }

    [Test]
    public void FormatNamespace_MemberInfo()
    {
        var underlying = new TestReflectionFormatter();
        var cached = new CachedReflectionFormatter(underlying);

        var method = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Public));
        cached.FormatNamespace(method).Should().Be("MrKWatkins.Reflection.Tests.TestTypes.Methods");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("MrKWatkins.Reflection.Tests.TestTypes.Methods");

        var sameNamespaceMethod = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Internal));
        cached.FormatNamespace(sameNamespaceMethod).Should().Be("MrKWatkins.Reflection.Tests.TestTypes.Methods");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("MrKWatkins.Reflection.Tests.TestTypes.Methods");

        var differentNamespaceProperty = GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal));
        cached.FormatNamespace(differentNamespaceProperty).Should().Be("MrKWatkins.Reflection.Tests.TestTypes.Properties");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("MrKWatkins.Reflection.Tests.TestTypes.Methods", "MrKWatkins.Reflection.Tests.TestTypes.Properties");
    }

    [Test]
    public void FormatNamespace_String()
    {
        var underlying = new TestReflectionFormatter();
        var cached = new CachedReflectionFormatter(underlying);

        cached.FormatNamespace("Some.Namespace").Should().Be("Some.Namespace");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("Some.Namespace");

        cached.FormatNamespace("Some.Namespace").Should().Be("Some.Namespace");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("Some.Namespace");

        cached.FormatNamespace("Different.Namespace").Should().Be("Different.Namespace");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("Some.Namespace", "Different.Namespace");
    }

    [Test]
    public void FormatNamespace_StringBuilder_MemberInfo()
    {
        var underlying = new TestReflectionFormatter();
        var cached = new CachedReflectionFormatter(underlying);

        var output = new StringBuilder();
        var method = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Public));
        cached.FormatNamespace(output, method);
        output.ToString().Should().Be("MrKWatkins.Reflection.Tests.TestTypes.Methods");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("MrKWatkins.Reflection.Tests.TestTypes.Methods");

        var sameNamespaceMethod = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Internal));
        cached.FormatNamespace(output, sameNamespaceMethod);
        output.ToString().Should().Be("MrKWatkins.Reflection.Tests.TestTypes.MethodsMrKWatkins.Reflection.Tests.TestTypes.Methods");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("MrKWatkins.Reflection.Tests.TestTypes.Methods");

        var differentNamespaceProperty = GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal));
        cached.FormatNamespace(output, differentNamespaceProperty);
        output.ToString().Should().Be("MrKWatkins.Reflection.Tests.TestTypes.MethodsMrKWatkins.Reflection.Tests.TestTypes.MethodsMrKWatkins.Reflection.Tests.TestTypes.Properties");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("MrKWatkins.Reflection.Tests.TestTypes.Methods", "MrKWatkins.Reflection.Tests.TestTypes.Properties");
    }

    [Test]
    public void FormatNamespace_StringBuilder_String()
    {
        var underlying = new TestReflectionFormatter();
        var cached = new CachedReflectionFormatter(underlying);

        var output = new StringBuilder();
        cached.FormatNamespace(output, "Some.Namespace");
        output.ToString().Should().BeEquivalentTo("Some.Namespace");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("Some.Namespace");

        cached.FormatNamespace(output, "Some.Namespace");
        output.ToString().Should().BeEquivalentTo("Some.NamespaceSome.Namespace");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("Some.Namespace");

        cached.FormatNamespace(output, "Different.Namespace");
        output.ToString().Should().BeEquivalentTo("Some.NamespaceSome.NamespaceDifferent.Namespace");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("Some.Namespace", "Different.Namespace");
    }

    [Test]
    public void FormatNamespace_TextWriter_MemberInfo()
    {
        var underlying = new TestReflectionFormatter();
        var cached = new CachedReflectionFormatter(underlying);

        using var output = new StringWriter();
        var method = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Public));
        cached.FormatNamespace(output, method);
        output.ToString().Should().Be("MrKWatkins.Reflection.Tests.TestTypes.Methods");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("MrKWatkins.Reflection.Tests.TestTypes.Methods");

        var sameNamespaceMethod = GetMethod<MethodAccessibility>(nameof(MethodAccessibility.Internal));
        cached.FormatNamespace(output, sameNamespaceMethod);
        output.ToString().Should().Be("MrKWatkins.Reflection.Tests.TestTypes.MethodsMrKWatkins.Reflection.Tests.TestTypes.Methods");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("MrKWatkins.Reflection.Tests.TestTypes.Methods");

        var differentNamespaceProperty = GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal));
        cached.FormatNamespace(output, differentNamespaceProperty);
        output.ToString().Should().Be("MrKWatkins.Reflection.Tests.TestTypes.MethodsMrKWatkins.Reflection.Tests.TestTypes.MethodsMrKWatkins.Reflection.Tests.TestTypes.Properties");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("MrKWatkins.Reflection.Tests.TestTypes.Methods", "MrKWatkins.Reflection.Tests.TestTypes.Properties");
    }

    [Test]
    public void FormatNamespace_TextWriter_String()
    {
        var underlying = new TestReflectionFormatter();
        var cached = new CachedReflectionFormatter(underlying);

        using var output = new StringWriter();
        cached.FormatNamespace(output, "Some.Namespace");
        output.ToString().Should().BeEquivalentTo("Some.Namespace");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("Some.Namespace");

        cached.FormatNamespace(output, "Some.Namespace");
        output.ToString().Should().BeEquivalentTo("Some.NamespaceSome.Namespace");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("Some.Namespace");

        cached.FormatNamespace(output, "Different.Namespace");
        output.ToString().Should().BeEquivalentTo("Some.NamespaceSome.NamespaceDifferent.Namespace");
        underlying.FormatNamespaceCalls.Should().BeEquivalentTo("Some.Namespace", "Different.Namespace");
    }

    private sealed class TestReflectionFormatter : IReflectionFormatter
    {
        private readonly List<MemberInfo> formatCalls = [];
        private readonly List<string> formatNamespaceCalls = [];

        public string Format(MemberInfo member)
        {
            formatCalls.Add(member);
            return member.Name;
        }

        public IReadOnlyList<MemberInfo> FormatCalls => formatCalls;

        public string FormatNamespace(string @namespace)
        {
            formatNamespaceCalls.Add(@namespace);
            return @namespace;
        }

        public IReadOnlyList<string> FormatNamespaceCalls => formatNamespaceCalls;

        public void Format(StringBuilder output, MemberInfo member) => throw new NotSupportedException();
        public void Format(TextWriter output, MemberInfo member) => throw new NotSupportedException();

        public string FormatNamespace(MemberInfo member) => throw new NotSupportedException();
        public void FormatNamespace(StringBuilder output, MemberInfo member) => throw new NotSupportedException();
        public void FormatNamespace(StringBuilder output, string @namespace) => throw new NotSupportedException();
        public void FormatNamespace(TextWriter output, MemberInfo member) => throw new NotSupportedException();
        public void FormatNamespace(TextWriter output, string @namespace) => throw new NotSupportedException();
    }
}