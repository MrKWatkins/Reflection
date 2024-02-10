using System.Reflection;
using System.Runtime.CompilerServices;

namespace MrKWatkins.Reflection;

public static class ReflectionExtensions
{
    [Pure]
    public static bool IsConst(this FieldInfo field) => field.IsLiteral;

    [Pure]
    public static bool IsReadOnly(this FieldInfo field) => field.IsInitOnly;

    [Pure]
    public static bool IsPublicOrProtected(this FieldInfo field) => field.IsPublic || field.IsProtected();

    [Pure]
    public static bool IsProtected(this FieldInfo field) => field.IsFamily || field.IsFamilyOrAssembly;

    [Pure]
    public static bool IsPublicOrProtected(this MethodBase method) => method.IsPublic || method.IsProtected();

    [Pure]
    public static bool IsProtected(this MethodBase method) => method.IsFamily || method.IsFamilyOrAssembly;

    [Pure]
    public static bool IsPublic(this EventInfo @event) => @event.AddMethod?.IsPublic == true;

    [Pure]
    public static bool IsProtected(this EventInfo @event) => @event.AddMethod?.IsProtected() == true;

    [Pure]
    public static bool IsPublicOrProtected(this EventInfo @event) => @event.AddMethod?.IsPublicOrProtected() == true;

    [Pure]
    public static bool IsRecord(this Type type)
    {
        if (!type.IsClass)
        {
            return false;
        }

        return type.GetProperty("EqualityContract", BindingFlags.Instance | BindingFlags.NonPublic)?
            .GetCustomAttribute(typeof(CompilerGeneratedAttribute)) != null;
    }

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
    public static Visibility GetVisibility(this FieldInfo field) => field.IsPublic ? Visibility.Public : Visibility.Protected;

    [Pure]
    public static Visibility? GetVisibility(this MethodBase method)
    {
        if (method.IsPublic)
        {
            return Visibility.Public;
        }
        return method.IsProtected() ? Visibility.Protected : null;
    }

    [Pure]
    public static string ToKeyword(this Visibility visibility) =>
        visibility switch
        {
            Visibility.Protected => "protected",
            Visibility.Public => "public",
            _ => throw new NotSupportedException($"The {nameof(Visibility)} {visibility} is not supported.")
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
            _ => throw new NotSupportedException($"Methods of type {method.GetType().DisplayName()} are not supported.")
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