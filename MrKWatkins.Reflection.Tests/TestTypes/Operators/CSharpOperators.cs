namespace MrKWatkins.Reflection.Tests.TestTypes.Operators;

// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading#overloadable-operators
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed class CSharpOperators
{
    // Cast operators.
    public static implicit operator string(CSharpOperators _) => throw new NotSupportedException();
    public static explicit operator CSharpOperators(string _) => throw new NotSupportedException();

    // Unary.
    public static CSharpOperators operator +(CSharpOperators _) => throw new NotSupportedException();
    public static CSharpOperators operator -(CSharpOperators _) => throw new NotSupportedException();
    public static CSharpOperators operator checked -(CSharpOperators _) => throw new NotSupportedException();
    public static CSharpOperators operator !(CSharpOperators _) => throw new NotSupportedException();
    public static CSharpOperators operator ~(CSharpOperators _) => throw new NotSupportedException();
    public static CSharpOperators operator ++(CSharpOperators _) => throw new NotSupportedException();
    public static CSharpOperators operator checked ++(CSharpOperators _) => throw new NotSupportedException();
    public static CSharpOperators operator --(CSharpOperators _) => throw new NotSupportedException();
    public static CSharpOperators operator checked --(CSharpOperators _) => throw new NotSupportedException();
    public static bool operator true(CSharpOperators _) => throw new NotSupportedException();
    public static bool operator false(CSharpOperators _) => throw new NotSupportedException();

    // Arithmetic.
    public static CSharpOperators operator +(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator checked +(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator -(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator checked -(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator *(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator checked *(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator /(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator checked /(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator %(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator &(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator |(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator ^(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator <<(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator >>(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator >>>(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();

    // Equality/comparison.
    public static CSharpOperators operator ==(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator !=(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator <(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator >(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator <=(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();
    public static CSharpOperators operator >=(CSharpOperators _, CSharpOperators __) => throw new NotSupportedException();

    public override bool Equals(object? obj) => throw new NotSupportedException();
    public override int GetHashCode() => throw new NotSupportedException();
}