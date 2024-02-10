namespace MrKWatkins.Reflection.Tests.TestTypes.Operators;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class InvalidCSharpOperators
{
    public void PublicInstanceMethod() => throw new NotSupportedException();

    public static void PublicStaticMethod() => throw new NotSupportedException();

    protected static void ProtectedStaticMethod() => throw new NotSupportedException();

    public static InvalidCSharpOperators op_UnsupportedOperator() => throw new NotSupportedException();

    // Incorrectly typed parameters.
    public static InvalidCSharpOperators op_Addition(byte _, byte __) => throw new NotSupportedException();
    public static byte op_Implicit(byte _) => throw new NotSupportedException();

    // Wrong number of parameters.
    public static InvalidCSharpOperators op_Equality(InvalidCSharpOperators _) => throw new NotSupportedException();
    public static InvalidCSharpOperators op_Inequality(InvalidCSharpOperators _, InvalidCSharpOperators __, InvalidCSharpOperators ___) => throw new NotSupportedException();
}