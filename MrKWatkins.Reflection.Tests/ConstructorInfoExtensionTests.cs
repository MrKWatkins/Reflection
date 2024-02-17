using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Constructors;

namespace MrKWatkins.Reflection.Tests;

public sealed class ConstructorInfoExtensionTests : TestFixture
{
    [TestCaseSource(nameof(EnumerateOverloadsTestCases))]
    public void EnumerateOverloads(ConstructorInfo constructor, ConstructorInfo[] expected) =>
        constructor.EnumerateOverloads().Should().BeEquivalentTo(expected);

    [Pure]
    public static IEnumerable<TestCaseData> EnumerateOverloadsTestCases()
    {
        var constructors = typeof(ConstructorOverloads).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var method in constructors)
        {
            yield return new TestCaseData(method, constructors.Where(m => m != method).ToArray());
        }
    }
}