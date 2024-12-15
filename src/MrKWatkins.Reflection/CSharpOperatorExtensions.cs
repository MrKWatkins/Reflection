namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="CSharpOperator"/>.
/// </summary>
public static class CSharpOperatorExtensions
{
    /// <summary>
    /// Returns the method name corresponding to the specified <see cref="CSharpOperator" />.
    /// </summary>
    /// <param name="operator">The operator.</param>
    /// <returns>The name of the method.</returns>
    [Pure]
    public static string ToMethodName(this CSharpOperator @operator) => $"op_{@operator}";
}