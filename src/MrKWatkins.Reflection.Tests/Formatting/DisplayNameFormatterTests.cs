using System.Reflection;
using MrKWatkins.Reflection.Formatting;
using MrKWatkins.Reflection.Tests.TestTypes.Events;
using MrKWatkins.Reflection.Tests.TestTypes.Fields;
using MrKWatkins.Reflection.Tests.TestTypes.Methods;
using MrKWatkins.Reflection.Tests.TestTypes.Operators;
using MrKWatkins.Reflection.Tests.TestTypes.Properties;
using MrKWatkins.Reflection.Tests.TestTypes.Types;

namespace MrKWatkins.Reflection.Tests.Formatting;

// TODO: Explicit interface implementations.
public sealed class DisplayNameFormatterTests : ReflectionFormatterTestFixture
{
    [TestCaseSource(nameof(Format_DefaultTestCases))]
    public void Format_Default(MemberInfo member, string expected) => new DisplayNameFormatter().Format(member).Should().Equal(expected);

    [Pure]
    public static IEnumerable<TestCaseData> Format_DefaultTestCases()
    {
        #region Constructors

        yield return CreateTestCase(GetConstructor(typeof(Dictionary<,>), [typeof(int)]), "Dictionary<TKey, TValue>.Dictionary(Int32)");
        yield return CreateTestCase(GetConstructor(typeof(Dictionary<byte, long>), [typeof(int)]), "Dictionary<Byte, Int64>.Dictionary(Int32)");
        yield return CreateTestCase(GetConstructor(typeof(Dictionary<,>), [typeof(IDictionary<,>).MakeGenericType(typeof(Dictionary<,>).GetGenericArguments()), typeof(IEqualityComparer<>).MakeGenericType(typeof(Dictionary<,>).GetGenericArguments()[0])]), "Dictionary<TKey, TValue>.Dictionary(IDictionary<TKey, TValue>, IEqualityComparer<TKey>)");
        yield return CreateTestCase(GetConstructor(typeof(Dictionary<byte, long>), [typeof(IDictionary<byte, long>), typeof(IEqualityComparer<byte>)]), "Dictionary<Byte, Int64>.Dictionary(IDictionary<Byte, Int64>, IEqualityComparer<Byte>)");
        yield return CreateTestCase(GetConstructor<Nested>(0), "Nested.Nested()");
        yield return CreateTestCase(GetConstructor(typeof(Nested<>), 0), "Nested<T>.Nested()");
        yield return CreateTestCase(GetConstructor(typeof(Nested<int>), 0), "Nested<Int32>.Nested()");

        #endregion

        #region Events

        yield return CreateTestCase(GetEvent<EventAccessibility>(nameof(EventAccessibility.Public)), "EventAccessibility.Public");

        #endregion

        #region Fields

        yield return CreateTestCase(GetField<FieldModifiers>(nameof(FieldModifiers.Const)), "FieldModifiers.Const");
        yield return CreateTestCase(GetField<FieldModifiers>(nameof(FieldModifiers.Instance)), "FieldModifiers.Instance");
        yield return CreateTestCase(GetField<FieldModifiers>(nameof(FieldModifiers.Static)), "FieldModifiers.Static");

        #endregion

        #region Methods

        yield return CreateTestCase(GetMethod<object>(nameof(ToString)), "Object.ToString()");
        yield return CreateTestCase(GetMethod<MethodParameterTypes>(nameof(MethodParameterTypes.ArrayParameter)), "MethodParameterTypes.ArrayParameter(Int32[])");
        yield return CreateTestCase(GetMethod<MethodParameterTypes>(nameof(MethodParameterTypes.MultidimensionalArrayParameter)), "MethodParameterTypes.MultidimensionalArrayParameter(Int32[,])");
        yield return CreateTestCase(GetMethod<MethodParameterTypes>(nameof(MethodParameterTypes.OutParameter)), "MethodParameterTypes.OutParameter(Int32)");
        yield return CreateTestCase(GetMethod<MethodParameterTypes>(nameof(MethodParameterTypes.PointerParameter)), "MethodParameterTypes.PointerParameter(Int32*)");
        yield return CreateTestCase(GetMethod<MethodParameterTypes>(nameof(MethodParameterTypes.RefParameter)), "MethodParameterTypes.RefParameter(Int32)");
        yield return CreateTestCase(GetMethod<Nested>(nameof(Nested.GenericMethodOneParameter)), "Nested.GenericMethodOneParameter<T1>(T1)");
        yield return CreateTestCase(GetMethod<Nested>(nameof(Nested.GenericMethodTwoParameters)), "Nested.GenericMethodTwoParameters<T1, T2>(T1, T2)");
        yield return CreateTestCase(GetMethod<Nested.Child>(nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested.Child.ChildGenericMethodOneParameter<T1>(T1)");
        yield return CreateTestCase(GetMethod<Nested.Child>(nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested.Child.ChildGenericMethodTwoParameters<T1, T2>(T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested.Child<TC1>.ChildGenericMethodOneParameter<T1>(TC1, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<int>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested.Child<Int32>.ChildGenericMethodOneParameter<T1>(Int32, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<int>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "Nested.Child<Int32>.ChildGenericMethodOneParameter<Single>(Int32, Single)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested.Child<TC1>.ChildGenericMethodTwoParameters<T1, T2>(TC1, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<int>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested.Child<Int32>.ChildGenericMethodTwoParameters<T1, T2>(Int32, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<int>), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float), typeof(double)), "Nested.Child<Int32>.ChildGenericMethodTwoParameters<Single, Double>(Int32, Single, Double)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<,>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested.Child<TC1, TC2>.ChildGenericMethodOneParameter<T1>(TC1, TC2, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<int, long>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "Nested.Child<Int32, Int64>.ChildGenericMethodOneParameter<Single>(Int32, Int64, Single)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<,>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested.Child<TC1, TC2>.ChildGenericMethodTwoParameters<T1, T2>(TC1, TC2, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested.Child<int, long>), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float), typeof(double)), "Nested.Child<Int32, Int64>.ChildGenericMethodTwoParameters<Single, Double>(Int32, Int64, Single, Double)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>), nameof(Nested.GenericMethodOneParameter)), "Nested<T>.GenericMethodOneParameter<T1>(T, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>), nameof(Nested.GenericMethodOneParameter)), "Nested<Int16>.GenericMethodOneParameter<T1>(Int16, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>), nameof(Nested.GenericMethodTwoParameters)), "Nested<T>.GenericMethodTwoParameters<T1, T2>(T, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>), nameof(Nested.GenericMethodTwoParameters)), "Nested<Int16>.GenericMethodTwoParameters<T1, T2>(Int16, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<T>.Child.ChildGenericMethodOneParameter<T1>(T, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<Int16>.Child.ChildGenericMethodOneParameter<T1>(Int16, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "Nested<Int16>.Child.ChildGenericMethodOneParameter<Single>(Int16, Single)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<T>.Child.ChildGenericMethodTwoParameters<T1, T2>(T, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<Int16>.Child.ChildGenericMethodTwoParameters<T1, T2>(Int16, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float), typeof(double)), "Nested<Int16>.Child.ChildGenericMethodTwoParameters<Single, Double>(Int16, Single, Double)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child<>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<T>.Child<TC1>.ChildGenericMethodOneParameter<T1>(T, TC1, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child<int>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<Int16>.Child<Int32>.ChildGenericMethodOneParameter<T1>(Int16, Int32, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child<int>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "Nested<Int16>.Child<Int32>.ChildGenericMethodOneParameter<Single>(Int16, Int32, Single)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child<>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<T>.Child<TC1>.ChildGenericMethodTwoParameters<T1, T2>(T, TC1, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child<int>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<Int16>.Child<Int32>.ChildGenericMethodTwoParameters<T1, T2>(Int16, Int32, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child<int>), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float), typeof(double)), "Nested<Int16>.Child<Int32>.ChildGenericMethodTwoParameters<Single, Double>(Int16, Int32, Single, Double)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child<,>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<T>.Child<TC1, TC2>.ChildGenericMethodOneParameter<T1>(T, TC1, TC2, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child<int, long>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<Int16>.Child<Int32, Int64>.ChildGenericMethodOneParameter<T1>(Int16, Int32, Int64, T1)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child<int, long>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "Nested<Int16>.Child<Int32, Int64>.ChildGenericMethodOneParameter<Single>(Int16, Int32, Int64, Single)");
        yield return CreateTestCase(GetMethod(typeof(Nested<>.Child<,>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<T>.Child<TC1, TC2>.ChildGenericMethodTwoParameters<T1, T2>(T, TC1, TC2, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child<int, long>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<Int16>.Child<Int32, Int64>.ChildGenericMethodTwoParameters<T1, T2>(Int16, Int32, Int64, T1, T2)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short>.Child<int, long>), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float), typeof(double)), "Nested<Int16>.Child<Int32, Int64>.ChildGenericMethodTwoParameters<Single, Double>(Int16, Int32, Int64, Single, Double)");
        yield return CreateTestCase(GetMethod(typeof(Nested<short?>.Child<int?, long?>), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float?), typeof(double?)), "Nested<Int16?>.Child<Int32?, Int64?>.ChildGenericMethodTwoParameters<Single?, Double?>(Int16?, Int32?, Int64?, Single?, Double?)");

        #endregion

        #region Operators

        yield return CreateTestCase(GetOperator<CSharpOperators>(CSharpOperator.Decrement), "CSharpOperators.Decrement(CSharpOperators)");
        yield return CreateTestCase(GetOperator<CSharpOperators>(CSharpOperator.Explicit), "CSharpOperators.Explicit(String)");

        #endregion

        #region Properties

        yield return CreateTestCase(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), "PropertyModifiers.Normal");
        yield return CreateTestCase(GetIndexer<PropertyIndexerOneParameter>(), "PropertyIndexerOneParameter.Item[Int32]");
        yield return CreateTestCase(GetIndexer<PropertyIndexerTwoParameters>(), "PropertyIndexerTwoParameters.Item[Int32, String]");

        #endregion

        #region Types

        yield return CreateTestCase(typeof(string), "String");
        yield return CreateTestCase(typeof(string).MakePointerType(), "String*");
        yield return CreateTestCase(typeof(string).MakeByRefType(), "String", "ref String");
        yield return CreateTestCase(typeof(string[]), "String[]");
        yield return CreateTestCase(typeof(string[,]), "String[,]");
        yield return CreateTestCase(typeof(int?), "Int32?");
        yield return CreateTestCase(typeof(IEnumerable<>), "IEnumerable<T>");
        yield return CreateTestCase(typeof(IEnumerable<string>), "IEnumerable<String>");
        yield return CreateTestCase(typeof(Dictionary<,>), "Dictionary<TKey, TValue>");
        yield return CreateTestCase(typeof(Dictionary<int, string>), "Dictionary<Int32, String>");
        yield return CreateTestCase(typeof(Dictionary<string, int?>), "Dictionary<String, Int32?>");
        yield return CreateTestCase(typeof(Nested.Child), "Nested.Child");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild), "Nested.Child.GrandChild");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild<>), "Nested.Child.GrandChild<TG1>");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild<int>), "Nested.Child.GrandChild<Int32>");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild<,>), "Nested.Child.GrandChild<TG1, TG2>");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild<int, byte>), "Nested.Child.GrandChild<Int32, Byte>");
        yield return CreateTestCase(typeof(Nested.Child<>), "Nested.Child<TC1>");
        yield return CreateTestCase(typeof(Nested.Child<>.GrandChild), "Nested.Child<TC1>.GrandChild");
        yield return CreateTestCase(typeof(Nested.Child<>.GrandChild<>), "Nested.Child<TC1>.GrandChild<TG1>");
        yield return CreateTestCase(typeof(Nested.Child<>.GrandChild<,>), "Nested.Child<TC1>.GrandChild<TG1, TG2>");
        yield return CreateTestCase(typeof(Nested.Child<int>), "Nested.Child<Int32>");
        yield return CreateTestCase(typeof(Nested.Child<int>.GrandChild), "Nested.Child<Int32>.GrandChild");
        yield return CreateTestCase(typeof(Nested.Child<int>.GrandChild<int>), "Nested.Child<Int32>.GrandChild<Int32>");
        yield return CreateTestCase(typeof(Nested.Child<int>.GrandChild<int, byte>), "Nested.Child<Int32>.GrandChild<Int32, Byte>");
        yield return CreateTestCase(typeof(Nested.Child<,>), "Nested.Child<TC1, TC2>");
        yield return CreateTestCase(typeof(Nested.Child<,>.GrandChild), "Nested.Child<TC1, TC2>.GrandChild");
        yield return CreateTestCase(typeof(Nested.Child<,>.GrandChild<>), "Nested.Child<TC1, TC2>.GrandChild<TG1>");
        yield return CreateTestCase(typeof(Nested.Child<,>.GrandChild<,>), "Nested.Child<TC1, TC2>.GrandChild<TG1, TG2>");
        yield return CreateTestCase(typeof(Nested.Child<int, byte>), "Nested.Child<Int32, Byte>");
        yield return CreateTestCase(typeof(Nested.Child<int, byte>.GrandChild), "Nested.Child<Int32, Byte>.GrandChild");
        yield return CreateTestCase(typeof(Nested.Child<int, byte>.GrandChild<int>), "Nested.Child<Int32, Byte>.GrandChild<Int32>");
        yield return CreateTestCase(typeof(Nested.Child<int, byte>.GrandChild<int, byte>), "Nested.Child<Int32, Byte>.GrandChild<Int32, Byte>");
        yield return CreateTestCase(typeof(Nested<>.Child), "Nested<T>.Child");
        yield return CreateTestCase(typeof(Nested<>.Child.GrandChild), "Nested<T>.Child.GrandChild");
        yield return CreateTestCase(typeof(Nested<>.Child.GrandChild<>), "Nested<T>.Child.GrandChild<TG1>");
        yield return CreateTestCase(typeof(Nested<>.Child.GrandChild<,>), "Nested<T>.Child.GrandChild<TG1, TG2>");
        yield return CreateTestCase(typeof(Nested<>.Child<>), "Nested<T>.Child<TC1>");
        yield return CreateTestCase(typeof(Nested<>.Child<>.GrandChild), "Nested<T>.Child<TC1>.GrandChild");
        yield return CreateTestCase(typeof(Nested<>.Child<>.GrandChild<>), "Nested<T>.Child<TC1>.GrandChild<TG1>");
        yield return CreateTestCase(typeof(Nested<>.Child<>.GrandChild<,>), "Nested<T>.Child<TC1>.GrandChild<TG1, TG2>");
        yield return CreateTestCase(typeof(Nested<>.Child<,>), "Nested<T>.Child<TC1, TC2>");
        yield return CreateTestCase(typeof(Nested<>.Child<,>.GrandChild), "Nested<T>.Child<TC1, TC2>.GrandChild");
        yield return CreateTestCase(typeof(Nested<>.Child<,>.GrandChild<>), "Nested<T>.Child<TC1, TC2>.GrandChild<TG1>");
        yield return CreateTestCase(typeof(Nested<>.Child<,>.GrandChild<,>), "Nested<T>.Child<TC1, TC2>.GrandChild<TG1, TG2>");
        yield return CreateTestCase(typeof(Nested<long>.Child), "Nested<Int64>.Child");
        yield return CreateTestCase(typeof(Nested<long>.Child.GrandChild), "Nested<Int64>.Child.GrandChild");
        yield return CreateTestCase(typeof(Nested<long>.Child.GrandChild<int>), "Nested<Int64>.Child.GrandChild<Int32>");
        yield return CreateTestCase(typeof(Nested<long>.Child.GrandChild<int, byte>), "Nested<Int64>.Child.GrandChild<Int32, Byte>");
        yield return CreateTestCase(typeof(Nested<long>.Child<int>), "Nested<Int64>.Child<Int32>");
        yield return CreateTestCase(typeof(Nested<long>.Child<int>.GrandChild), "Nested<Int64>.Child<Int32>.GrandChild");
        yield return CreateTestCase(typeof(Nested<long>.Child<int>.GrandChild<int>), "Nested<Int64>.Child<Int32>.GrandChild<Int32>");
        yield return CreateTestCase(typeof(Nested<long>.Child<int>.GrandChild<int, byte>), "Nested<Int64>.Child<Int32>.GrandChild<Int32, Byte>");
        yield return CreateTestCase(typeof(Nested<long>.Child<int, byte>), "Nested<Int64>.Child<Int32, Byte>");
        yield return CreateTestCase(typeof(Nested<long>.Child<int, byte>.GrandChild), "Nested<Int64>.Child<Int32, Byte>.GrandChild");
        yield return CreateTestCase(typeof(Nested<long>.Child<int, byte>.GrandChild<int>), "Nested<Int64>.Child<Int32, Byte>.GrandChild<Int32>");
        yield return CreateTestCase(typeof(Nested<long>.Child<int, byte>.GrandChild<int, byte>), "Nested<Int64>.Child<Int32, Byte>.GrandChild<Int32, Byte>");

        #endregion
    }

    [TestCaseSource(nameof(Format_OptionsTestCases))]
    public void Format_Options(DisplayNameFormatterOptions options, MemberInfo member, string expected) =>
        new DisplayNameFormatter(options).Format(member).Should().Equal(expected);

    [Pure]
    public static IEnumerable<TestCaseData> Format_OptionsTestCases()
    {
        TestCaseData Create(DisplayNameFormatterOptions options, MemberInfo member, string expected) =>
            new TestCaseData(options, member, expected)
                .SetArgDisplayNames($"{options} {expected}");

        var doNotPrefixMembersWithType = new DisplayNameFormatterOptions { PrefixMembersWithType = false };

        yield return Create(doNotPrefixMembersWithType, typeof(string), "String");
        yield return Create(doNotPrefixMembersWithType, typeof(int?), "Int32?");
        yield return Create(doNotPrefixMembersWithType, typeof(IEnumerable<int?>), "IEnumerable<Int32?>");
        yield return Create(doNotPrefixMembersWithType, GetConstructor(typeof(Dictionary<string, int?>), [typeof(int)]), "Dictionary(Int32)");
        yield return Create(doNotPrefixMembersWithType, GetMethod(typeof(Nested.Child<int?, long?>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float?)), "ChildGenericMethodOneParameter<Single?>(Int32?, Int64?, Single?)");

        var qualified = new DisplayNameFormatterOptions { UseFullyQualifiedTypes = true };

        yield return Create(qualified, typeof(string), "System.String");
        yield return Create(qualified, typeof(int?), "System.Int32?");
        yield return Create(qualified, typeof(IEnumerable<int?>), "System.Collections.Generic.IEnumerable<System.Int32?>");
        yield return Create(qualified, GetConstructor(typeof(Dictionary<string, int?>), [typeof(int)]), "System.Collections.Generic.Dictionary<System.String, System.Int32?>.Dictionary(System.Int32)");
        yield return Create(qualified, GetMethod(typeof(Nested.Child<int?, long?>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float?)), "MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child<System.Int32?, System.Int64?>.ChildGenericMethodOneParameter<System.Single?>(System.Int32?, System.Int64?, System.Single?)");

        var noQuestionMarks = new DisplayNameFormatterOptions { UseQuestionMarksForNullableTypes = false };

        yield return Create(noQuestionMarks, typeof(string), "String");
        yield return Create(noQuestionMarks, typeof(int?), "Nullable<Int32>");
        yield return Create(noQuestionMarks, typeof(IEnumerable<int?>), "IEnumerable<Nullable<Int32>>");
        yield return Create(noQuestionMarks, GetConstructor(typeof(Dictionary<string, int?>), [typeof(int)]), "Dictionary<String, Nullable<Int32>>.Dictionary(Int32)");
        yield return Create(noQuestionMarks, GetMethod(typeof(Nested.Child<int?, long?>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float?)), "Nested.Child<Nullable<Int32>, Nullable<Int64>>.ChildGenericMethodOneParameter<Nullable<Single>>(Nullable<Int32>, Nullable<Int64>, Nullable<Single>)");

        var qualifiedNoQuestionMarks = new DisplayNameFormatterOptions { UseFullyQualifiedTypes = true, UseQuestionMarksForNullableTypes = false };

        yield return Create(qualifiedNoQuestionMarks, typeof(string), "System.String");
        yield return Create(qualifiedNoQuestionMarks, typeof(int?), "System.Nullable<System.Int32>");
        yield return Create(qualifiedNoQuestionMarks, typeof(IEnumerable<int?>), "System.Collections.Generic.IEnumerable<System.Nullable<System.Int32>>");
        yield return Create(qualifiedNoQuestionMarks, GetConstructor(typeof(Dictionary<string, int?>), [typeof(int)]), "System.Collections.Generic.Dictionary<System.String, System.Nullable<System.Int32>>.Dictionary(System.Int32)");
        yield return Create(qualifiedNoQuestionMarks, GetMethod(typeof(Nested.Child<int?, long?>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float?)), "MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child<System.Nullable<System.Int32>, System.Nullable<System.Int64>>.ChildGenericMethodOneParameter<System.Nullable<System.Single>>(System.Nullable<System.Int32>, System.Nullable<System.Int64>, System.Nullable<System.Single>)");

        var keywords = new DisplayNameFormatterOptions { UseCSharpKeywordsForPrimitiveTypes = true };

        yield return Create(keywords, typeof(string), "string");
        yield return Create(keywords, typeof(int?), "int?");
        yield return Create(keywords, typeof(IEnumerable<int?>), "IEnumerable<int?>");
        yield return Create(keywords, GetConstructor(typeof(Dictionary<string, int?>), [typeof(int)]), "Dictionary<string, int?>.Dictionary(int)");
        yield return Create(keywords, GetMethod(typeof(Nested.Child<int?, long?>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float?)), "Nested.Child<int?, long?>.ChildGenericMethodOneParameter<float?>(int?, long?, float?)");

        var qualifiedNoQuestionMarksKeywords = new DisplayNameFormatterOptions { UseCSharpKeywordsForPrimitiveTypes = true, UseFullyQualifiedTypes = true, UseQuestionMarksForNullableTypes = false };

        yield return Create(qualifiedNoQuestionMarksKeywords, typeof(string), "string");
        yield return Create(qualifiedNoQuestionMarksKeywords, typeof(int?), "System.Nullable<int>");
        yield return Create(qualifiedNoQuestionMarksKeywords, typeof(IEnumerable<int?>), "System.Collections.Generic.IEnumerable<System.Nullable<int>>");
        yield return Create(qualifiedNoQuestionMarksKeywords, GetConstructor(typeof(Dictionary<string, int?>), [typeof(int)]), "System.Collections.Generic.Dictionary<string, System.Nullable<int>>.Dictionary(int)");
        yield return Create(qualifiedNoQuestionMarksKeywords, GetMethod(typeof(Nested.Child<int?, long?>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float?)), "MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child<System.Nullable<int>, System.Nullable<long>>.ChildGenericMethodOneParameter<System.Nullable<float>>(System.Nullable<int>, System.Nullable<long>, System.Nullable<float>)");
    }

    [TestCaseSource(nameof(FormatNamespaceTestCases))]
    public void FormatNamespace(MemberInfo member, string expected) => new DisplayNameFormatter().FormatNamespace(member).Should().Equal(expected);

    [Pure]
    public static IEnumerable<TestCaseData> FormatNamespaceTestCases()
    {
        yield return CreateTestCase(GetConstructor(typeof(Dictionary<,>), [typeof(int)]), "System.Collections.Generic");
        yield return CreateTestCase(GetEvent<EventAccessibility>(nameof(EventAccessibility.Public)), "MrKWatkins.Reflection.Tests.TestTypes.Events");
        yield return CreateTestCase(GetField<FieldModifiers>(nameof(FieldModifiers.Const)), "MrKWatkins.Reflection.Tests.TestTypes.Fields");
        yield return CreateTestCase(GetMethod<object>(nameof(ToString)), "System");
        yield return CreateTestCase(GetMethod<Nested>(nameof(Nested.GenericMethodOneParameter)), "MrKWatkins.Reflection.Tests.TestTypes.Types");
        yield return CreateTestCase(GetMethod<Nested.Child>(nameof(Nested.Child.ChildGenericMethodTwoParameters)), "MrKWatkins.Reflection.Tests.TestTypes.Types");
        yield return CreateTestCase(GetOperator<CSharpOperators>(CSharpOperator.Decrement), "MrKWatkins.Reflection.Tests.TestTypes.Operators");
        yield return CreateTestCase(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), "MrKWatkins.Reflection.Tests.TestTypes.Properties");
        yield return CreateTestCase(GetIndexer<PropertyIndexerOneParameter>(), "MrKWatkins.Reflection.Tests.TestTypes.Properties");
        yield return CreateTestCase(typeof(string), "System");
        yield return CreateTestCase(typeof(string).MakePointerType(), "System");
        yield return CreateTestCase(typeof(string).MakeByRefType(), "System");
        yield return CreateTestCase(typeof(string[]), "System");
        yield return CreateTestCase(typeof(string[,]), "System");
        yield return CreateTestCase(typeof(int?), "System");
        yield return CreateTestCase(typeof(Nested.Child), "MrKWatkins.Reflection.Tests.TestTypes.Types");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild), "MrKWatkins.Reflection.Tests.TestTypes.Types");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild<>), "MrKWatkins.Reflection.Tests.TestTypes.Types");
        yield return CreateTestCase(typeof(Nested.Child.GrandChild<int>), "MrKWatkins.Reflection.Tests.TestTypes.Types");
    }
}