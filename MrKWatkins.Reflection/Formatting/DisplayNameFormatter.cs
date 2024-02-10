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
            Format(output, property.GetIndexParameters().Select(p => p.ParameterType));
            output.Write(']');
        }
    }

    private void Format(TextWriter output, [InstantHandle] IEnumerable<Type> types)
    {
        var needsSeparator = false;
        foreach (var type in types)
        {
            if (needsSeparator)
            {
                output.Write(", ");
            }

            Format(output, type);

            needsSeparator = true;
        }
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
                for (var f = 0; f < genericArguments.Count; f++)
                {
                    if (f != 0)
                    {
                        output.Write(", ");
                    }

                    Format(output, genericArguments[f]);
                }
                output.Write('>');
            }

            isFirst = false;
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

    [Pure]
    private static IEnumerable<(Type Type, ArraySegment<Type> GenericArguments)> EnumerateNestedTypes(Type type)
    {
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
}