using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Operators;

namespace MrKWatkins.Reflection.Tests;

public sealed class MethodInfoExtensionTests
{
    [TestCaseSource(nameof(CSharpOperatorTestCases))]
    public void GetCSharpOperator(MethodInfo method, CSharpOperator? expected) => method.GetCSharpOperator().Should().Be(expected);

    [TestCaseSource(nameof(CSharpOperatorTestCases))]
    public void IsOperator(MethodInfo method, CSharpOperator? expected) => method.IsCSharpOperator().Should().Be(expected.HasValue);

    [Pure]
    public static IEnumerable<TestCaseData> CSharpOperatorTestCases()
    {
        var valid = Enum.GetValues<CSharpOperator>()
            .Select(o => new TestCaseData(GetMethod<CSharpOperators>($"op_{o}"), o).SetArgDisplayNames($"Valid: {o}"));

        var invalid = typeof(InvalidCSharpOperators)
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
            .Select(method => new TestCaseData(method, null).SetArgDisplayNames($"Invalid: {method.Name}"));

        return valid.Concat(invalid);
    }

    [Pure]
    private static MethodInfo GetMethod<T>(string name) =>
        typeof(T).GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new InvalidOperationException($"Could not find method {name} on {typeof(T).Name}.");
}