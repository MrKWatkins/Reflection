using System.Collections.Frozen;
namespace MrKWatkins.Reflection;

public static class DisplayNameExtensions
{
    private static readonly FrozenDictionary<Type, string> TypeKeywords = new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(bool?), "bool?" },
            { typeof(char), "char" },
            { typeof(char?), "char?" },
            { typeof(decimal), "decimal" },
            { typeof(decimal?), "decimal?" },
            { typeof(double), "double" },
            { typeof(double?), "double?" },
            { typeof(float), "float" },
            { typeof(float?), "float?" },
            { typeof(int), "int" },
            { typeof(int?), "int?" },
            { typeof(long), "long" },
            { typeof(long?), "long?" },
            { typeof(nint), "nint" },
            { typeof(nint?), "nint?" },
            { typeof(nuint), "nuint" },
            { typeof(nuint?), "nuint?" },
            { typeof(object), "object" },
            { typeof(sbyte), "sbyte" },
            { typeof(sbyte?), "sbyte?" },
            { typeof(string), "string" },
            { typeof(uint), "uint" },
            { typeof(uint?), "uint?" },
            { typeof(ulong), "ulong" },
            { typeof(ulong?), "ulong?" },
            { typeof(ushort), "ushort" },
            { typeof(ushort?), "ushort?" },
            { typeof(void), "void" }
        }
        .ToFrozenDictionary();

    [Pure]
    public static string DisplayNameOrKeyword(this Type type) => TypeKeywords.GetValueOrDefault(type) ?? type.ToDisplayName();
}