using System.Reflection;
using MrKWatkins.Reflection.Formatting;

namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="MemberInfo" />.
/// </summary>
public static class MemberInfoExtensions
{
    private static readonly CachedReflectionFormatter DisplayNameFormatter = new(new DisplayNameFormatter());

    /// <summary>
    /// Returns the <see cref="Accessibility" /> of the specified <see cref="MemberInfo" />.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>
    /// The <see cref="Accessibility" /> of the <paramref name="member"/>.
    /// </returns>
    [Pure]
    public static Accessibility GetAccessibility(this MemberInfo member) =>
        member switch
        {
            EventInfo eventInfo => eventInfo.GetAccessibility(),
            FieldInfo fieldInfo => fieldInfo.GetAccessibility(),
            MethodBase methodBase => methodBase.GetAccessibility(),
            PropertyInfo propertyInfo => propertyInfo.GetAccessibility(),
            Type type => type.GetAccessibility(),
            _ => throw new NotSupportedException($"{nameof(MemberInfo)}s of type {member.GetType().Name} are not supported.")
        };

    /// <summary>
    /// Returns <c>true</c> if the member is protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>
    /// <c>true</c> if the member is <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsProtected(this MemberInfo member) => member.GetAccessibility() is Accessibility.Protected or Accessibility.ProtectedInternal;

    /// <summary>
    /// Returns <c>true</c> if the member is public, i.e. its <see cref="Accessibility" /> is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>
    /// <c>true</c> if the member is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublic(this MemberInfo member) => member.GetAccessibility() == Accessibility.Public;

    /// <summary>
    /// Returns <c>true</c> if the member is public or protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>
    /// <c>true</c> if the member is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />;
    /// <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublicOrProtected(this MemberInfo member) => member.GetAccessibility() >= Accessibility.Protected;

    /// <summary>
    /// Returns a display name for the member. Created using <see cref="DisplayNameFormatter" /> with default options.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>The display name of the member.</returns>
    [Pure]
    public static string ToDisplayName(this MemberInfo member) => DisplayNameFormatter.Format(member);
}