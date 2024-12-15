namespace MrKWatkins.Reflection.Tests;

public sealed class CSharpOperatorExtensionsTests
{
    [TestCase(CSharpOperator.Addition, "op_Addition")]
    [TestCase(CSharpOperator.CheckedDecrement, "op_CheckedDecrement")]
    public void ToMethodName(CSharpOperator @operator, string expected) => @operator.ToMethodName().Should().Be(expected);
}