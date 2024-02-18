using System.Reflection;
using System.Runtime.CompilerServices;

namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="PropertyInfo" />.
/// </summary>
public static class PropertyInfoExtensions
{
    /// <summary>
    /// Enumerates the overloads of the specified property that are declared in the same type. Only indexer properties can be overloaded.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>
    /// The overloads of <paramref name="property"/> declared in the same type; will be empty if the property is not overloaded or is
    /// not an indexer property.
    /// </returns>
    [Pure]
    public static IEnumerable<PropertyInfo> EnumerateOverloads(this PropertyInfo property)
    {
        if (!property.IsIndexer())
        {
            return Enumerable.Empty<PropertyInfo>();
        }

        return property.DeclaringType!
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
            .Where(p => p != property && p.IsIndexer());
    }

    /// <summary>
    /// Returns the <see cref="Accessibility" /> of the specified <see cref="PropertyInfo" />.
    /// </summary>
    /// <param name="property">The method.</param>
    /// <returns>
    /// The <see cref="Accessibility" /> of the <paramref name="property"/>.
    /// </returns>
    [Pure]
    public static Accessibility GetAccessibility(this PropertyInfo property)
    {
        var getAccessibility = property.GetMethod?.GetAccessibility() ?? Accessibility.Private;
        var setAccessibility = property.SetMethod?.GetAccessibility() ?? Accessibility.Private;

        return getAccessibility >= setAccessibility ? getAccessibility : setAccessibility;
    }

    /// <summary>
    /// If the specified <see cref="PropertyInfo" /> overrides a property in a base class then this returns the base <see cref="PropertyInfo" />,
    /// otherwise it returns the specified <see cref="PropertyInfo" />.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>The <see cref="PropertyInfo" /> in the base class <paramref name="property"/> overrides, otherwise <paramref name="property"/>.</returns>
    [Pure]
    public static PropertyInfo GetBaseDefinition(this PropertyInfo property)
    {
        var baseDefinition = GetAccessorBaseDefinition(property.GetMethod) ?? GetAccessorBaseDefinition(property.SetMethod);
        return baseDefinition?.DeclaringType!
                   .GetProperty(property.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
               ?? property;
    }

    [Pure]
    private static MethodInfo? GetAccessorBaseDefinition(MethodInfo? accessor)
    {
        var baseDefinition = accessor?.GetBaseDefinition();
        return baseDefinition != accessor ? baseDefinition : null;
    }

    /// <summary>
    /// Gets the <see cref="Virtuality" /> of the specified <see cref="PropertyInfo" />.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>The <see cref="Virtuality" /> of <paramref name="property"/>.</returns>
    [Pure]
    public static Virtuality GetVirtuality(this PropertyInfo property)
    {
        var isNew = property.IsNew();
        if (property.IsAbstract())
        {
            return isNew ? Virtuality.NewAbstract : Virtuality.Abstract;
        }

        var accessor = (property.GetMethod ?? property.SetMethod)!;
        if (property.GetBaseDefinition() != property)
        {
            return accessor.IsFinal ? Virtuality.SealedOverride : Virtuality.Override;
        }

        if (accessor is { IsVirtual: true, IsFinal: false })
        {
            return isNew ? Virtuality.NewVirtual : Virtuality.Virtual;
        }

        return isNew ? Virtuality.New : Virtuality.Normal;
    }

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="PropertyInfo" /> has a setter marked with the init modifier; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns><c>true</c> if <paramref name="property"/> has a setter marked with the init modifier; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool HasInitSetter(this PropertyInfo property) =>
        property.SetMethod?.ReturnParameter.GetRequiredCustomModifiers().Contains(typeof(IsExternalInit)) == true;

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="PropertyInfo" /> has public or protected overloads, as viewed from an external assembly, i.e.
    /// their <see cref="Accessibility" /> is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or
    /// <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise. Only applies to indexer properties.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns><c>true</c> if <paramref name="property"/> has public or protected overloads; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool HasPublicOrProtectedOverloads(this PropertyInfo property) => property.EnumerateOverloads().Any(p => p.IsPublicOrProtected());

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="PropertyInfo" /> is abstract; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns><c>true</c> if <paramref name="property"/> is abstract; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsAbstract(this PropertyInfo property) => (property.GetMethod ?? property.SetMethod)!.IsAbstract;

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="PropertyInfo" /> is abstract or virtual; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns><c>true</c> if t<paramref name="property"/> is abstract or virtual; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsAbstractOrVirtual(this PropertyInfo property) => (property.GetMethod ?? property.SetMethod)!.IsVirtual;

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="PropertyInfo" /> is an indexer property; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns><c>true</c> if <paramref name="property"/> is an indexer property; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsIndexer(this PropertyInfo property) => property.GetIndexParameters().Length > 0;

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="PropertyInfo" /> is a property marked with the new modifier; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns><c>true</c> if <paramref name="property"/> is a property marked with the new modifier; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsNew(this PropertyInfo property)
    {
        if (property.GetBaseDefinition() != property)
        {
            return false;
        }

        return property.DeclaringType?.BaseType?
            // Not using BindingFlags.DeclaredOnly so will retrieve any depth lower in the hierarchy.
            .GetProperty(property.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic) != null;
    }

    /// <summary>
    /// Returns <c>true</c> if the property is protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="property"/> is <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />;
    /// <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsProtected(this PropertyInfo property) => property.GetAccessibility() is Accessibility.Protected or Accessibility.ProtectedInternal;

    /// <summary>
    /// Returns <c>true</c> if the property is public, i.e. its <see cref="Accessibility" /> is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="property"/> is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublic(this PropertyInfo property) => property.GetAccessibility() == Accessibility.Public;

    /// <summary>
    /// Returns <c>true</c> if the property is public or protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="property"/> is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or
    /// <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublicOrProtected(this PropertyInfo property) => property.GetAccessibility() >= Accessibility.Protected;

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="PropertyInfo" /> represents a property marked with the required modifier; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property</param>
    /// <returns><c>true</c> if <paramref name="property"/> represents a property marked with the required modifier; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsRequired(this PropertyInfo property) => property.IsDefined(typeof(RequiredMemberAttribute));

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="PropertyInfo" /> represents a static property; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property</param>
    /// <returns><c>true</c> if <paramref name="property"/> represents a static property; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsStatic(this PropertyInfo property) => (property.GetMethod ?? property.SetMethod)!.IsStatic;
}