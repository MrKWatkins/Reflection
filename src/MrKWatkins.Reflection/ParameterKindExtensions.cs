namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="ParameterKind" />.
/// </summary>
public static class ParameterKindExtensions
{
    /// <summary>
    /// Returns the equivalent of the specified <see cref="ParameterKind" /> in C# keywords, e.g. "params" for
    /// <see cref="ParameterKind.Params" />. The empty string is returned for <see cref="ParameterKind.Normal"/>.
    /// </summary>
    /// <param name="parameterKind">The <see cref="ParameterKind" />.</param>
    /// <returns>The equivalent C# keywords.</returns>
    public static string ToCSharpKeywords(this ParameterKind parameterKind) => parameterKind switch
    {
        ParameterKind.Normal => "",
        ParameterKind.Params => "params",
        ParameterKind.Ref => "ref",
        ParameterKind.Out => "out",
        ParameterKind.In => "in",
        _ => throw new NotSupportedException($"The {nameof(ParameterKind)} value {parameterKind} is not supported.")
    };
}