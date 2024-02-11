using System.Reflection;
using MrKWatkins.Reflection.Formatting;
using MrKWatkins.Reflection.Tests.TestTypes.Events;
using MrKWatkins.Reflection.Tests.TestTypes.Fields;
using MrKWatkins.Reflection.Tests.TestTypes.Operators;
using MrKWatkins.Reflection.Tests.TestTypes.Properties;
using MrKWatkins.Reflection.Tests.TestTypes.Types;

namespace MrKWatkins.Reflection.Tests.Formatting;

// TODO: Explicit interface implementations.
public sealed class DisplayNameFormatterTests : TestFixture
{
    [TestCaseSource(nameof(Format_UnqualifiedTestCases))]
    public void Format_Unqualified(MemberInfo member, string expected) =>
        new DisplayNameFormatter(DisplayNameFormatterOptions.None).Format(member).Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> Format_UnqualifiedTestCases()
    {
        static TestCaseData Create(MemberInfo member, string expected, string? name = null) => new TestCaseData(member, expected).SetArgDisplayNames(name ?? expected);

        #region Constructors

        yield return Create(GetConstructor(typeof(Dictionary<,>), [typeof(int)]), "Dictionary<TKey, TValue>.Dictionary(Int32)");
        yield return Create(GetConstructor(typeof(Dictionary<byte, long>), [typeof(int)]), "Dictionary<Byte, Int64>.Dictionary(Int32)");
        yield return Create(GetConstructor(typeof(Dictionary<,>), [typeof(IDictionary<,>).MakeGenericType(typeof(Dictionary<,>).GetGenericArguments()), typeof(IEqualityComparer<>).MakeGenericType(typeof(Dictionary<,>).GetGenericArguments()[0])]), "Dictionary<TKey, TValue>.Dictionary(IDictionary<TKey, TValue>, IEqualityComparer<TKey>)");
        yield return Create(GetConstructor(typeof(Dictionary<byte, long>), [typeof(IDictionary<byte, long>), typeof(IEqualityComparer<byte>)]), "Dictionary<Byte, Int64>.Dictionary(IDictionary<Byte, Int64>, IEqualityComparer<Byte>)");
        yield return Create(GetConstructor<Nested>(), "Nested.Nested()");
        yield return Create(GetConstructor(typeof(Nested<>)), "Nested<T>.Nested()");
        yield return Create(GetConstructor(typeof(Nested<int>)), "Nested<Int32>.Nested()");

        #endregion

        #region Events

        yield return Create(GetEvent<EventAccessibility>(nameof(EventAccessibility.Public)), "EventAccessibility.Public");

        #endregion

        #region Fields

        yield return Create(GetField<FieldModifiers>(nameof(FieldModifiers.Const)), "FieldModifiers.Const");
        yield return Create(GetField<FieldModifiers>(nameof(FieldModifiers.Instance)), "FieldModifiers.Instance");
        yield return Create(GetField<FieldModifiers>(nameof(FieldModifiers.Static)), "FieldModifiers.Static");

        #endregion

        #region Methods

        yield return Create(GetMethod<object>(nameof(ToString)), "Object.ToString()");
        yield return Create(GetMethod<Nested>(nameof(Nested.GenericMethodOneParameter)), "Nested.GenericMethodOneParameter<T1>(T1)");
        yield return Create(GetMethod<Nested>(nameof(Nested.GenericMethodTwoParameters)), "Nested.GenericMethodTwoParameters<T1, T2>(T1, T2)");
        yield return Create(GetMethod<Nested.Child>(nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested.Child.ChildGenericMethodOneParameter<T1>(T1)");
        yield return Create(GetMethod<Nested.Child>(nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested.Child.ChildGenericMethodTwoParameters<T1, T2>(T1, T2)");
        yield return Create(GetMethod(typeof(Nested.Child<>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested.Child<TC1>.ChildGenericMethodOneParameter<T1>(TC1, T1)");
        yield return Create(GetMethod(typeof(Nested.Child<int>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested.Child<Int32>.ChildGenericMethodOneParameter<T1>(Int32, T1)");
        yield return Create(GetMethod(typeof(Nested.Child<int>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "Nested.Child<Int32>.ChildGenericMethodOneParameter<Single>(Int32, Single)");
        yield return Create(GetMethod(typeof(Nested.Child<>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested.Child<TC1>.ChildGenericMethodTwoParameters<T1, T2>(TC1, T1, T2)");
        yield return Create(GetMethod(typeof(Nested.Child<int>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested.Child<Int32>.ChildGenericMethodTwoParameters<T1, T2>(Int32, T1, T2)");
        yield return Create(GetMethod(typeof(Nested.Child<int>), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float), typeof(double)), "Nested.Child<Int32>.ChildGenericMethodTwoParameters<Single, Double>(Int32, Single, Double)");
        yield return Create(GetMethod(typeof(Nested.Child<,>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested.Child<TC1, TC2>.ChildGenericMethodOneParameter<T1>(TC1, TC2, T1)");
        yield return Create(GetMethod(typeof(Nested.Child<int, long>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "Nested.Child<Int32, Int64>.ChildGenericMethodOneParameter<Single>(Int32, Int64, Single)");
        yield return Create(GetMethod(typeof(Nested.Child<,>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested.Child<TC1, TC2>.ChildGenericMethodTwoParameters<T1, T2>(TC1, TC2, T1, T2)");
        yield return Create(GetMethod(typeof(Nested.Child<int, long>), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float), typeof(double)), "Nested.Child<Int32, Int64>.ChildGenericMethodTwoParameters<Single, Double>(Int32, Int64, Single, Double)");
        yield return Create(GetMethod(typeof(Nested<>), nameof(Nested.GenericMethodOneParameter)), "Nested<T>.GenericMethodOneParameter<T1>(T, T1)");
        yield return Create(GetMethod(typeof(Nested<short>), nameof(Nested.GenericMethodOneParameter)), "Nested<Int16>.GenericMethodOneParameter<T1>(Int16, T1)");
        yield return Create(GetMethod(typeof(Nested<>), nameof(Nested.GenericMethodTwoParameters)), "Nested<T>.GenericMethodTwoParameters<T1, T2>(T, T1, T2)");
        yield return Create(GetMethod(typeof(Nested<short>), nameof(Nested.GenericMethodTwoParameters)), "Nested<Int16>.GenericMethodTwoParameters<T1, T2>(Int16, T1, T2)");
        yield return Create(GetMethod(typeof(Nested<>.Child), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<T>.Child.ChildGenericMethodOneParameter<T1>(T, T1)");
        yield return Create(GetMethod(typeof(Nested<short>.Child), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<Int16>.Child.ChildGenericMethodOneParameter<T1>(Int16, T1)");
        yield return Create(GetMethod(typeof(Nested<short>.Child), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "Nested<Int16>.Child.ChildGenericMethodOneParameter<Single>(Int16, Single)");
        yield return Create(GetMethod(typeof(Nested<>.Child), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<T>.Child.ChildGenericMethodTwoParameters<T1, T2>(T, T1, T2)");
        yield return Create(GetMethod(typeof(Nested<short>.Child), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<Int16>.Child.ChildGenericMethodTwoParameters<T1, T2>(Int16, T1, T2)");
        yield return Create(GetMethod(typeof(Nested<short>.Child), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float), typeof(double)), "Nested<Int16>.Child.ChildGenericMethodTwoParameters<Single, Double>(Int16, Single, Double)");
        yield return Create(GetMethod(typeof(Nested<>.Child<>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<T>.Child<TC1>.ChildGenericMethodOneParameter<T1>(T, TC1, T1)");
        yield return Create(GetMethod(typeof(Nested<short>.Child<int>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<Int16>.Child<Int32>.ChildGenericMethodOneParameter<T1>(Int16, Int32, T1)");
        yield return Create(GetMethod(typeof(Nested<short>.Child<int>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "Nested<Int16>.Child<Int32>.ChildGenericMethodOneParameter<Single>(Int16, Int32, Single)");
        yield return Create(GetMethod(typeof(Nested<>.Child<>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<T>.Child<TC1>.ChildGenericMethodTwoParameters<T1, T2>(T, TC1, T1, T2)");
        yield return Create(GetMethod(typeof(Nested<short>.Child<int>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<Int16>.Child<Int32>.ChildGenericMethodTwoParameters<T1, T2>(Int16, Int32, T1, T2)");
        yield return Create(GetMethod(typeof(Nested<short>.Child<int>), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float), typeof(double)), "Nested<Int16>.Child<Int32>.ChildGenericMethodTwoParameters<Single, Double>(Int16, Int32, Single, Double)");
        yield return Create(GetMethod(typeof(Nested<>.Child<,>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<T>.Child<TC1, TC2>.ChildGenericMethodOneParameter<T1>(T, TC1, TC2, T1)");
        yield return Create(GetMethod(typeof(Nested<short>.Child<int, long>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "Nested<Int16>.Child<Int32, Int64>.ChildGenericMethodOneParameter<T1>(Int16, Int32, Int64, T1)");
        yield return Create(GetMethod(typeof(Nested<short>.Child<int, long>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "Nested<Int16>.Child<Int32, Int64>.ChildGenericMethodOneParameter<Single>(Int16, Int32, Int64, Single)");
        yield return Create(GetMethod(typeof(Nested<>.Child<,>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<T>.Child<TC1, TC2>.ChildGenericMethodTwoParameters<T1, T2>(T, TC1, TC2, T1, T2)");
        yield return Create(GetMethod(typeof(Nested<short>.Child<int, long>), nameof(Nested.Child.ChildGenericMethodTwoParameters)), "Nested<Int16>.Child<Int32, Int64>.ChildGenericMethodTwoParameters<T1, T2>(Int16, Int32, Int64, T1, T2)");
        yield return Create(GetMethod(typeof(Nested<short>.Child<int, long>), nameof(Nested.Child.ChildGenericMethodTwoParameters)).MakeGenericMethod(typeof(float), typeof(double)), "Nested<Int16>.Child<Int32, Int64>.ChildGenericMethodTwoParameters<Single, Double>(Int16, Int32, Int64, Single, Double)");

        #endregion

        #region Operators

        yield return Create(GetOperator<CSharpOperators>(CSharpOperator.Decrement), "CSharpOperators.Decrement(CSharpOperators)");
        yield return Create(GetOperator<CSharpOperators>(CSharpOperator.Explicit), "CSharpOperators.Explicit(String)");

        #endregion

        #region Properties

        yield return Create(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), "PropertyModifiers.Normal");
        yield return Create(GetProperty<PropertyIndexerOneParameter>("Item"), "PropertyIndexerOneParameter.Item[Int32]");
        yield return Create(GetProperty<PropertyIndexerTwoParameters>("Item"), "PropertyIndexerTwoParameters.Item[Int32, String]");

        #endregion

        #region Types

        yield return Create(typeof(string), "String");
        yield return Create(typeof(string).MakePointerType(), "String*");
        yield return Create(typeof(string).MakeByRefType(), "String", "ref String");
        yield return Create(typeof(string[]), "String[]");
        yield return Create(typeof(string[,]), "String[,]");
        yield return Create(typeof(int?), "Nullable<Int32>");
        yield return Create(typeof(IEnumerable<>), "IEnumerable<T>");
        yield return Create(typeof(IEnumerable<string>), "IEnumerable<String>");
        yield return Create(typeof(Dictionary<,>), "Dictionary<TKey, TValue>");
        yield return Create(typeof(Dictionary<int, string>), "Dictionary<Int32, String>");
        yield return Create(typeof(Nested.Child), "Nested.Child");
        yield return Create(typeof(Nested.Child.GrandChild), "Nested.Child.GrandChild");
        yield return Create(typeof(Nested.Child.GrandChild<>), "Nested.Child.GrandChild<TG1>");
        yield return Create(typeof(Nested.Child.GrandChild<int>), "Nested.Child.GrandChild<Int32>");
        yield return Create(typeof(Nested.Child.GrandChild<,>), "Nested.Child.GrandChild<TG1, TG2>");
        yield return Create(typeof(Nested.Child.GrandChild<int, byte>), "Nested.Child.GrandChild<Int32, Byte>");
        yield return Create(typeof(Nested.Child<>), "Nested.Child<TC1>");
        yield return Create(typeof(Nested.Child<>.GrandChild), "Nested.Child<TC1>.GrandChild");
        yield return Create(typeof(Nested.Child<>.GrandChild<>), "Nested.Child<TC1>.GrandChild<TG1>");
        yield return Create(typeof(Nested.Child<>.GrandChild<,>), "Nested.Child<TC1>.GrandChild<TG1, TG2>");
        yield return Create(typeof(Nested.Child<int>), "Nested.Child<Int32>");
        yield return Create(typeof(Nested.Child<int>.GrandChild), "Nested.Child<Int32>.GrandChild");
        yield return Create(typeof(Nested.Child<int>.GrandChild<int>), "Nested.Child<Int32>.GrandChild<Int32>");
        yield return Create(typeof(Nested.Child<int>.GrandChild<int, byte>), "Nested.Child<Int32>.GrandChild<Int32, Byte>");
        yield return Create(typeof(Nested.Child<,>), "Nested.Child<TC1, TC2>");
        yield return Create(typeof(Nested.Child<,>.GrandChild), "Nested.Child<TC1, TC2>.GrandChild");
        yield return Create(typeof(Nested.Child<,>.GrandChild<>), "Nested.Child<TC1, TC2>.GrandChild<TG1>");
        yield return Create(typeof(Nested.Child<,>.GrandChild<,>), "Nested.Child<TC1, TC2>.GrandChild<TG1, TG2>");
        yield return Create(typeof(Nested.Child<int, byte>), "Nested.Child<Int32, Byte>");
        yield return Create(typeof(Nested.Child<int, byte>.GrandChild), "Nested.Child<Int32, Byte>.GrandChild");
        yield return Create(typeof(Nested.Child<int, byte>.GrandChild<int>), "Nested.Child<Int32, Byte>.GrandChild<Int32>");
        yield return Create(typeof(Nested.Child<int, byte>.GrandChild<int, byte>), "Nested.Child<Int32, Byte>.GrandChild<Int32, Byte>");
        yield return Create(typeof(Nested<>.Child), "Nested<T>.Child");
        yield return Create(typeof(Nested<>.Child.GrandChild), "Nested<T>.Child.GrandChild");
        yield return Create(typeof(Nested<>.Child.GrandChild<>), "Nested<T>.Child.GrandChild<TG1>");
        yield return Create(typeof(Nested<>.Child.GrandChild<,>), "Nested<T>.Child.GrandChild<TG1, TG2>");
        yield return Create(typeof(Nested<>.Child<>), "Nested<T>.Child<TC1>");
        yield return Create(typeof(Nested<>.Child<>.GrandChild), "Nested<T>.Child<TC1>.GrandChild");
        yield return Create(typeof(Nested<>.Child<>.GrandChild<>), "Nested<T>.Child<TC1>.GrandChild<TG1>");
        yield return Create(typeof(Nested<>.Child<>.GrandChild<,>), "Nested<T>.Child<TC1>.GrandChild<TG1, TG2>");
        yield return Create(typeof(Nested<>.Child<,>), "Nested<T>.Child<TC1, TC2>");
        yield return Create(typeof(Nested<>.Child<,>.GrandChild), "Nested<T>.Child<TC1, TC2>.GrandChild");
        yield return Create(typeof(Nested<>.Child<,>.GrandChild<>), "Nested<T>.Child<TC1, TC2>.GrandChild<TG1>");
        yield return Create(typeof(Nested<>.Child<,>.GrandChild<,>), "Nested<T>.Child<TC1, TC2>.GrandChild<TG1, TG2>");
        yield return Create(typeof(Nested<long>.Child), "Nested<Int64>.Child");
        yield return Create(typeof(Nested<long>.Child.GrandChild), "Nested<Int64>.Child.GrandChild");
        yield return Create(typeof(Nested<long>.Child.GrandChild<int>), "Nested<Int64>.Child.GrandChild<Int32>");
        yield return Create(typeof(Nested<long>.Child.GrandChild<int, byte>), "Nested<Int64>.Child.GrandChild<Int32, Byte>");
        yield return Create(typeof(Nested<long>.Child<int>), "Nested<Int64>.Child<Int32>");
        yield return Create(typeof(Nested<long>.Child<int>.GrandChild), "Nested<Int64>.Child<Int32>.GrandChild");
        yield return Create(typeof(Nested<long>.Child<int>.GrandChild<int>), "Nested<Int64>.Child<Int32>.GrandChild<Int32>");
        yield return Create(typeof(Nested<long>.Child<int>.GrandChild<int, byte>), "Nested<Int64>.Child<Int32>.GrandChild<Int32, Byte>");
        yield return Create(typeof(Nested<long>.Child<int, byte>), "Nested<Int64>.Child<Int32, Byte>");
        yield return Create(typeof(Nested<long>.Child<int, byte>.GrandChild), "Nested<Int64>.Child<Int32, Byte>.GrandChild");
        yield return Create(typeof(Nested<long>.Child<int, byte>.GrandChild<int>), "Nested<Int64>.Child<Int32, Byte>.GrandChild<Int32>");
        yield return Create(typeof(Nested<long>.Child<int, byte>.GrandChild<int, byte>), "Nested<Int64>.Child<Int32, Byte>.GrandChild<Int32, Byte>");

        #endregion
    }

    [TestCaseSource(nameof(Format_QualifiedTestCases))]
    public void Format_Qualified(MemberInfo member, string expected) =>
        new DisplayNameFormatter(DisplayNameFormatterOptions.UseQualifiedNames).Format(member).Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> Format_QualifiedTestCases()
    {
        static TestCaseData Create(MemberInfo member, string expected, string? name = null) => new TestCaseData(member, expected).SetArgDisplayNames(name ?? expected);

        yield return Create(typeof(string), "System.String");
        yield return Create(typeof(string).MakePointerType(), "System.String*");
        yield return Create(typeof(string).MakeByRefType(), "System.String", "ref String");
        yield return Create(typeof(string[]), "System.String[]");
        yield return Create(typeof(string[,]), "System.String[,]");
        yield return Create(typeof(int?), "System.Nullable<System.Int32>");
        yield return Create(typeof(IEnumerable<>), "System.Collections.Generic.IEnumerable<T>");
        yield return Create(typeof(IEnumerable<string>), "System.Collections.Generic.IEnumerable<System.String>");
        yield return Create(typeof(Dictionary<,>), "System.Collections.Generic.Dictionary<TKey, TValue>");
        yield return Create(typeof(Dictionary<int, string>), "System.Collections.Generic.Dictionary<System.Int32, System.String>");
        yield return Create(typeof(Nested<>.Child<,>.GrandChild<,>), "MrKWatkins.Reflection.Tests.TestTypes.Types.Nested<T>.Child<TC1, TC2>.GrandChild<TG1, TG2>");
        yield return Create(typeof(Nested<long>.Child<int>.GrandChild), "MrKWatkins.Reflection.Tests.TestTypes.Types.Nested<System.Int64>.Child<System.Int32>.GrandChild");
        yield return Create(GetConstructor(typeof(Dictionary<,>), [typeof(int)]), "System.Collections.Generic.Dictionary<TKey, TValue>.Dictionary(System.Int32)");
        yield return Create(GetConstructor(typeof(Dictionary<byte, long>), [typeof(int)]), "System.Collections.Generic.Dictionary<System.Byte, System.Int64>.Dictionary(System.Int32)");
        yield return Create(GetField<FieldModifiers>(nameof(FieldModifiers.Const)), "MrKWatkins.Reflection.Tests.TestTypes.Fields.FieldModifiers.Const");
        yield return Create(GetEvent<EventAccessibility>(nameof(EventAccessibility.Public)), "MrKWatkins.Reflection.Tests.TestTypes.Events.EventAccessibility.Public");
        yield return Create(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), "MrKWatkins.Reflection.Tests.TestTypes.Properties.PropertyModifiers.Normal");
        yield return Create(GetProperty<PropertyIndexerOneParameter>("Item"), "MrKWatkins.Reflection.Tests.TestTypes.Properties.PropertyIndexerOneParameter.Item[System.Int32]");
        yield return Create(GetProperty<PropertyIndexerTwoParameters>("Item"), "MrKWatkins.Reflection.Tests.TestTypes.Properties.PropertyIndexerTwoParameters.Item[System.Int32, System.String]");
        yield return Create(GetOperator<CSharpOperators>(CSharpOperator.Decrement), "MrKWatkins.Reflection.Tests.TestTypes.Operators.CSharpOperators.Decrement(MrKWatkins.Reflection.Tests.TestTypes.Operators.CSharpOperators)");
        yield return Create(GetMethod(typeof(Nested.Child<,>), nameof(Nested.Child.ChildGenericMethodOneParameter)), "MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child<TC1, TC2>.ChildGenericMethodOneParameter<T1>(TC1, TC2, T1)");
        yield return Create(GetMethod(typeof(Nested.Child<int, long>), nameof(Nested.Child.ChildGenericMethodOneParameter)).MakeGenericMethod(typeof(float)), "MrKWatkins.Reflection.Tests.TestTypes.Types.Nested.Child<System.Int32, System.Int64>.ChildGenericMethodOneParameter<System.Single>(System.Int32, System.Int64, System.Single)");
    }
}