namespace MrKWatkins.Reflection;

public static class CSharpOperatorExtensions
{
    [Pure]
    public static string ToMethodName(this CSharpOperator @operator) => $"op_{@operator}";
}