using System.Reflection;

namespace MrKWatkins.Reflection.Formatting;

public sealed class DisplayNameFormatter : ReflectionFormatter
{
    private readonly DisplayNameFormatterOptions options;

    public DisplayNameFormatter(DisplayNameFormatterOptions options)
    {
        this.options = options;
    }

    protected override void Format(TextWriter output, EventInfo @event)
    {
        Format(output, @event.DeclaringType!);
        output.Write('.');
        output.Write(@event.Name);
    }

    protected override void Format(TextWriter output, FieldInfo field)
    {
        Format(output, field.DeclaringType!);
        output.Write('.');
        output.Write(field.Name);
    }

    protected override void Format(TextWriter output, PropertyInfo property)
    {
        Format(output, property.DeclaringType!);
        output.Write('.');
        output.Write(property.Name);
        if (property.IsIndexer())
        {
            output.Write('[');
            WriteTypeList(output, property.GetIndexParameters().Select(p => p.ParameterType));
            output.Write(']');
        }
    }

    protected override void Format(TextWriter output, MethodInfo method)
    {
        Format(output, method.DeclaringType!);
        output.Write('.');
        output.Write(method.Name);

        var genericArguments = method.GetGenericArguments();
        if (genericArguments.Length > 0)
        {
            output.Write('<');
            WriteTypeList(output, genericArguments);
            output.Write('>');
        }

        output.Write('(');
        WriteTypeList(output, method.GetParameters().Select(p => p.ParameterType));
        output.Write(')');
    }

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

        var isFirst = true;
        foreach (var (nestedType, genericArguments) in EnumerateNestedTypes(type))
        {
            if (!isFirst)
            {
                output.Write('.');
            }

            WriteName(output, nestedType, isFirst && options.HasFlag(DisplayNameFormatterOptions.UseQualifiedNames));

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

    private static void WriteName(TextWriter output, Type type, bool qualifiedName)
    {
        if (qualifiedName && type.Namespace != null)
        {
            output.Write(type.Namespace);
            output.Write('.');
        }

        var name = type.Name;
        output.Write(name.Length > 2 && name[^2] == '`' ? name.AsSpan()[..^2] : name);
    }
}