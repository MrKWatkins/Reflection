using System.Reflection;

namespace MrKWatkins.Reflection;

public static class TypeExtensions
{
    /// <summary>
    /// Returns the <see cref="Accessibility" /> of the specified <see cref="Type" />.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// The <see cref="Accessibility" /> of the <paramref name="type"/>.
    /// </returns>
    [Pure]
    public static Accessibility GetAccessibility(this Type type)
    {
        const int visibilityMask = (int) TypeAttributes.VisibilityMask;

        var visibility = (TypeAttributes)(visibilityMask & (int) type.Attributes);

        return visibility switch
        {
            TypeAttributes.NotPublic => Accessibility.Internal,
            TypeAttributes.Public => Accessibility.Public,
            TypeAttributes.NestedPublic => Accessibility.Public,
            TypeAttributes.NestedPrivate => Accessibility.Private,
            TypeAttributes.NestedFamily => Accessibility.Protected,
            TypeAttributes.NestedAssembly => Accessibility.Internal,
            TypeAttributes.NestedFamANDAssem => Accessibility.PrivateProtected,
            _ => Accessibility.ProtectedInternal    // Only case left is TypeAttributes.NestedFamORAssem
        };
    }

    /// <summary>
    /// Returns <c>true</c> if the type is protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// <c>true</c> if the type is <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsProtected(this Type type) => type.GetAccessibility() is Accessibility.Protected or Accessibility.ProtectedInternal;

    /// <summary>
    /// Returns <c>true</c> if the type is public, nested or not, i.e. its <see cref="Accessibility" /> is <see cref="Accessibility.Public" />;
    /// <c>false</c> otherwise.
    /// </summary>
    /// <remarks>
    /// Differs from <see cref="Type.IsPublic" />; that property returns <c>false</c> if the type is nested, this method returns <c>true</c> for nested types.
    /// </remarks>
    /// <param name="type">The type.</param>
    /// <returns>
    /// <c>true</c> if the type is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublic(this Type type) => type.GetAccessibility() == Accessibility.Public;

    /// <summary>
    /// Returns <c>true</c> if the type is public or protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// <c>true</c> if the type is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />;
    /// <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublicOrProtected(this Type type) => type.GetAccessibility() >= Accessibility.Protected;
}