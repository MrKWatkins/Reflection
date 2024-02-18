namespace MrKWatkins.Reflection.Tests;

public sealed class AccessibilityExtensionsTests
{
    [TestCase(Accessibility.Private, "private")]
    [TestCase(Accessibility.PrivateProtected, "private protected")]
    [TestCase(Accessibility.Internal, "internal")]
    [TestCase(Accessibility.Protected, "protected")]
    [TestCase(Accessibility.ProtectedInternal, "protected internal")]
    [TestCase(Accessibility.Public, "public")]
    public void ToCSharpKeywords(Accessibility accessibility, string expected) => accessibility.ToCSharpKeywords().Should().Be(expected);

    [Test]
    public void ToCSharpKeywords_ThrowsForUnsupportedValue() =>
        ((Accessibility)int.MaxValue).Invoking(a => a.ToCSharpKeywords()).Should().Throw<NotSupportedException>();
}