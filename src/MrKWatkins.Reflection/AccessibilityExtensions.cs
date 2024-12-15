namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="Accessibility" />.
/// </summary>
public static class AccessibilityExtensions
{
    /// <summary>
    /// Returns the equivalent of the specified <see cref="Accessibility" /> in C# keywords, e.g. "private protected" for
    /// <see cref="Accessibility.PrivateProtected" />
    /// </summary>
    /// <param name="accessibility">The <see cref="Accessibility" />.</param>
    /// <returns>The equivalent C# keywords.</returns>
    public static string ToCSharpKeywords(this Accessibility accessibility) => accessibility switch
    {
        Accessibility.Private => "private",
        Accessibility.PrivateProtected => "private protected",
        Accessibility.Internal => "internal",
        Accessibility.Protected => "protected",
        Accessibility.ProtectedInternal => "protected internal",
        Accessibility.Public => "public",
        _ => throw new NotSupportedException($"The {nameof(Accessibility)} value {accessibility} is not supported.")
    };
}