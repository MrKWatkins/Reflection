using System.Diagnostics;
using System.Reflection;

namespace MrKWatkins.Reflection;

/// <summary>
/// Extension methods for <see cref="MethodInfo" />.
/// </summary>
public static class MethodInfoExtensions
{
    /// <summary>
    /// Enumerates the overloads of the specified method that are declared in the same type.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns>The overloads of <paramref name="method"/> declared in the same type; will be empty if the method is not overloaded.</returns>
    [Pure]
    public static IEnumerable<MethodInfo> EnumerateOverloads(this MethodInfo method)
    {
        var type = method.DeclaringType!;
        return type
            .GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
            .Where(m => m != method && m.Name == method.Name);
    }

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

    /// <summary>
    /// Gets the <see cref="Virtuality" /> of the specified <see cref="MethodInfo" />.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns>The <see cref="Virtuality" /> of <paramref name="method"/>.</returns>
    [Pure]
    public static Virtuality GetVirtuality(this MethodInfo method)
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

        return isNew ? Virtuality.New : Virtuality.Normal;
    }

    /// <summary>
    /// Returns <c>true</c> if the specified <see cref="MethodInfo" /> has public or protected overloads, as viewed from an external assembly, i.e.
    /// their <see cref="Accessibility" /> is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or
    /// <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns><c>true</c> if <paramref name="method"/> has public or protected overloads; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool HasPublicOrProtectedOverloads(this MethodInfo method) => method.EnumerateOverloads().Any(m => m.IsPublicOrProtected());

    /// <summary>
    /// Returns <c>true</c> if the specified method has the <c>new</c> modifier; <c>false</c> otherwise.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns><c>true</c> if the <paramref name="method"/> has the <c>new</c> modifier; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsNew(this MethodInfo method)
    {
        if (method.GetBaseDefinition() != method)
        {
            return false;
        }

        return method.DeclaringType?.BaseType?
            // Not using BindingFlags.DeclaredOnly so will retrieve any depth lower in the hierarchy.
            .GetMethod(method.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic) != null;
    }
}