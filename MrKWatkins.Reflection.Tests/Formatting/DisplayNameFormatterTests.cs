using System.Reflection;
using MrKWatkins.Reflection.Formatting;
using MrKWatkins.Reflection.Tests.TestTypes.Events;
using MrKWatkins.Reflection.Tests.TestTypes.Fields;
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
        static TestCaseData Create(MemberInfo member, string expected) => new TestCaseData(member, expected).SetArgDisplayNames(member.ToString()!);

        #region Types

        yield return Create(typeof(string), "String");
        yield return Create(typeof(string).MakePointerType(), "String*");
        yield return Create(typeof(string).MakeByRefType(), "String");
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

        #region Fields

        yield return Create(GetField<FieldModifiers>(nameof(FieldModifiers.Const)), "FieldModifiers.Const");
        yield return Create(GetField<FieldModifiers>(nameof(FieldModifiers.Instance)), "FieldModifiers.Instance");
        yield return Create(GetField<FieldModifiers>(nameof(FieldModifiers.Static)), "FieldModifiers.Static");

        #endregion

        #region Events

        yield return Create(GetEvent<EventAccessibility>(nameof(EventAccessibility.Public)), "EventAccessibility.Public");

        #endregion

        #region Properties

        yield return Create(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), "PropertyModifiers.Normal");
        yield return Create(GetProperty<PropertyIndexerOneParameter>("Item"), "PropertyIndexerOneParameter.Item[Int32]");
        yield return Create(GetProperty<PropertyIndexerTwoParameters>("Item"), "PropertyIndexerTwoParameters.Item[Int32, String]");

        #endregion
