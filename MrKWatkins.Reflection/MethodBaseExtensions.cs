using System.Reflection;
using System.Runtime.CompilerServices;

namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="MethodBase" />.
/// </summary>
public static class MethodBaseExtensions
{
    [Pure]
    public static IEnumerable<MethodBase> EnumerateOverloads(this MethodBase method) =>
        method switch
        {
            ConstructorInfo constructorInfo => constructorInfo.EnumerateOverloads(),
            MethodInfo methodInfo => methodInfo.EnumerateOverloads(),
            _ => throw new NotSupportedException($"Methods of type {method.GetType().ToDisplayName()} are not supported.")
        };

    /// <summary>
    /// Returns the <see cref="Accessibility" /> of the specified <see cref="MethodBase" />.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns>
    /// The <see cref="Accessibility" /> of the <paramref name="method"/>.
    /// </returns>
    [Pure]
    public static Accessibility GetAccessibility(this MethodBase method)
    {
        const int memberAccessMask = (int)MethodAttributes.MemberAccessMask;

        // Ignoring MethodAttributes.PrivateScope; assuming that can't happen in the wild.
        var memberAccess = memberAccessMask & (int)method.Attributes;

        return (Accessibility)memberAccess - 1;
    }

    /// <summary>
    /// Returns <c>true</c> if the method is protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns>
    /// <c>true</c> if the method is <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsProtected(this MethodBase method) => method.GetAccessibility() is Accessibility.Protected or Accessibility.ProtectedInternal;

    /// <summary>
    /// Returns <c>true</c> if the method is public, i.e. its <see cref="Accessibility" /> is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </summary>
    /// <remarks>
    /// Equivalent to <see cref="MethodBase.IsPublic" />; included for completion.
    /// </remarks>
    /// <param name="method">The method.</param>
    /// <returns>
    /// <c>true</c> if the method is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPublic(this MethodBase method) => method.IsPublic;

    /// <summary>
    /// Returns <c>true</c> if the method is public or protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns>
    /// <c>true</c> if the method is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />;
    /// <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublicOrProtected(this MethodBase method) => method.GetAccessibility() >= Accessibility.Protected;
}