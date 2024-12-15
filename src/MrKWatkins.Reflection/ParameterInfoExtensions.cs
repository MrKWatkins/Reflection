using System.Reflection;
using System.Runtime.CompilerServices;

namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="ParameterInfo" />.
/// </summary>
public static class ParameterInfoExtensions
{
    private static readonly NullabilityInfoContext NullabilityInfoContext = new();

    /// <summary>
    /// Gets the <see cref="ParameterKind" />  of the specified <see cref="ParameterInfo" />.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <returns>The <see cref="ParameterKind" /> of <paramref name="parameter"/>.</returns>
    [Pure]
    public static ParameterKind GetKind(this ParameterInfo parameter)
    {
        if (parameter.IsIn)
        {
            return ParameterKind.In;
        }

        if (parameter.IsOut)
        {
            return ParameterKind.Out;
        }

        if (parameter.GetCustomAttribute<ParamArrayAttribute>() != null ||
            parameter.GetCustomAttribute<ParamCollectionAttribute>() != null)
        {
            return ParameterKind.Params;
        }

        if (parameter.ParameterType.IsByRef)
        {
            return ParameterKind.Ref;
        }

        return ParameterKind.Normal;
    }

    /// <summary>
    /// Returns <c>true</c> if the specified parameter is a nullable reference type.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <returns><c>true</c> if the <paramref name="parameter"/>'s is a nullable reference type; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsNullableReferenceType(this ParameterInfo parameter)
    {
        if (parameter.ParameterType.IsValueType)
        {
            return false;
        }

        var info = NullabilityInfoContext.Create(parameter);

        return info.ReadState == NullabilityState.Nullable;
    }
}