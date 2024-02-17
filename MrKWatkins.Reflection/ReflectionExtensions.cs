using System.Reflection;

namespace MrKWatkins.Reflection;

public static class ReflectionExtensions
{

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