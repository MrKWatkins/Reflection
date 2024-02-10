using System.Reflection;
using System.Runtime.CompilerServices;

namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="PropertyInfo" />.
/// </summary>
public static class PropertyInfoExtensions
{
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

    [Pure]
    public static Virtuality? GetVirtuality(this PropertyInfo property)
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

        return isNew ? Virtuality.New : null;
    }

    [Pure]
    public static bool HasInitSetter(this PropertyInfo property) =>
        property.SetMethod?.ReturnParameter.GetRequiredCustomModifiers().Contains(typeof(IsExternalInit)) == true;

    [Pure]
    public static bool HasPublicOrProtectedOverloads(this PropertyInfo property)
    {
        if (!property.IsIndexer())
        {
            return false;
        }

        var type = property.DeclaringType!;
        var indexers = type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
            .Where(p => p != property && p.IsIndexer() && p.IsPublicOrProtected());
        return indexers.Any();
    }

    [Pure]
    public static bool IsAbstract(this PropertyInfo property) => (property.GetMethod ?? property.SetMethod)!.IsAbstract;

    [Pure]
    public static bool IsAbstractOrVirtual(this PropertyInfo property) => (property.GetMethod ?? property.SetMethod)!.IsVirtual;

    [Pure]
    public static bool IsIndexer(this PropertyInfo property) => property.GetIndexParameters().Length > 0;

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
    /// <c>true</c> if the property is <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsProtected(this PropertyInfo property) => property.GetAccessibility() is Accessibility.Protected or Accessibility.ProtectedInternal;

    /// <summary>
    /// Returns <c>true</c> if the property is public, i.e. its <see cref="Accessibility" /> is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>
    /// <c>true</c> if the property is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublic(this PropertyInfo property) => property.GetAccessibility() == Accessibility.Public;

    /// <summary>
    /// Returns <c>true</c> if the property is public or protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>
    /// <c>true</c> if the property is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />;
    /// <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublicOrProtected(this PropertyInfo property) => property.GetAccessibility() >= Accessibility.Protected;

    [Pure]
    public static bool IsRequired(this PropertyInfo property) => property.IsDefined(typeof(RequiredMemberAttribute));

    [Pure]
    public static bool IsStatic(this PropertyInfo property) => (property.GetMethod ?? property.SetMethod)!.IsStatic;
}