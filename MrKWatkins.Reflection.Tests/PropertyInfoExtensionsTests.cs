using System.Reflection;
using System.Text.RegularExpressions;
using MrKWatkins.Reflection.Tests.TestTypes.Properties;

namespace MrKWatkins.Reflection.Tests;

public sealed class PropertyInfoExtensionsTests
{
    [TestCaseSource(nameof(GetBaseDefinitionTestCases))]
    public void GetBaseDefinition(PropertyInfo property, PropertyInfo? expected) => property.GetBaseDefinition().Should().BeSameAs(expected);

    [Pure]
    public static IEnumerable<TestCaseData> GetBaseDefinitionTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.Abstract)), GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Abstract)));
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.Override)), GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Override)));
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.SealedOverride)), GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.SealedOverride)));
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.Virtual)), GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Virtual)));
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.New)), GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.New)));
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.NewAbstract)), GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.NewAbstract)));
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.NewVirtual)), GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.NewVirtual)));
        yield return new TestCaseData(GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Normal)), GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Normal)));
    }

    [TestCaseSource(nameof(GetVirtualityTestCases))]
    public void GetVirtuality(PropertyInfo property, Virtuality? expected) => property.GetVirtuality().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> GetVirtualityTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Normal)), null);
        yield return new TestCaseData(GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Virtual)), Virtuality.Virtual);
        yield return new TestCaseData(GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Abstract)), Virtuality.Abstract);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.Override)), Virtuality.Override);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.SealedOverride)), Virtuality.SealedOverride);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.New)), Virtuality.New);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.NewAbstract)), Virtuality.NewAbstract);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.NewVirtual)), Virtuality.NewVirtual);
    }

    [TestCaseSource(nameof(VisibilityTestCases))]
    public void GetVisibility(PropertyInfo property, Visibility? expected) => property.GetVisibility().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> VisibilityTestCases() =>
        typeof(PropertyVisibility)
            .GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Select(property =>
            {
                if (property.Name.Contains("Public"))
                {
                    return new TestCaseData(property, Visibility.Public);
                }

                return new TestCaseData(property, Regex.IsMatch(property.Name, "(?<!Private)Protected") ? Visibility.Protected : null);
            });

    [TestCase(nameof(PropertyModifiers.InitSetter), true)]
    [TestCase(nameof(PropertyModifiers.Normal), false)]
    public void HasInitSetter(string name, bool expected) => GetProperty<PropertyModifiers>(name).HasInitSetter().Should().Be(expected);

    [TestCaseSource(nameof(HasPublicOrProtectedOverloadsTestCases))]
    public void HasPublicOrProtectedOverloads(PropertyInfo property, bool expected) => property.HasPublicOrProtectedOverloads().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> HasPublicOrProtectedOverloadsTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyIndexer>(nameof(PropertyIndexer.Normal)), false);
        yield return new TestCaseData(GetProperty<PropertyIndexer>("Item"), false);

        var publicOverloaded = GetProperties<PropertyPublicOverloadedIndexer>("Item");
        yield return new TestCaseData(publicOverloaded[0], true);
        yield return new TestCaseData(publicOverloaded[1], true);

        var protectedOverloaded = GetProperties<PropertyProtectedOverloadedIndexer>("Item");
        yield return new TestCaseData(protectedOverloaded[0], true);
        yield return new TestCaseData(protectedOverloaded[1], true);

        var privateOverloaded = GetProperties<PropertyPrivateOverloadedIndexer>("Item");
        yield return new TestCaseData(privateOverloaded[0], false);
        yield return new TestCaseData(privateOverloaded[1], true);
    }

    [TestCase(nameof(PropertyVirtuality.Abstract), true)]
    [TestCase(nameof(PropertyVirtuality.Normal), false)]
    [TestCase(nameof(PropertyVirtuality.Virtual), false)]
    public void IsAbstract(string name, bool expected) => GetProperty<PropertyVirtuality>(name).IsAbstract().Should().Be(expected);

    [TestCase(nameof(PropertyVirtuality.Abstract), true)]
    [TestCase(nameof(PropertyVirtuality.Normal), false)]
    [TestCase(nameof(PropertyVirtuality.Virtual), true)]
    public void IsAbstractOrVirtual(string name, bool expected) => GetProperty<PropertyVirtuality>(name).IsAbstractOrVirtual().Should().Be(expected);

    [TestCaseSource(nameof(IsIndexerTestCases))]
    public void IsIndexer(PropertyInfo property, bool expected) => property.IsIndexer().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> IsIndexerTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyIndexer>(nameof(PropertyIndexer.Normal)), false);
        yield return new TestCaseData(GetProperty<PropertyIndexer>("Item"), true);

        var publicOverloaded = GetProperties<PropertyPublicOverloadedIndexer>("Item");
        yield return new TestCaseData(publicOverloaded[0], true);
        yield return new TestCaseData(publicOverloaded[1], true);

        var protectedOverloaded = GetProperties<PropertyProtectedOverloadedIndexer>("Item");
        yield return new TestCaseData(protectedOverloaded[0], true);
        yield return new TestCaseData(protectedOverloaded[1], true);

        var privateOverloaded = GetProperties<PropertyPrivateOverloadedIndexer>("Item");
        yield return new TestCaseData(privateOverloaded[0], true);
        yield return new TestCaseData(privateOverloaded[1], true);
    }

    [TestCaseSource(nameof(IsNewTestCases))]
    public void IsNew(PropertyInfo property, bool expected) => property.IsNew().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> IsNewTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Normal)), false);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.Abstract)), false);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.Virtual)), false);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.Override)), false);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.New)), true);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.NewAbstract)), true);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.NewVirtual)), true);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubSubClass>(nameof(PropertyVirtualitySubSubClass.New)), true);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubSubClass>(nameof(PropertyVirtualitySubSubClass.NewSubSubClass)), true);
    }

    [TestCaseSource(nameof(VisibilityTestCases))]
    public void IsProtected(PropertyInfo property, Visibility? visibility) =>
        property.IsProtected().Should().Be(visibility == Visibility.Protected);

    [TestCaseSource(nameof(VisibilityTestCases))]
    public void IsPublic(PropertyInfo property, Visibility? visibility) =>
        property.IsPublic().Should().Be(visibility == Visibility.Public);

    [TestCaseSource(nameof(VisibilityTestCases))]
    public void IsPublicOrProtected(PropertyInfo property, Visibility? visibility) =>
        property.IsPublicOrProtected().Should().Be(visibility != null);

    [TestCase(nameof(PropertyModifiers.Required), true)]
    [TestCase(nameof(PropertyModifiers.Normal), false)]
    public void IsRequired(string name, bool expected) => GetProperty<PropertyModifiers>(name).IsRequired().Should().Be(expected);

    [TestCaseSource(nameof(IsStaticTestCases))]
    public void IsStatic(PropertyInfo property, bool expected) => property.IsStatic().Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> IsStaticTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), false);
        yield return new TestCaseData(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Static)), true);
        yield return new TestCaseData(GetProperty<PropertyIndexer>("Item"), false);
    }

    [Pure]
    private static PropertyInfo GetProperty<T>(string name) =>
        typeof(T).GetProperty(name) ?? throw new InvalidOperationException($"Could not find property {name} on {typeof(T).DisplayName()}.");

    [Pure]
    private static List<PropertyInfo> GetProperties<T>(string name)
    {
        var properties = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.Name == name)
                .ToList();

        if (properties.Count == 0)
        {
            throw new InvalidOperationException($"Could not find property {name} on {typeof(T).DisplayName()}.");
        }

        return properties;
    }
}