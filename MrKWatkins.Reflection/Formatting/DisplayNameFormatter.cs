using System.Reflection;

namespace MrKWatkins.Reflection.Formatting;

/// <summary>
/// Formats reflection types into a human readable display name.
/// </summary>
public sealed class DisplayNameFormatter : ReflectionFormatter
{
    private readonly DisplayNameFormatterOptions options;

    /// <summary>
    /// Initialises a new instance of the <see cref="DisplayNameFormatter"/> class using default options.
    /// </summary>
    public DisplayNameFormatter()
        : this(new DisplayNameFormatterOptions())
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="DisplayNameFormatter"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to use.</param>
    public DisplayNameFormatter(DisplayNameFormatterOptions options)
    {
        this.options = options;
    }

    /// <summary>
    /// Formats the specified namespace.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="namespace"/> to.</param>
    /// <param name="namespace">The namespace.</param>
    public override void FormatNamespace(TextWriter output, string @namespace) => output.Write(@namespace);

    /// <summary>
    /// Formats the specified <see cref="ConstructorInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="constructor"/> to.</param>
    /// <param name="constructor">The constructor.</param>
    protected override void Format(TextWriter output, ConstructorInfo constructor) => WriteMethod(output, constructor);

    /// <summary>
    /// Formats the specified <see cref="EventInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="event"/> to.</param>
    /// <param name="event">The event.</param>
    protected override void Format(TextWriter output, EventInfo @event)
    {
        if (options.PrefixMembersWithType)
        {
            Format(output, @event.DeclaringType!);
            output.Write('.');
        }
        output.Write(@event.Name);
    }

    /// <summary>
    /// Formats the specified <see cref="FieldInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="field"/> to.</param>
    /// <param name="field">The field.</param>
    protected override void Format(TextWriter output, FieldInfo field)
    {
        if (options.PrefixMembersWithType)
        {
            Format(output, field.DeclaringType!);
            output.Write('.');
        }

        output.Write(field.Name);
    }

    /// <summary>
    /// Formats the specified <see cref="MethodInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="method"/> to.</param>
    /// <param name="method">The method.</param>
    protected override void Format(TextWriter output, MethodInfo method) => WriteMethod(output, method);

    /// <summary>
    /// Formats the specified <see cref="PropertyInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="property"/> to.</param>
    /// <param name="property">The property.</param>
    protected override void Format(TextWriter output, PropertyInfo property)
    {
        if (options.PrefixMembersWithType)
        {
            Format(output, property.DeclaringType!);
            output.Write('.');
        }

        output.Write(property.Name);
        if (property.IsIndexer())
        {
            output.Write('[');
            WriteTypeList(output, property.GetIndexParameters().Select(p => p.ParameterType));
            output.Write(']');
        }
    }

    /// <summary>
    /// Formats the specified <see cref="Type" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="type"/> to.</param>
    /// <param name="type">The type.</param>
    protected override void Format(TextWriter output, Type type)
    {
        if (type.IsArray)
        {
            Format(output, type.GetElementType()!);
            output.Write('[');
            for (var f = 0; f < type.GetArrayRank() - 1; f++)
            {
                output.Write(',');
            }
            output.Write(']');
            return;
        }

        if (type.IsPointer)
        {
            Format(output, type.GetElementType()!);
            output.Write('*');
            return;
        }

        if (type.IsByRef)
        {
            Format(output, type.GetElementType()!);
            return;
        }

        if (type.IsGenericParameter)
        {
            output.Write(type.Name);
            return;
        }

        if (options.UseQuestionMarksForNullableTypes)
        {
            var underlying = Nullable.GetUnderlyingType(type);
            if (underlying != null)
            {
                WriteName(output, underlying, options.UseFullyQualifiedTypes);
                output.Write('?');
                return;
            }
        }

        var isFirst = true;
        foreach (var (nestedType, genericArguments) in EnumerateNestedTypes(type))
        {
            if (!isFirst)
            {
                output.Write('.');
            }

            WriteName(output, nestedType, isFirst && options.UseFullyQualifiedTypes);

            if (genericArguments.Count > 0)
            {
                output.Write('<');
                WriteTypeList(output, genericArguments);
                output.Write('>');
            }

            isFirst = false;
        }
    }

    [Pure]
    private static IEnumerable<(Type Type, ArraySegment<Type> GenericArguments)> EnumerateNestedTypes(Type type)
    {
        // Nested generic types have are defined with generic type parameters for all their parent types. For example, given
        // Root<T>.Parent<U>.Child<V> then Root has one type T, Parent has T, U and Child has T, U, V. When formatting we want
        // to show only the generic types parameters explicitly specified for that type. Using the type parameters for the type
        // being formatted we can go through all the types in the nested hierarchy, keep track of how many have been used at
        // each level and for a given level only use the ones found at that level excluding any used higher up.
        var genericArguments = type.GetGenericArguments();
        var hierarchy = new List<Type> { type };
        while (true)
        {
            var parent = type.DeclaringType;
            if (parent is null)
            {
                break;
            }
            hierarchy.Add(parent);
            type = parent;
        }

        var parametersUsed = 0;
        for (var f = 0; f < hierarchy.Count; f++)
        {
            var nestedType = hierarchy[hierarchy.Count - f - 1];
            var totalParameters = nestedType.GetGenericArguments().Length;
            var parametersDefinedByItem = totalParameters - parametersUsed;
            yield return (nestedType, new ArraySegment<Type>(genericArguments, parametersUsed, parametersDefinedByItem));
            parametersUsed = totalParameters;
        }
    }

    private void WriteMethod(TextWriter output, MethodBase method)
    {
        var type = method.DeclaringType!;
        if (options.PrefixMembersWithType)
        {
            Format(output, method.DeclaringType!);
            output.Write('.');
        }

        if (method.IsConstructor)
        {
            WriteName(output, type, false);
        }
        else if (method.Name.StartsWith("op_", StringComparison.Ordinal))
        {
            output.Write(method.Name.AsSpan(3));
        }
        else
        {
            output.Write(method.Name);
        }

        if (method.IsGenericMethod)
        {
            output.Write('<');
            WriteTypeList(output, method.GetGenericArguments());
            output.Write('>');
        }

        output.Write('(');
        WriteTypeList(output, method.GetParameters().Select(p => p.ParameterType));
        output.Write(')');
    }

    private void WriteTypeList(TextWriter output, [InstantHandle] IEnumerable<Type> types)
    {
        using var enumerator = types.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            return;
        }

        Format(output, enumerator.Current);

        while (enumerator.MoveNext())
        {
            output.Write(", ");
            Format(output, enumerator.Current);
        }
    }

    private void WriteName(TextWriter output, Type type, bool qualifiedName)
    {
        if (options.UseCSharpKeywordsForPrimitiveTypes && CSharp.PrimitiveTypeKeywords.TryGetValue(type, out var keyword))
        {
            output.Write(keyword);
            return;
        }

        if (qualifiedName && type.Namespace != null)
        {
            output.Write(type.Namespace);
            output.Write('.');
        }

        var name = type.Name;
        output.Write(name.Length > 2 && name[^2] == '`' ? name.AsSpan()[..^2] : name);
    }
}