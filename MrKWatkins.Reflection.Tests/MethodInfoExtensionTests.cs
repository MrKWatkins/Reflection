using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;
using MrKWatkins.Reflection.Tests.TestTypes.Operators;

namespace MrKWatkins.Reflection.Tests;

public sealed class MethodInfoExtensionTests : TestFixture
{
    [TestCaseSource(nameof(EnumerateOverloadsTestCases))]
    public void EnumerateOverloads(MethodInfo method, MethodInfo[] expected) =>
        method.EnumerateOverloads().Should().BeEquivalentTo(expected);

    [Pure]
    public static IEnumerable<TestCaseData> EnumerateOverloadsTestCases()
    {
        var methods = GetMethods<MethodOverloads>(nameof(MethodOverloads.Overload));
        foreach (var method in methods)
        {
            yield return new TestCaseData(method, methods.Where(m => m != method).ToArray());
        }
    }

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

    [TestCaseSource(nameof(GetVirtualityTestCases))]
    public void GetVirtuality(MethodInfo method, Virtuality expected) => method.GetVirtuality().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> GetVirtualityTestCases()
    {
        yield return new TestCaseData(GetMethod<MethodVirtuality>(nameof(MethodVirtuality.Normal)), Virtuality.Normal);
        yield return new TestCaseData(GetMethod<MethodVirtuality>(nameof(MethodVirtuality.Virtual)), Virtuality.Virtual);
        yield return new TestCaseData(GetMethod<MethodVirtuality>(nameof(MethodVirtuality.Abstract)), Virtuality.Abstract);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.Override)), Virtuality.Override);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.SealedOverride)), Virtuality.SealedOverride);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.New)), Virtuality.New);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.NewAbstract)), Virtuality.NewAbstract);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.NewVirtual)), Virtuality.NewVirtual);
    }

    [TestCaseSource(nameof(IsNewTestCases))]
    public void IsNew(MethodInfo method, bool expected) => method.IsNew().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> IsNewTestCases()
    {
        yield return new TestCaseData(GetMethod<MethodVirtuality>(nameof(MethodVirtuality.Normal)), false);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.Abstract)), false);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.Virtual)), false);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.Override)), false);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.New)), true);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.NewAbstract)), true);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubClass>(nameof(MethodVirtualitySubClass.NewVirtual)), true);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubSubClass>(nameof(MethodVirtualitySubSubClass.New)), true);
        yield return new TestCaseData(GetMethod<MethodVirtualitySubSubClass>(nameof(MethodVirtualitySubSubClass.NewSubSubClass)), true);
    }
}