using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="Type" />.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// If a type is a nested type then it enumerates its parents, starting at the outermost type, followed by the type itself.
    /// If it is not nested then it just returns the type.
    /// </summary>
    public static IEnumerable<Type> EnumerateNestedTypes(this Type type) => EnumerateNestedTypes(type, type.GetGenericArguments());

    private static IEnumerable<Type> EnumerateNestedTypes(Type type, Type[] genericTypeArguments)
    {
        if (type.IsNested)
        {
            foreach (var parent in EnumerateNestedTypes(type.DeclaringType!, genericTypeArguments))
            {
                yield return parent;
            }
        }

        yield return type.IsGenericTypeDefinition ? type.MakeGenericType(genericTypeArguments[..type.GetGenericArguments().Length]) : type;
    }

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
        const int visibilityMask = (int)TypeAttributes.VisibilityMask;

        var visibility = (TypeAttributes)(visibilityMask & (int)type.Attributes);

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

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="Type" /> is a <c>ref struct</c>; <c>false</c> otherwise.
    /// </summary>
    /// <remarks>Equivalent to <see cref="Type.IsByRefLike"/>.</remarks>
    /// <param name="type">The type.</param>
    /// <returns><c>true</c> if the specified <see cref="Type" /> is a <c>ref struct</c>; <c>false</c> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsRefStruct(this Type type) => type.IsByRefLike;

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="Type" /> is a <c>readonly struct</c>; <c>false</c> otherwise.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns><c>true</c> if the specified <see cref="Type" /> is a <c>readonly struct</c>; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsReadOnlyStruct(this Type type) => type.IsValueType && type.GetCustomAttribute<IsReadOnlyAttribute>() != null;

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="Type" /> is a <c>record class</c> or <c>record struct</c>; <c>false</c> otherwise.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns><c>true</c> if the specified <see cref="Type" /> is a <c>record class</c> or <c>record struct</c>; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsRecord(this Type type) => type.GetMethod("PrintMembers", BindingFlags.Instance | BindingFlags.NonPublic, [typeof(StringBuilder)]) != null;
}