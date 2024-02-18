using System.Reflection;
using MrKWatkins.Reflection.Formatting;
using MrKWatkins.Reflection.Tests.TestTypes.Events;
using MrKWatkins.Reflection.Tests.TestTypes.Fields;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;
using MrKWatkins.Reflection.Tests.TestTypes.Operators;
using MrKWatkins.Reflection.Tests.TestTypes.Properties;
using MrKWatkins.Reflection.Tests.TestTypes.Types;

namespace MrKWatkins.Reflection.Tests.Formatting;

public sealed class XmlDocIdFormatterTests : ReflectionFormatterTestFixture
{
    [TestCaseSource(nameof(Format_ThrowsOnInvalidTypeTestCases))]
    public void Format_ThrowsOnInvalidType(MemberInfo member, string expectedMessage) =>
        new XmlDocIdFormatter().Invoking(f => f.Format(member)).Should().Throw<ArgumentException>().WithMessage(expectedMessage);

    [Pure]
    public static IEnumerable<TestCaseData> Format_ThrowsOnInvalidTypeTestCases()
    {
        yield return new TestCaseData(typeof(string).MakePointerType(), "Type String* is a pointer type; pointer types do not have XmlDocIds.*");
        yield return new TestCaseData(typeof(string).MakeByRefType(), "Type String& is a by ref type; by ref types do not have XmlDocIds.*");
        yield return new TestCaseData(typeof(string[]), "Type String[] is an array type; array types do not have XmlDocIds.*");
        yield return new TestCaseData(typeof(string[,]), "Type String[,] is an array type; array types do not have XmlDocIds.*");
        yield return new TestCaseData(typeof(IEnumerable<string>), "Type IEnumerable<String> is a constructed generic type; constructed generic types do not have XmlDocIds.*");
        yield return new TestCaseData(GetMethod(typeof(Nested.Child<int>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Type Nested.Child<Int32> is a constructed generic type; constructed generic types do not have XmlDocIds.*");
    }

    [TestCaseSource(nameof(FormatTestCases))]
    public void Format(MemberInfo member, string expected) => new XmlDocIdFormatter().Format(member).Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> FormatTestCases()
    {
        #region Constructors

        yield return CreateTestCase(GetConstructor(typeof(Dictionary<,>), [typeof(int)]), "M:System.Collections.Generic.Dictionary`2.#ctor(System.Int32)");
        yield return CreateTestCase(GetConstructor(typeof(Dictionary<,>), [typeof(IDictionary<,>).MakeGenericType(typeof(Dictionary<,>).GetGenericArguments()), typeof(IEqualityComparer<>).MakeGenericType(typeof(Dictionary<,>).GetGenericArguments()[0])]), "M:System.Collections.Generic.Dictionary`2.#ctor(System.Collections.Generic.IDictionary{`0,`1},System.Collections.Generic.IEqualityComparer{`0})");
        yield return CreateTestCase(GetConstructor<Nested>(0), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.#ctor");
        yield return CreateTestCase(GetConstructor(typeof(Nested<>), 0), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.#ctor");

        #endregion

        #region Events

        yield return CreateTestCase(GetEvent<EventAccessibility>(nameof(EventAccessibility.Public)), "E:MrKWatkins.Reflection.Tests.TestTypes.Events.EventAccessibility.Public");

        #endregion

        #region Fields

        yield return CreateTestCase(GetField<FieldModifiers>(nameof(FieldModifiers.Const)), "F:MrKWatkins.Reflection.Tests.TestTypes.Fields.FieldModifiers.Const");
        yield return CreateTestCase(GetField<FieldModifiers>(nameof(FieldModifiers.Instance)), "F:MrKWatkins.Reflection.Tests.TestTypes.Fields.FieldModifiers.Instance");
        yield return CreateTestCase(GetField<FieldModifiers>(nameof(FieldModifiers.Static)), "F:MrKWatkins.Reflection.Tests.TestTypes.Fields.FieldModifiers.Static");

        #endregion

        #region Methods

        yield return CreateTestCase(GetMethod<object>(nameof(ToString)), "M:System.Object.ToString");
        yield return CreateTestCase(GetMethod<MethodParameterTypes>(nameof(MethodParameterTypes.ArrayParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Methods.MethodParameterTypes.ArrayParameter(System.Int32[])");
        yield return CreateTestCase(GetMethod<MethodParameterTypes>(nameof(MethodParameterTypes.MultidimensionalArrayParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Methods.MethodParameterTypes.MultidimensionalArrayParameter(System.Int32[,])");
        yield return CreateTestCase(GetMethod<MethodParameterTypes>(nameof(MethodParameterTypes.OutParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Methods.MethodParameterTypes.OutParameter(System.Int32@)");
        yield return CreateTestCase(GetMethod<MethodParameterTypes>(nameof(MethodParameterTypes.PointerParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Methods.MethodParameterTypes.PointerParameter(System.Int32*)");
        yield return CreateTestCase(GetMethod<MethodParameterTypes>(nameof(MethodParameterTypes.RefParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Methods.MethodParameterTypes.RefParameter(System.Int32@)");
        yield return CreateTestCase(GetMethod<Nested>(nameof(Nested.GenericMethodOneParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.GenericMethodOneParameter``1(``0)");
        yield return CreateTestCase(GetMethod<Nested>(nameof(Nested.GenericMethodTwoParameters)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.GenericMethodTwoParameters``2(``0,``1)");
        yield return CreateTestCase(GetMethod<Nested.Child>(nameof(Nested.Child.ChildGenericMethodOneParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child.ChildGenericMethodOneParameter``1(``0)");
        yield return CreateTestCase(GetMethod<Nested.Child>(nameof(Nested.Child.ChildGenericMethodTwoParameters)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child.ChildGenericMethodTwoParameters``2(``0,``1)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`1.ChildGenericMethodOneParameter``1(`0,``0)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`1.ChildGenericMethodTwoParameters``2(`0,``0,``1)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<,>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`2.ChildGenericMethodOneParameter``1(`0,`1,``0)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<,>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`2.ChildGenericMethodTwoParameters``2(`0,`1,``0,``1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>), nameof(Nested.GenericMethodOneParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.GenericMethodOneParameter``1(`0,``0)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>), nameof(Nested.GenericMethodTwoParameters)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.GenericMethodTwoParameters``2(`0,``0,``1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child), nameof(Nested.Child.ChildGenericMethodOneParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child.ChildGenericMethodOneParameter``1(`0,``0)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child.ChildGenericMethodTwoParameters``2(`0,``0,``1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child<>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`1.ChildGenericMethodOneParameter``1(`0,`1,``0)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child<>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`1.ChildGenericMethodTwoParameters``2(`0,`1,``0,``1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child<,>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`2.ChildGenericMethodOneParameter``1(`0,`1,`2,``0)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child<,>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "M:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`2.ChildGenericMethodTwoParameters``2(`0,`1,`2,``0,``1)");

        #endregion

        #region Operators

        yield return CreateTestCase(GetOperator<CSharpOperators>(CSharpOperator.Decrement), "M:MrKWatkins.Reflection.Tests.TestTypes.Operators.CSharpOperators.op_Decrement(MrKWatkins.Reflection.Tests.TestTypes.Operators.CSharpOperators)");
        yield return CreateTestCase(GetOperator<CSharpOperators>(CSharpOperator.Explicit), "M:MrKWatkins.Reflection.Tests.TestTypes.Operators.CSharpOperators.op_Explicit(System.String)~MrKWatkins.Reflection.Tests.TestTypes.Operators.CSharpOperators");

        #endregion

        #region Properties

        yield return CreateTestCase(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), "P:MrKWatkins.Reflection.Tests.TestTypes.Properties.PropertyModifiers.Normal");
        yield return CreateTestCase(GetProperty<PropertyIndexerOneParameter>("Item"), "P:MrKWatkins.Reflection.Tests.TestTypes.Properties.PropertyIndexerOneParameter.Item(System.Int32)");
        yield return CreateTestCase(GetProperty<PropertyIndexerTwoParameters>("Item"), "P:MrKWatkins.Reflection.Tests.TestTypes.Properties.PropertyIndexerTwoParameters.Item(System.Int32,System.String)");

        #endregion

        #region Types

        yield return CreateTestCase(typeof(string), "T:System.String");
        yield return CreateTestCase(typeof(IEnumerable<>), "T:System.Collections.Generic.IEnumerable`1");
        yield return CreateTestCase(typeof(Dictionary<,>), "T:System.Collections.Generic.Dictionary`2");
        yield return CreateTestCase(typeof(Nested.Child), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child.GrandChild");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild<>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child.GrandChild`1");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild<,>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child.GrandChild`2");
        yield return CreateTestCase(typeof(Nested.Child<>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`1");
        yield return CreateTestCase(typeof(Nested.Child<>.GrandChild), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`1.GrandChild");
        yield return CreateTestCase(typeof(Nested.Child<>.GrandChild<>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`1.GrandChild`1");
        yield return CreateTestCase(typeof(Nested.Child<>.GrandChild<,>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`1.GrandChild`2");
        yield return CreateTestCase(typeof(Nested.Child<,>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`2");
        yield return CreateTestCase(typeof(Nested.Child<,>.GrandChild), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`2.GrandChild");
        yield return CreateTestCase(typeof(Nested.Child<,>.GrandChild<>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`2.GrandChild`1");
        yield return CreateTestCase(typeof(Nested.Child<,>.GrandChild<,>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child`2.GrandChild`2");
        yield return CreateTestCase(typeof(Nested<>.Child), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child");
        yield return CreateTestCase(typeof(Nested<>.Child.GrandChild), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child.GrandChild");
        yield return CreateTestCase(typeof(Nested<>.Child.GrandChild<>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child.GrandChild`1");
        yield return CreateTestCase(typeof(Nested<>.Child.GrandChild<,>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child.GrandChild`2");
        yield return CreateTestCase(typeof(Nested<>.Child<>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`1");
        yield return CreateTestCase(typeof(Nested<>.Child<>.GrandChild), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`1.GrandChild");
        yield return CreateTestCase(typeof(Nested<>.Child<>.GrandChild<>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`1.GrandChild`1");
        yield return CreateTestCase(typeof(Nested<>.Child<>.GrandChild<,>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`1.GrandChild`2");
        yield return CreateTestCase(typeof(Nested<>.Child<,>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`2");
        yield return CreateTestCase(typeof(Nested<>.Child<,>.GrandChild), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`2.GrandChild");
        yield return CreateTestCase(typeof(Nested<>.Child<,>.GrandChild<>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`2.GrandChild`1");
        yield return CreateTestCase(typeof(Nested<>.Child<,>.GrandChild<,>), "T:MrKWatkins.Reflection.Tests.TestTypes.Types.Nested`1.Child`2.GrandChild`2");

        #endregion
    }


    [TestCaseSource(nameof(FormatNamespaceTestCases))]
    public void FormatNamespace(MemberInfo member, string expected) => new XmlDocIdFormatter().FormatNamespace(member).Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> FormatNamespaceTestCases()
    {
        yield return CreateTestCase(GetConstructor(typeof(Dictionary<,>), [typeof(int)]), "N:System.Collections.Generic");
        yield return CreateTestCase(GetEvent<EventAccessibility>(nameof(EventAccessibility.Public)), "N:MrKWatkins.Reflection.Tests.TestTypes.Events");
        yield return CreateTestCase(GetField<FieldModifiers>(nameof(FieldModifiers.Const)), "N:MrKWatkins.Reflection.Tests.TestTypes.Fields");
        yield return CreateTestCase(GetMethod<object>(nameof(ToString)), "N:System");
        yield return CreateTestCase(GetMethod<Nested>(nameof(Nested.GenericMethodOneParameter)), "N:MrKWatkins.Reflection.Tests.TestTypes.Types");
        yield return CreateTestCase(GetMethod<Nested.Child>(nameof(Nested.Child.ChildGenericMethodTwoParameters)), "N:MrKWatkins.Reflection.Tests.TestTypes.Types");
        yield return CreateTestCase(GetOperator<CSharpOperators>(CSharpOperator.Decrement), "N:MrKWatkins.Reflection.Tests.TestTypes.Operators");
        yield return CreateTestCase(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), "N:MrKWatkins.Reflection.Tests.TestTypes.Properties");
        yield return CreateTestCase(GetProperty<PropertyIndexerOneParameter>("Item"), "N:MrKWatkins.Reflection.Tests.TestTypes.Properties");
        yield return CreateTestCase(typeof(string), "N:System");
        yield return CreateTestCase(typeof(string).MakePointerType(), "N:System");
        yield return CreateTestCase(typeof(string).MakeByRefType(), "N:System");
        yield return CreateTestCase(typeof(string[]), "N:System");
        yield return CreateTestCase(typeof(string[,]), "N:System");
        yield return CreateTestCase(typeof(int?), "N:System");
        yield return CreateTestCase(typeof(Nested.Child), "N:MrKWatkins.Reflection.Tests.TestTypes.Types");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild), "N:MrKWatkins.Reflection.Tests.TestTypes.Types");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild<>), "N:MrKWatkins.Reflection.Tests.TestTypes.Types");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild<int>), "N:MrKWatkins.Reflection.Tests.TestTypes.Types");
    }
}