/*
        // Methods.
        yield return Create(
            typeof(object).GetMethod(nameof(ToString))!,
            "M:System.Object.ToString");

        yield return Create(
            typeof(HashSet<>).GetMethod(nameof(HashSet<int>.CopyTo),
            [
                typeof(HashSet<>).GetGenericArguments()[0].MakeArrayType()
            ])!,
            "M:System.Collections.Generic.HashSet`1.CopyTo(`0[])");

        yield return Create(
            typeof(Dictionary<,>).GetMethod(nameof(Dictionary<int, int>.TryGetValue))!,
            "M:System.Collections.Generic.Dictionary`2.TryGetValue(`0,`1@)");

        yield return Create(
            typeof(Dictionary<,>).GetMethod("System.Collections.IDictionary.GetEnumerator", BindingFlags.Instance | BindingFlags.NonPublic)!,
            "M:System.Collections.Generic.Dictionary`2.System#Collections#IDictionary#GetEnumerator");

        yield return Create(
            typeof(CollectionExtensions).GetMethods().Single(m => m.Name == nameof(CollectionExtensions.GetValueOrDefault) && m.GetParameters().Length == 2),
            "M:System.Collections.Generic.CollectionExtensions.GetValueOrDefault``2(System.Collections.Generic.IReadOnlyDictionary{``0,``1},``0)");

        yield return Create(
            typeof(ParameterTypes).GetMethod(nameof(ParameterTypes.MultidimensionalArrayParameter))!,
            "M:MrKWatkins.DocGen.Tests.XmlDocIdTests.TestCases.MultidimensionalArrayParameter(System.Int32[,])");

        // Constructors.
        yield return Create(
            typeof(Dictionary<int, string>).GetConstructor(Type.EmptyTypes)!,
            "M:System.Collections.Generic.Dictionary`2.#ctor");

        yield return Create(
            typeof(Dictionary<int, string>).GetConstructor([typeof(int)])!,
            "M:System.Collections.Generic.Dictionary`2.#ctor(System.Int32)");

        yield return Create(
            typeof(Dictionary<int, string>).GetConstructor([typeof(IEqualityComparer<int>)])!,
            "M:System.Collections.Generic.Dictionary`2.#ctor(System.Collections.Generic.IEqualityComparer{System.Int32})");

        yield return Create(
            typeof(Dictionary<,>).GetConstructor(
            [
                typeof(IEqualityComparer<>).MakeGenericType(typeof(Dictionary<,>).GetGenericArguments()[0])
            ])!,
            "M:System.Collections.Generic.Dictionary`2.#ctor(System.Collections.Generic.IEqualityComparer{`0})");

        yield return Create(
            typeof(Dictionary<,>).GetConstructor(
            [
                typeof(IEnumerable<>).MakeGenericType(typeof(KeyValuePair<,>).MakeGenericType(typeof(Dictionary<,>).GetGenericArguments()))
            ])!,
            "M:System.Collections.Generic.Dictionary`2.#ctor(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{`0,`1}})");

        yield return Create(
            typeof(string).GetConstructor([typeof(char).MakePointerType()])!,
            "M:System.String.#ctor(System.Char*)");

        // Operators.
        yield return Create(
            typeof(decimal).GetMethod("op_Implicit", [typeof(byte)])!,
            "M:System.Decimal.op_Implicit(System.Byte)~System.Decimal");

        yield return Create(
            typeof(decimal).GetMethod("op_Explicit", [typeof(double)])!,
            "M:System.Decimal.op_Explicit(System.Double)~System.Decimal");

        yield return Create(
            typeof(Half).GetMethods().Single(m => m.Name == "op_CheckedExplicit" && m.ReturnType == typeof(byte)),
            "M:System.Half.op_CheckedExplicit(System.Half)~System.Byte");
            */
    }

    [TestCaseSource(nameof(Format_QualifiedTestCases))]
    public void Format_Qualified(MemberInfo member, string expected) =>
        new DisplayNameFormatter(DisplayNameFormatterOptions.UseQualifiedNames).Format(member).Should().Be(expected);

    [Pure]
    public static IEnumerable<TestCaseData> Format_QualifiedTestCases()
    {
        static TestCaseData Create(MemberInfo member, string expected) => new TestCaseData(member, expected).SetArgDisplayNames(member.ToString()!);

        yield return Create(typeof(string), "System.String");
        yield return Create(typeof(string).MakePointerType(), "System.String*");
        yield return Create(typeof(string).MakeByRefType(), "System.String");
        yield return Create(typeof(string[]), "System.String[]");
        yield return Create(typeof(string[,]), "System.String[,]");
        yield return Create(typeof(int?), "System.Nullable<System.Int32>");
        yield return Create(typeof(IEnumerable<>), "System.Collections.Generic.IEnumerable<T>");
        yield return Create(typeof(IEnumerable<string>), "System.Collections.Generic.IEnumerable<System.String>");
        yield return Create(typeof(Dictionary<,>), "System.Collections.Generic.Dictionary<TKey, TValue>");
        yield return Create(typeof(Dictionary<int, string>), "System.Collections.Generic.Dictionary<System.Int32, System.String>");
        yield return Create(typeof(Nested<>.Child<,>.GrandChild<,>), "MrKWatkins.Reflection.Tests.TestTypes.Types.Nested<T>.Child<TC1, TC2>.GrandChild<TG1, TG2>");
        yield return Create(typeof(Nested<long>.Child<int>.GrandChild), "MrKWatkins.Reflection.Tests.TestTypes.Types.Nested<System.Int64>.Child<System.Int32>.GrandChild");

        yield return Create(GetField<FieldModifiers>(nameof(FieldModifiers.Const)), "MrKWatkins.Reflection.Tests.TestTypes.Fields.FieldModifiers.Const");

        yield return Create(GetEvent<EventAccessibility>(nameof(EventAccessibility.Public)), "MrKWatkins.Reflection.Tests.TestTypes.Events.EventAccessibility.Public");

        yield return Create(GetProperty<PropertyModifiers>(nameof(PropertyModifiers.Normal)), "MrKWatkins.Reflection.Tests.TestTypes.Properties.PropertyModifiers.Normal");
        yield return Create(GetProperty<PropertyIndexerOneParameter>("Item"), "MrKWatkins.Reflection.Tests.TestTypes.Properties.PropertyIndexerOneParameter.Item[System.Int32]");
        yield return Create(GetProperty<PropertyIndexerTwoParameters>("Item"), "MrKWatkins.Reflection.Tests.TestTypes.Properties.PropertyIndexerTwoParameters.Item[System.Int32, System.String]");
    }
}