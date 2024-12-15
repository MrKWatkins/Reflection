namespace MrKWatkins.Reflection.Tests;

public sealed class VirtualityExtensionsTests
{
    [TestCase(Virtuality.Normal, "")]
    [TestCase(Virtuality.Abstract, "abstract")]
    [TestCase(Virtuality.Virtual, "virtual")]
    [TestCase(Virtuality.Override, "override")]
    [TestCase(Virtuality.SealedOverride, "sealed override")]
    [TestCase(Virtuality.New, "new")]
    [TestCase(Virtuality.NewAbstract, "new abstract")]
    [TestCase(Virtuality.NewVirtual, "new virtual")]
    public void ToCSharpKeywords(Virtuality virtuality, string expected) => virtuality.ToCSharpKeywords().Should().Be(expected);

    [Test]
    public void ToCSharpKeywords_ThrowsForUnsupportedValue() =>
        ((Virtuality)int.MaxValue).Invoking(a => a.ToCSharpKeywords()).Should().Throw<NotSupportedException>();
}