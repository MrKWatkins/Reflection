using System.Reflection;
using System.Runtime.CompilerServices;

namespace MrKWatkins.Reflection;

public static class PropertyInfoExtensions
{
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
    public static Visibility? GetVisibility(this PropertyInfo property)
    {
        if (property.IsPublic())
        {
            return Visibility.Public;
        }
        return property.IsProtected() ? Visibility.Protected : null;
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

    [Pure]
    public static bool IsProtected(this PropertyInfo property)
    {
        var getMethod = property.GetMethod;
        var setMethod = property.SetMethod;
        if (getMethod == null)
        {
            return setMethod!.IsProtected();
        }

        if (setMethod == null)
        {
            return getMethod.IsProtected();
        }

        if (getMethod.IsPublic || setMethod.IsPublic)
        {
            return false;
        }

        return getMethod.IsProtected() || setMethod.IsProtected();
    }

    [Pure]
    public static bool IsPublic(this PropertyInfo property) => property.GetMethod?.IsPublic == true || property.SetMethod?.IsPublic == true;

    [Pure]
    public static bool IsPublicOrProtected(this PropertyInfo property) =>
        property.GetMethod?.IsPublicOrProtected() == true || property.SetMethod?.IsPublicOrProtected() == true;

    [Pure]
    public static bool IsRequired(this PropertyInfo property) => property.IsDefined(typeof(RequiredMemberAttribute));

    [Pure]
    public static bool IsStatic(this PropertyInfo property) => (property.GetMethod ?? property.SetMethod)!.IsStatic;
}