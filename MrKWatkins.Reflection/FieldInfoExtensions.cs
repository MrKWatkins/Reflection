using System.Reflection;
using System.Runtime.CompilerServices;

namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="FieldInfo" />.
/// </summary>
public static class FieldInfoExtensions
{
    /// <summary>
    /// Returns the <see cref="Accessibility" /> of the specified <see cref="FieldInfo" />.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>
    /// The <see cref="Accessibility" /> of the <paramref name="field"/>.
    /// </returns>
    [Pure]
    public static Accessibility GetAccessibility(this FieldInfo field)
    {
        const int memberAccessMask = (int) MethodAttributes.MemberAccessMask;

        // Ignoring fieldAttributes.PrivateScope; assuming that can't happen in the wild.
        var memberAccess = memberAccessMask & (int) field.Attributes;

        return (Accessibility) memberAccess - 1;
    }

    /// <summary>
    /// Returns <c>true</c> if the field is const; <c>false</c> otherwise.
    /// </summary>
    /// <remarks>
    /// Equivalent to <see cref="FieldInfo.IsLiteral" />.
    /// </remarks>
    /// <param name="field">The field.</param>
    /// <returns>
    /// <c>true</c> if the field is const; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsConst(this FieldInfo field) => field.IsLiteral;

    /// <summary>
    /// Returns <c>true</c> if the field is protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>
    /// <c>true</c> if the field is <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsProtected(this FieldInfo field) => field.GetAccessibility() is Accessibility.Protected or Accessibility.ProtectedInternal;

    /// <summary>
    /// Returns <c>true</c> if the field is public, i.e. its <see cref="Accessibility" /> is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </summary>
    /// <remarks>
    /// Equivalent to <see cref="FieldInfo.IsPublic" />; included for completion.
    /// </remarks>
    /// <param name="field">The field.</param>
    /// <returns>
    /// <c>true</c> if the field is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPublic(this FieldInfo field) => field.IsPublic;

    /// <summary>
    /// Returns <c>true</c> if the field is public or protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>
    /// <c>true</c> if the field is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />;
    /// <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublicOrProtected(this FieldInfo field) => field.GetAccessibility() >= Accessibility.Protected;

    /// <summary>
    /// Returns <c>true</c> if the field is <c>readonly</c>; <c>false</c> otherwise.
    /// </summary>
    /// <remarks>
    /// Equivalent to <see cref="FieldInfo.IsInitOnly" />.
    /// </remarks>
    /// <param name="field">The field.</param>
    /// <returns>
    /// <c>true</c> if the field is <c>readonly</c>; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsReadOnly(this FieldInfo field) => field.IsInitOnly;
}