namespace MrKWatkins.Reflection.Formatting;

public sealed record DisplayNameFormatterOptions
{
    /// <summary>
    /// Whether members should be prefixed with their type or not. Defaults to <c>true</c>.
    /// </summary>
    public bool PrefixMembersWithType { get; init; } = true;

    /// <summary>
    /// Whether types should be fully qualified with their namespace or not. Defaults to <c>false</c>.
    /// </summary>
    public bool UseFullyQualifiedTypes { get; init; }

    /// <summary>
    /// Whether question marks should be used to identify nullable types or not, i.e. <code>int?</code> instead of <code>Nullable&lt;T&gt;</code>.
    /// Defaults to <c>true</c>.
    /// </summary>
    public bool UseQuestionMarksForNullableTypes { get; init; } = true;
}