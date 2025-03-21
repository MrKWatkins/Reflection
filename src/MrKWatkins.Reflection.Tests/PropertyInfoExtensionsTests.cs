using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Properties;

namespace MrKWatkins.Reflection.Tests;

public sealed class PropertyInfoExtensionsTests : TestFixture
{
    [TestCaseSource(nameof(EnumerateOverloadsTestCases))]
    public void EnumerateOverloads(PropertyInfo property, PropertyInfo[] expected) =>
        property.EnumerateOverloads().Should().SequenceEqual(expected);

    [Pure]
    public static IEnumerable<TestCaseData> EnumerateOverloadsTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), Array.Empty<PropertyInfo>());
        yield return new TestCaseData(GetIndexer<PropertyIndexerOneParameter>(), Array.Empty<PropertyInfo>());

        var publicOverloaded = GetIndexers<PropertyPublicOverloadedIndexer>();
        yield return new TestCaseData(publicOverloaded[0], new[] { publicOverloaded[1] });
        yield return new TestCaseData(publicOverloaded[1], new[] { publicOverloaded[0] });

        var protectedOverloaded = GetIndexers<PropertyProtectedOverloadedIndexer>();
        yield return new TestCaseData(protectedOverloaded[0], new[] { protectedOverloaded[1] });
        yield return new TestCaseData(protectedOverloaded[1], new[] { protectedOverloaded[0] });

        var privateOverloaded = GetIndexers<PropertyPrivateOverloadedIndexer>();
        yield return new TestCaseData(privateOverloaded[0], new[] { privateOverloaded[1] });
        yield return new TestCaseData(privateOverloaded[1], new[] { privateOverloaded[0] });
    }

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void GetAccessibility(PropertyInfo property, Accessibility expected) => property.GetAccessibility().Should().Equal(expected);

    [TestCaseSource(nameof(GetBaseDefinitionTestCases))]
    public void GetBaseDefinition(PropertyInfo property, PropertyInfo? expected) => property.GetBaseDefinition().Should().BeTheSameInstanceAs(expected);

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
    public void GetVirtuality(PropertyInfo property, Virtuality expected) => property.GetVirtuality().Should().Equal(expected);

    [Pure]
    public static IEnumerable<TestCaseData> GetVirtualityTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Normal)), Virtuality.Normal);
        yield return new TestCaseData(GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Virtual)), Virtuality.Virtual);
        yield return new TestCaseData(GetProperty<PropertyVirtuality>(nameof(PropertyVirtuality.Abstract)), Virtuality.Abstract);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.Override)), Virtuality.Override);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.SealedOverride)), Virtuality.SealedOverride);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.New)), Virtuality.New);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.NewAbstract)), Virtuality.NewAbstract);
        yield return new TestCaseData(GetProperty<PropertyVirtualitySubClass>(nameof(PropertyVirtualitySubClass.NewVirtual)), Virtuality.NewVirtual);

        yield return new TestCaseData(GetIndexer<PropertyIndexerVirtuality>("normal"), Virtuality.Normal);
        yield return new TestCaseData(GetIndexer<PropertyIndexerVirtuality>("virtual"), Virtuality.Virtual);
        yield return new TestCaseData(GetIndexer<PropertyIndexerVirtuality>("abstract"), Virtuality.Abstract);
        yield return new TestCaseData(GetIndexer<PropertyIndexerVirtualitySubClass>("override"), Virtuality.Override);
        yield return new TestCaseData(GetIndexer<PropertyIndexerVirtualitySubClass>("sealedOverride"), Virtuality.SealedOverride);
        yield return new TestCaseData(GetIndexer<PropertyIndexerVirtualitySubClass>("new"), Virtuality.New);
        yield return new TestCaseData(GetIndexer<PropertyIndexerVirtualitySubClass>("newAbstract"), Virtuality.NewAbstract);
        yield return new TestCaseData(GetIndexer<PropertyIndexerVirtualitySubClass>("newVirtual"), Virtuality.NewVirtual);
    }

    [TestCase(nameof(PropertyModifiers.InitSetter), true)]
    [TestCase(nameof(PropertyModifiers.Normal), false)]
    public void HasInitSetter(string name, bool expected) => GetProperty<PropertyModifiers>(name).HasInitSetter().Should().Equal(expected);

    [TestCaseSource(nameof(HasPublicOrProtectedOverloadsTestCases))]
    public void HasPublicOrProtectedOverloads(PropertyInfo property, bool expected) => property.HasPublicOrProtectedOverloads().Should().Equal(expected);

    [Pure]
    public static IEnumerable<TestCaseData> HasPublicOrProtectedOverloadsTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), false);
        yield return new TestCaseData(GetIndexer<PropertyIndexerOneParameter>(), false);

        var publicOverloaded = GetIndexers<PropertyPublicOverloadedIndexer>();
        yield return new TestCaseData(publicOverloaded[0], true);
        yield return new TestCaseData(publicOverloaded[1], true);

        var protectedOverloaded = GetIndexers<PropertyProtectedOverloadedIndexer>();
        yield return new TestCaseData(protectedOverloaded[0], true);
        yield return new TestCaseData(protectedOverloaded[1], true);

        var privateOverloaded = GetIndexers<PropertyPrivateOverloadedIndexer>();
        yield return new TestCaseData(privateOverloaded[0], false);
        yield return new TestCaseData(privateOverloaded[1], true);
    }

    [TestCase(nameof(PropertyVirtuality.Abstract), true)]
    [TestCase(nameof(PropertyVirtuality.Normal), false)]
    [TestCase(nameof(PropertyVirtuality.Virtual), false)]
    public void IsAbstract(string name, bool expected) => GetProperty<PropertyVirtuality>(name).IsAbstract().Should().Equal(expected);

    [TestCase(nameof(PropertyVirtuality.Abstract), true)]
    [TestCase(nameof(PropertyVirtuality.Normal), false)]
    [TestCase(nameof(PropertyVirtuality.Virtual), true)]
    public void IsAbstractOrVirtual(string name, bool expected) => GetProperty<PropertyVirtuality>(name).IsAbstractOrVirtual().Should().Equal(expected);

    [TestCaseSource(nameof(IsIndexerTestCases))]
    public void IsIndexer(PropertyInfo property, bool expected) => property.IsIndexer().Should().Equal(expected);

    [Pure]
    public static IEnumerable<TestCaseData> IsIndexerTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), false);
        yield return new TestCaseData(GetIndexer<PropertyIndexerOneParameter>(), true);

        var publicOverloaded = GetIndexers<PropertyPublicOverloadedIndexer>();
        yield return new TestCaseData(publicOverloaded[0], true);
        yield return new TestCaseData(publicOverloaded[1], true);

        var protectedOverloaded = GetIndexers<PropertyProtectedOverloadedIndexer>();
        yield return new TestCaseData(protectedOverloaded[0], true);
        yield return new TestCaseData(protectedOverloaded[1], true);

        var privateOverloaded = GetIndexers<PropertyPrivateOverloadedIndexer>();
        yield return new TestCaseData(privateOverloaded[0], true);
        yield return new TestCaseData(privateOverloaded[1], true);
    }

    [TestCaseSource(nameof(IsNewTestCases))]
    public void IsNew(PropertyInfo property, bool expected) => property.IsNew().Should().Equal(expected);

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

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsProtected(PropertyInfo property, Accessibility visibility) =>
        property.IsProtected().Should().Equal(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublic(PropertyInfo property, Accessibility visibility) =>
        property.IsPublic().Should().Equal(visibility == Accessibility.Public);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublicOrProtected(PropertyInfo property, Accessibility visibility) =>
        property.IsPublicOrProtected().Should().Equal(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCase(nameof(PropertyModifiers.Required), true)]
    [TestCase(nameof(PropertyModifiers.Normal), false)]
    public void IsRequired(string name, bool expected) => GetProperty<PropertyModifiers>(name).IsRequired().Should().Equal(expected);

    [TestCaseSource(nameof(IsStaticTestCases))]
    public void IsStatic(PropertyInfo property, bool expected) => property.IsStatic().Should().Equal(expected);

    [Pure]
    public static IEnumerable<TestCaseData> IsStaticTestCases()
    {
        yield return new TestCaseData(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), false);
        yield return new TestCaseData(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Static)), true);
        yield return new TestCaseData(GetIndexer<PropertyIndexerOneParameter>(), false);
    }

    [Pure]
    public static IEnumerable<TestCaseData> AccessibilityTestCases()
    {
        TestCaseData CreateTestCase(string name, Accessibility expected) => new TestCaseData(GetProperty<PropertyAccessibility>(name), expected).SetArgDisplayNames(name);

        yield return CreateTestCase("PublicGetPublicSet", Accessibility.Public);
        yield return CreateTestCase("PublicGetProtectedSet", Accessibility.Public);
        yield return CreateTestCase("PublicGetInternalSet", Accessibility.Public);
        yield return CreateTestCase("PublicGetProtectedInternalSet", Accessibility.Public);
        yield return CreateTestCase("PublicGetPrivateProtectedSet", Accessibility.Public);
        yield return CreateTestCase("PublicGetPrivateSet", Accessibility.Public);
        yield return CreateTestCase("PublicGetNoSet", Accessibility.Public);
        yield return CreateTestCase("ProtectedGetPublicSet", Accessibility.Public);
        yield return CreateTestCase("ProtectedGetProtectedSet", Accessibility.Protected);
        yield return CreateTestCase("ProtectedGetProtectedInternalSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("ProtectedGetPrivateProtectedSet", Accessibility.Protected);
        yield return CreateTestCase("ProtectedGetPrivateSet", Accessibility.Protected);
        yield return CreateTestCase("ProtectedGetNoSet", Accessibility.Protected);
        yield return CreateTestCase("InternalGetPublicSet", Accessibility.Public);
        yield return CreateTestCase("InternalGetInternalSet", Accessibility.Internal);
        yield return CreateTestCase("InternalGetProtectedInternalSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("InternalGetPrivateProtectedSet", Accessibility.Internal);
        yield return CreateTestCase("InternalGetPrivateSet", Accessibility.Internal);
        yield return CreateTestCase("InternalGetNoSet", Accessibility.Internal);
        yield return CreateTestCase("ProtectedInternalGetPublicSet", Accessibility.Public);
        yield return CreateTestCase("ProtectedInternalGetProtectedSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("ProtectedInternalGetInternalSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("ProtectedInternalGetProtectedInternalSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("ProtectedInternalGetPrivateProtectedSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("ProtectedInternalGetPrivateSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("ProtectedInternalGetNoSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("PrivateProtectedGetPublicSet", Accessibility.Public);
        yield return CreateTestCase("PrivateProtectedGetProtectedSet", Accessibility.Protected);
        yield return CreateTestCase("PrivateProtectedGetInternalSet", Accessibility.Internal);
        yield return CreateTestCase("PrivateProtectedGetProtectedInternalSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("PrivateProtectedGetPrivateProtectedSet", Accessibility.PrivateProtected);
        yield return CreateTestCase("PrivateProtectedGetPrivateSet", Accessibility.PrivateProtected);
        yield return CreateTestCase("PrivateProtectedGetNoSet", Accessibility.PrivateProtected);
        yield return CreateTestCase("PrivateGetPublicSet", Accessibility.Public);
        yield return CreateTestCase("PrivateGetProtectedSet", Accessibility.Protected);
        yield return CreateTestCase("PrivateGetInternalSet", Accessibility.Internal);
        yield return CreateTestCase("PrivateGetProtectedInternalSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("PrivateGetPrivateProtectedSet", Accessibility.PrivateProtected);
        yield return CreateTestCase("PrivateGetPrivateSet", Accessibility.Private);
        yield return CreateTestCase("PrivateGetNoSet", Accessibility.Private);
        yield return CreateTestCase("NoGetPublicSet", Accessibility.Public);
        yield return CreateTestCase("NoGetProtectedSet", Accessibility.Protected);
        yield return CreateTestCase("NoGetInternalSet", Accessibility.Internal);
        yield return CreateTestCase("NoGetProtectedInternalSet", Accessibility.ProtectedInternal);
        yield return CreateTestCase("NoGetPrivateProtectedSet", Accessibility.PrivateProtected);
        yield return CreateTestCase("NoGetPrivateSet", Accessibility.Private);
    }
}