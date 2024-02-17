using System.Collections.Frozen;

namespace MrKWatkins.Reflection;

/// <summary>
/// Members related specifically to C# and not .NET in general.
/// </summary>
public static class CSharp
{
    /// <summary>
    /// All the keywords in CSharp.
    /// </summary>
    public static readonly IReadOnlySet<string> Keywords = new[]
    {
        "abstract", "as",
        "base", "bool", "break", "byte",
        "case", "catch", "char", "checked", "class", "const", "continue",
        "decimal", "default", "delegate", "do", "double",
        "else", "enum", "event", "explicit", "extern",
        "false", "finally", "fixed", "float", "for", "foreach",
        "goto",
        "if", "implicit", "in", "int", "interface", "internal", "is",
        "lock", "long",
        "namespace", "new", "null",
        "object", "operator", "out", "override",
        "params", "private", "protected", "public",
        "readonly", "ref", "return",
        "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch",
        "this", "throw", "true", "try", "typeof",
        "uint", "ulong", "unchecked", "unsafe", "ushort", "using",
        "virtual", "void", "volatile",
        "while"
    }.ToFrozenSet();

    /// <summary>
    /// Keywords for the primitive types in C#.
    /// </summary>
    public static readonly IReadOnlyDictionary<Type, string> PrimitiveTypeKeywords = new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(long), "long" },
            { typeof(nint), "nint" },
            { typeof(nuint), "nuint" },
            { typeof(object), "object" },
            { typeof(sbyte), "sbyte" },
            { typeof(string), "string" },
            { typeof(uint), "uint" },
            { typeof(ulong), "ulong" },
            { typeof(ushort), "ushort" },
            { typeof(void), "void" }
        }
        .ToFrozenDictionary();
}