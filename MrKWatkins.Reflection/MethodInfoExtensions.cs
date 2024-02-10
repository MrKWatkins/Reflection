using System.Diagnostics;
using System.Reflection;

namespace MrKWatkins.Reflection;

public static class MethodInfoExtensions
{
    /// <summary>
    /// Returns the relevant <see cref="CSharpOperator" /> value if the specified method is a C# operator; <c>null</c> otherwise.
    /// </summary>
    /// <remarks>
    /// Checks if the method is public, static, its name matches one of the operator method names and that the method has the correct signature.
    /// </remarks>
    /// <param name="method">The method.</param>
    /// <returns>A <see cref="CSharpOperator" /> value if the specified method is a C# operator; <c>null</c> otherwise.</returns>
    [Pure]
    public static CSharpOperator? GetCSharpOperator(this MethodInfo method)
    {
        if (method is not { IsPublic: true, IsStatic: true } ||
            !method.Name.StartsWith("op_", StringComparison.Ordinal) ||
            !Enum.TryParse<CSharpOperator>(method.Name.AsSpan(3), out var @operator))
        {
            return null;
        }

        var parameters = method.GetParameters();

        switch (@operator)
        {
            case CSharpOperator.Explicit:
            case CSharpOperator.Implicit:
                return parameters.Length == 1 &&
                       (method.ReturnType == method.DeclaringType || parameters[0].ParameterType == method.DeclaringType)
                    ? @operator
                    : null;

            // Unary.
            case CSharpOperator.CheckedDecrement:
            case CSharpOperator.CheckedIncrement:
            case CSharpOperator.CheckedUnaryNegation:
            case CSharpOperator.Decrement:
            case CSharpOperator.False:
            case CSharpOperator.Increment:
            case CSharpOperator.LogicalNot:
            case CSharpOperator.OnesComplement:
            case CSharpOperator.True:
            case CSharpOperator.UnaryNegation:
            case CSharpOperator.UnaryPlus:
                return parameters.Length == 1 &&
                       parameters[0].ParameterType == method.DeclaringType
                    ? @operator
                    : null;

            // Binary.
            case CSharpOperator.Addition:
            case CSharpOperator.BitwiseAnd:
            case CSharpOperator.BitwiseOr:
            case CSharpOperator.CheckedAddition:
            case CSharpOperator.CheckedDivision:
            case CSharpOperator.CheckedMultiply:
            case CSharpOperator.CheckedSubtraction:
            case CSharpOperator.Division:
            case CSharpOperator.Equality:
            case CSharpOperator.ExclusiveOr:
            case CSharpOperator.GreaterThan:
            case CSharpOperator.GreaterThanOrEqual:
            case CSharpOperator.Inequality:
            case CSharpOperator.LeftShift:
            case CSharpOperator.LessThan:
            case CSharpOperator.LessThanOrEqual:
            case CSharpOperator.Modulus:
            case CSharpOperator.Multiply:
            case CSharpOperator.RightShift:
            case CSharpOperator.Subtraction:
            case CSharpOperator.UnsignedRightShift:
                return parameters.Length == 2 &&
                       (parameters[0].ParameterType == method.DeclaringType || parameters[1].ParameterType == method.DeclaringType)
                    ? @operator
                    : null;
        }
        throw new UnreachableException();
    }

    /// <summary>
    /// Returns <c>true</c> if the specified method is a C# operator; <c>false</c> otherwise.
    /// </summary>
    /// <remarks>
    /// Checks if the method is public, static, its name matches one of the operator method names and that the method has the correct signature.
    /// </remarks>
    /// <param name="method">The method.</param>
    /// <returns><c>true</c> if the <paramref name="method"/> is a C# operator; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsCSharpOperator(this MethodInfo method) => method.GetCSharpOperator() != null;
}