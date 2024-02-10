using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Reflection;

namespace MrKWatkins.Reflection;

public static class DisplayNameExtensions
{
    private static readonly ConcurrentDictionary<MemberInfo, string> Cache = new();
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
    public static string DisplayNameOrKeyword(this Type type) => TypeKeywords.GetValueOrDefault(type) ?? DisplayName(type);

    [Pure]
    public static string DisplayName(this MemberInfo member) => Cache.GetOrAdd(member, m =>
        m switch
        {
            ConstructorInfo constructor => BuildDisplayName(constructor),
            EventInfo @event => BuildDisplayName(@event),
            FieldInfo field => BuildDisplayName(field),
            MethodInfo method => BuildDisplayName(method),
            PropertyInfo property => BuildDisplayName(property),
            Type type => BuildDisplayName(type),
            _ => throw new NotSupportedException($"Members of type {member.GetType().DisplayName()} are not supported.")
        });

    [Pure]
    private static string BuildDisplayName(ConstructorInfo constructor)
    {
        var type = constructor.DeclaringType!;
        if (type.IsGenericType)
        {
            return $"{constructor.DeclaringType!.Name[..^2]}{BuildParameterList(constructor)}";
        }
        return $"{constructor.DeclaringType!.Name}{BuildParameterList(constructor)}";
    }

    [Pure]
    private static string BuildDisplayName(MemberInfo member) => member.Name;

    [Pure]
    private static string BuildDisplayName(MethodInfo method) => $"{method.Name}{BuildParameterList(method)}";

    [Pure]
    private static string BuildDisplayName(Type type)
    {
        if (type.IsArray)
        {
            var elementType = BuildDisplayName(type.GetElementType()!);
            var rank = new string(',', type.GetArrayRank() - 1);
            return $"{elementType}[{rank}]";
        }

        if (type.IsByRef)
        {
            return BuildDisplayName(type.GetElementType()!);
        }

        var underlyingType = Nullable.GetUnderlyingType(type);
        if (underlyingType != null)
        {
            return $"{BuildDisplayName(underlyingType)}?";
        }

        if (type.IsGenericParameter)
        {
            if (type.GenericParameterAttributes.HasFlag(GenericParameterAttributes.Contravariant))
            {
                return $"in {type.Name}";
            }

            if (type.GenericParameterAttributes.HasFlag(GenericParameterAttributes.Covariant))
            {
                return $"out {type.Name}";
            }

            return type.Name;
        }

        return type.IsGenericType
            ? $"{type.Name[..^2]}<{string.Join(", ", type.GetGenericArguments().Select(DisplayName))}>"
            : type.Name;
    }

    [Pure]
    private static string BuildParameterList(MethodBase method) =>
        $"({string.Join(", ", method.GetParameters().Select(p => p.ParameterType.DisplayName()))})";
}