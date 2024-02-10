namespace MrKWatkins.Reflection;

/// <summary>
/// Describes how accessible a member is.
/// </summary>
/// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/accessibility-levels">Accessibility Levels (C# Reference)</see>
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public enum Accessibility
{
    /// <summary>
    /// Access is limited to the containing type.
    /// </summary>
    Private = 0,

    /// <summary>
    /// Access is limited to the containing class or types derived from the containing class within the current assembly.
    /// </summary>
    PrivateProtected = 1,

    /// <summary>
    /// Access is limited to the current assembly.
    /// </summary>
    Internal = 2,

    /// <summary>
    /// Access is limited to the containing class or types derived from the containing class.
    /// </summary>
    Protected = 3,

    /// <summary>
    /// Access is limited to the current assembly or types derived from the containing class.
    /// </summary>
    ProtectedInternal = 4,

    /// <summary>
    /// Access is not restricted.
    /// </summary>
    Public = 5
}