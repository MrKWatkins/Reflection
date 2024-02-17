using System.Reflection;

namespace MrKWatkins.Reflection;

public static class ParameterInfoExtensions
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
}