using MrKWatkins.Reflection.Tests.TestTypes.Methods;

namespace MrKWatkins.Reflection.Tests;

public sealed class ParameterInfoExtensionsTests : TestFixture
{
    [TestCase(nameof(MethodParameterTypes.NormalParameter), null)]
    [TestCase(nameof(MethodParameterTypes.InParameter), ParameterKind.In)]
    [TestCase(nameof(MethodParameterTypes.OutParameter), ParameterKind.Out)]
    [TestCase(nameof(MethodParameterTypes.RefParameter), ParameterKind.Ref)]
    [TestCase(nameof(MethodParameterTypes.ParamsParameter), ParameterKind.Params)]
    public void GetKind(string method, ParameterKind? expected)
    {
        var parameter = GetMethod<MethodParameterTypes>(method).GetParameters()[0];
        parameter.GetKind().Should().Be(expected);
    }
}