namespace MrKWatkins.Reflection.Tests;

public sealed class ParameterKindExtensionsTests
{
    [TestCase(ParameterKind.Normal, "")]
    [TestCase(ParameterKind.Params, "params")]
    [TestCase(ParameterKind.Ref, "ref")]
    [TestCase(ParameterKind.Out, "out")]
    [TestCase(ParameterKind.In, "in")]
    public void ToCSharpKeywords(ParameterKind parameterKind, string expected) => parameterKind.ToCSharpKeywords().Should().Be(expected);

    [Test]
    public void ToCSharpKeywords_ThrowsForUnsupportedValue() =>
        ((ParameterKind)int.MaxValue).Invoking(a => a.ToCSharpKeywords()).Should().Throw<NotSupportedException>();
}