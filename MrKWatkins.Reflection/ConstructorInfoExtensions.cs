using System.Reflection;

namespace MrKWatkins.Reflection;

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
}