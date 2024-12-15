namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="Virtuality" />.
/// </summary>
public static class VirtualityExtensions
{
    /// <summary>
    /// Returns the equivalent of the specified <see cref="Virtuality" /> in C# keywords, e.g. "sealed override" for
    /// <see cref="Virtuality.SealedOverride" />. The empty string is returned for <see cref="Virtuality.Normal"/>.
    /// </summary>
    /// <param name="virtuality">The <see cref="Virtuality" />.</param>
    /// <returns>The equivalent C# keywords.</returns>
    public static string ToCSharpKeywords(this Virtuality virtuality) => virtuality switch
    {
        Virtuality.Normal => "",
        Virtuality.Abstract => "abstract",
        Virtuality.Virtual => "virtual",
        Virtuality.Override => "override",
        Virtuality.SealedOverride => "sealed override",
        Virtuality.New => "new",
        Virtuality.NewAbstract => "new abstract",
        Virtuality.NewVirtual => "new virtual",
        _ => throw new NotSupportedException($"The {nameof(Virtuality)} value {virtuality} is not supported.")
    };
}