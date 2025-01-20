using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Constructors;
using MrKWatkins.Reflection.Tests.TestTypes.Types;

namespace MrKWatkins.Reflection.Tests;

public sealed class ConstructorInfoExtensionTests : TestFixture
{
    [TestCaseSource(nameof(EnumerateOverloadsTestCases))]
    public void EnumerateOverloads(ConstructorInfo constructor, ConstructorInfo[] expected) =>
        constructor.EnumerateOverloads().Should().SequenceEqual(expected);

    [Pure]
    public static IEnumerable<TestCaseData> EnumerateOverloadsTestCases()
    {
        var constructors = typeof(ConstructorOverloads).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var method in constructors)
        {
            yield return new TestCaseData(method, constructors.Where(m => m != method).ToArray());
        }
    }

    [TestCaseSource(nameof(HasPublicOrProtectedOverloadsTestCases))]
    public void HasPublicOrProtectedOverloads(ConstructorInfo constructor, bool expected) => constructor.HasPublicOrProtectedOverloads().Should().Equal(expected);

    [Pure]
    public static IEnumerable<TestCaseData> HasPublicOrProtectedOverloadsTestCases()
    {
        yield return new TestCaseData(GetConstructor<Class>(0), false);

        var publicOverloaded = GetConstructors<ConstructorPublicOverload>();
        yield return new TestCaseData(publicOverloaded[0], true);
        yield return new TestCaseData(publicOverloaded[1], true);

        var protectedOverloaded = GetConstructors<ConstructorProtectedOverload>();
        yield return new TestCaseData(protectedOverloaded[0], true);
        yield return new TestCaseData(protectedOverloaded[1], true);

        var privateOverloaded = GetConstructors<ConstructorPrivateOverload>();
        yield return new TestCaseData(privateOverloaded[0], false);
        yield return new TestCaseData(privateOverloaded[1], true);
    }
}