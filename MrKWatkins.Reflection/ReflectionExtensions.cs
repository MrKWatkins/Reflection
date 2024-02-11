using System.Reflection;

namespace MrKWatkins.Reflection;

public static class ReflectionExtensions
{
    /// <summary>
    /// If a type is a nested type then it enumerates its parents, starting at the outermost type, followed by the type itself.
    /// If it is not nested then it just returns the type.
    /// </summary>
    public static IEnumerable<Type> EnumerateNestedTypes(this Type type)
    {
        if (type.IsNested)
        {
            foreach (var parent in EnumerateNestedTypes(type.DeclaringType!))
            {
                yield return parent;
            }
        }

        yield return type;
    }

    [Pure]
    public static string ToKeyword(this Accessibility accessibility) =>
        accessibility switch
        {
            Accessibility.Protected => "protected",
            Accessibility.Public => "public",
            _ => throw new NotSupportedException($"The {nameof(Accessibility)} {accessibility} is not supported.")
        };

    [Pure]
    public static Virtuality? GetVirtuality(this MethodInfo method)
    {
        var isNew = method.IsNew();
        if (method.IsAbstract)
        {
            return isNew ? Virtuality.NewAbstract : Virtuality.Abstract;
        }

        if (method.GetBaseDefinition() != method)
        {
            return method.IsFinal ? Virtuality.SealedOverride : Virtuality.Override;
        }

        if (method is { IsVirtual: true, IsFinal: false })
        {
            return isNew ? Virtuality.NewVirtual : Virtuality.Virtual;
        }

        return isNew ? Virtuality.New : null;
    }

    [Pure]
    public static bool IsNew(this MethodInfo method)
    {
        if (method.GetBaseDefinition() != method)
        {
            return false;
        }

        var baseType = method.DeclaringType?.BaseType;
        while (baseType != null && baseType != typeof(object))
        {
            var sameMethod = baseType.GetMethod(method.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            if (sameMethod != null)
            {
                return true;
            }

            baseType = baseType.BaseType;
        }

        return false;
    }

    [Pure]
    public static string ToKeyword(this Virtuality virtuality) =>
        virtuality switch
        {
            Virtuality.Abstract => "abstract",
            Virtuality.Virtual => "virtual",
            Virtuality.Override => "override",
            Virtuality.SealedOverride => "sealed override",
            Virtuality.New => "new",
            Virtuality.NewAbstract => "new abstract",
            Virtuality.NewVirtual => "new virtual",
            _ => throw new NotSupportedException($"The {nameof(Virtuality)} {virtuality} is not supported.")
        };

    [Pure]
    public static ParameterKind? GetKind(this ParameterInfo parameter)
    {
        if (parameter.IsIn)
        {
            return ParameterKind.In;
        }

        if (parameter.IsOut)
        {
            return ParameterKind.Out;
        }

        if (parameter.GetCustomAttribute<ParamArrayAttribute>() != null)
        {
            return ParameterKind.Params;
        }

        if (parameter.ParameterType.IsByRef)
        {
            return ParameterKind.Ref;
        }

        return null;
    }

    [Pure]
    public static string ToKeyword(this ParameterKind parameterKind) =>
        parameterKind switch
        {
            ParameterKind.Params => "params",
            ParameterKind.Ref => "ref",
            ParameterKind.Out => "out",
            ParameterKind.In => "in",
            _ => throw new NotSupportedException($"The {nameof(ParameterKind)} {parameterKind} is not supported.")
        };

    [Pure]
    public static bool HasPublicOrProtectedOverloads(this MethodBase method) =>
        method switch
        {
            ConstructorInfo constructorInfo => constructorInfo.HasPublicOrProtectedOverloads(),
            MethodInfo methodInfo => methodInfo.HasPublicOrProtectedOverloads(),
            _ => throw new NotSupportedException($"Methods of type {method.GetType().ToDisplayName()} are not supported.")
        };

    [Pure]
    public static bool HasPublicOrProtectedOverloads(this MethodInfo method)
    {
        var type = method.DeclaringType!;
        var methods = type
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
            .Where(m => m != method && m.Name == method.Name && m.IsPublicOrProtected());
        return methods.Any();
    }

    [Pure]
    public static bool HasPublicOrProtectedOverloads(this ConstructorInfo constructor)
    {
        var type = constructor.DeclaringType!;
        var constructors = type
            .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
            .Where(c => c != constructor && c.IsPublicOrProtected());
        return constructors.Any();
    }
}