using System.Reflection;

namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="ParameterInfo" />.
/// </summary>
public static class ParameterInfoExtensions
{
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

        if (parameter.GetCustomAttribute<ParamArrayAttribute>() != null)
        {
            return ParameterKind.Params;
        }

        if (parameter.ParameterType.IsByRef)
        {
            return ParameterKind.Ref;
        }

        return ParameterKind.Normal;
    }
}