using System.Reflection;

namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="ConstructorInfo" />.
/// </summary>
public static class ConstructorInfoExtensions
{
    /// <summary>
    /// Enumerates the overloads of the specified constructor.
    /// </summary>
    /// <param name="constructor">The constructor.</param>
    /// <returns>The overloads of <paramref name="constructor"/>; will be empty if the method is not overloaded.</returns>
    [Pure]
    public static IEnumerable<ConstructorInfo> EnumerateOverloads(this ConstructorInfo constructor)
    {
        var type = constructor.DeclaringType!;
        return type
            .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(c => c != constructor);
    }

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="ConstructorInfo" /> has public or protected overloads, as viewed from an external assembly, i.e.
    /// their <see cref="Accessibility" /> is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or
    /// <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="constructor">The method.</param>
    /// <returns><c>true</c> if <paramref name="constructor"/> has public or protected overloads; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool HasPublicOrProtectedOverloads(this ConstructorInfo constructor) => constructor.EnumerateOverloads().Any(c => c.IsPublicOrProtected());
}