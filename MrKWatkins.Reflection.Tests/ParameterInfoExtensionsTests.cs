using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;

namespace MrKWatkins.Reflection.Tests;

public sealed class ParameterInfoExtensionsTests : TestFixture
{
    [TestCase(nameof(MethodParameterTypes.NormalParameter), ParameterKind.Normal)]
    [TestCase(nameof(MethodParameterTypes.InParameter), ParameterKind.In)]
    [TestCase(nameof(MethodParameterTypes.OutParameter), ParameterKind.Out)]
    [TestCase(nameof(MethodParameterTypes.RefParameter), ParameterKind.Ref)]
    [TestCase(nameof(MethodParameterTypes.ParamsArrayParameter), ParameterKind.Params)]
    [TestCase(nameof(MethodParameterTypes.ParamsIEnumerableParameter), ParameterKind.Params)]
    [TestCase(nameof(MethodParameterTypes.ParamsSpanParameter), ParameterKind.Params)]
    public void GetKind(string method, ParameterKind expected)
    {
        var parameter = GetMethod<MethodParameterTypes>(method).GetParameters()[0];
        parameter.GetKind().Should().Be(expected);
    }

    [TestCaseSource(nameof(IsNullableReferenceTypeTestCases))]
    public void IsNullableReferenceType(ParameterInfo parameter, bool expected) => parameter.IsNullableReferenceType().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> IsNullableReferenceTypeTestCases()
    {
        yield return new TestCaseData(GetMethod<MethodNullableReferenceTypes>(nameof(MethodNullableReferenceTypes.NullableReturn)).ReturnParameter, true);
        yield return new TestCaseData(GetMethod<MethodNullableReferenceTypes>(nameof(MethodNullableReferenceTypes.NonNullableReturn)).ReturnParameter, false);
    }
}