using System.Reflection;

namespace MrKWatkins.Reflection.Tests.Formatting;

public abstract class ReflectionFormatterTestFixture : TestFixture
{
    [Pure]
    protected static TestCaseData CreateTestCase(MemberInfo member, string expected, string? name = null) =>
        new TestCaseData(member, expected)
            .SetArgDisplayNames(name ?? expected);
}