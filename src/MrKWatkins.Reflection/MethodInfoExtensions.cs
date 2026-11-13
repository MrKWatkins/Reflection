using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

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

        return @operator switch
        {
            CSharpOperator.Explicit or CSharpOperator.Implicit => parameters.Length == 1 &&
                                   (method.ReturnType == method.DeclaringType || parameters[0].ParameterType == method.DeclaringType)
                                ? @operator
                                : null,
            // Unary.
            CSharpOperator.CheckedDecrement or CSharpOperator.CheckedIncrement or CSharpOperator.CheckedUnaryNegation or CSharpOperator.Decrement or CSharpOperator.False or CSharpOperator.Increment or CSharpOperator.LogicalNot or CSharpOperator.OnesComplement or CSharpOperator.True or CSharpOperator.UnaryNegation or CSharpOperator.UnaryPlus => parameters.Length == 1 &&
                                   parameters[0].ParameterType == method.DeclaringType
                                ? @operator
                                : null,
            // Binary.
            CSharpOperator.Addition or CSharpOperator.BitwiseAnd or CSharpOperator.BitwiseOr or CSharpOperator.CheckedAddition or CSharpOperator.CheckedDivision or CSharpOperator.CheckedMultiply or CSharpOperator.CheckedSubtraction or CSharpOperator.Division or CSharpOperator.Equality or CSharpOperator.ExclusiveOr or CSharpOperator.GreaterThan or CSharpOperator.GreaterThanOrEqual or CSharpOperator.Inequality or CSharpOperator.LeftShift or CSharpOperator.LessThan or CSharpOperator.LessThanOrEqual or CSharpOperator.Modulus or CSharpOperator.Multiply or CSharpOperator.RightShift or CSharpOperator.Subtraction or CSharpOperator.UnsignedRightShift => parameters.Length == 2 &&
                                   (parameters[0].ParameterType == method.DeclaringType || parameters[1].ParameterType == method.DeclaringType)
                                ? @operator
                                : null,
            _ => throw new UnreachableException(),
        };
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
    /// Returns <c>true</c> if the specified <see cref="MethodInfo" /> has public or protected overloads, as viewed from an external assembly, that is,
    /// their <see cref="Accessibility" /> is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or
    /// <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns><c>true</c> if <paramref name="method"/> has public or protected overloads; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool HasPublicOrProtectedOverloads(this MethodInfo method) => method.EnumerateOverloads().Any(m => m.IsPublicOrProtected());

    /// <summary>
    /// Returns <c>true</c> if the specified method is an extension method; <c>false</c> otherwise.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns><c>true</c> if the <paramref name="method"/> is an extension method; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsExtensionMethod(this MethodInfo method) => method.GetCustomAttribute<ExtensionAttribute>() != null;

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

        var baseType = method.DeclaringType?.BaseType;
        if (baseType == null)
        {
            return false;
        }

        return baseType
            // Not using BindingFlags.DeclaredOnly so will retrieve any depth lower in the hierarchy.
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic)
            .Any(m => m.Name == method.Name);
    }

    /// <summary>
    /// Returns <c>true</c> if the specified method's return type is a nullable reference type.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns><c>true</c> if the <paramref name="method"/>'s return type is a nullable reference type; <c>false</c> otherwise.</returns>
    [Pure]
    public static bool IsReturnNullableReferenceType(this MethodInfo method) => method.ReturnParameter.IsNullableReferenceType();
}