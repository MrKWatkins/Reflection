using System.Reflection;

namespace MrKWatkins.Reflection.Formatting;

public sealed class XmlDocIdFormatter : ReflectionFormatter
{
    protected override void Format(TextWriter output, EventInfo @event)
    {
        output.Write("E:");
        Write(output, @event.DeclaringType!);
        output.Write('.');
        WriteName(output, @event);
    }

    protected override void Format(TextWriter output, FieldInfo field)
    {
        output.Write("F:");
        Write(output, field.DeclaringType!);
        output.Write('.');
        WriteName(output, field);
    }

    protected override void Format(TextWriter output, PropertyInfo property)
    {
        output.Write("P:");
        Write(output, property.DeclaringType!);
        output.Write('.');
        WriteName(output, property);
        WriteParameters(output, property.GetIndexParameters());
    }

    protected override void Format(TextWriter output, MethodInfo method)
    {
        Format(output, (MethodBase)method);

        if (method.Name is "op_Implicit" or "op_Explicit" or "op_CheckedExplicit")
        {
            output.Write('~');
            WriteParameterType(output, method, method.ReturnType);
        }
    }

    protected override void Format(TextWriter output, Type type)
    {
        output.Write("T:");
        Write(output, type);
    }

    private static void Write(TextWriter output, Type type)
    {
        VerifyType(type);

        WriteNamespace(output, type);
        foreach (var nested in type.EnumerateNestedTypes())
        {
            WriteName(output, nested);
            if (nested != type)
            {
                output.Write('.');
            }
        }
    }

    protected override void Format(TextWriter output, MethodBase method)
    {
        output.Write("M:");
        Write(output, method.DeclaringType!);  // Ignoring global methods on a module; we don't generate documentation for them.
        output.Write('.');

        WriteName(output, method);

        if (method.IsGenericMethod)
        {
            output.Write("``");
            output.Write(method.GetGenericArguments().Length);
        }

        WriteParameters(output, method.GetParameters());
    }

    private static void WriteParameters(TextWriter output, IReadOnlyList<ParameterInfo> parameters)
    {
        if (parameters.Count == 0)
        {
            return;
        }

        output.Write('(');
        foreach (var parameter in parameters)
        {
            if (parameter != parameters[0])
            {
                output.Write(',');
            }

            WriteParameterType(output, parameter.Member, parameter.ParameterType);
        }
        output.Write(')');
    }

    private static void WriteParameterType(TextWriter output, MemberInfo member, Type type)
    {
        if (type.IsPointer)
        {
            WriteParameterTypePointer(output, member, type);
            return;
        }

        if (type.IsArray)
        {
            WriteParameterTypeArray(output, member, type);
            return;
        }

        if (type.IsByRef)
        {
            WriteParameterTypeByRef(output, member, type);
            return;
        }

        if (type.IsGenericMethodParameter)
        {
            WriteParameterTypeGenericMethodParameter(output, member, type);
            return;
        }

        if (type.IsGenericTypeParameter)
        {
            WriteParameterTypeGenericTypeParameter(output, member, type);
            return;
        }

        if (type.IsGenericType)
        {
            WriteParameterTypeGenericType(output, member, type);
            return;
        }

        WriteNamespace(output, type);
        WriteName(output, type);
    }

    private static void WriteParameterTypeGenericType(TextWriter output, MemberInfo member, Type type)
    {
        WriteNamespace(output, type);
        WriteName(output, type.GetGenericTypeDefinition(), false);

        output.Write('{');
        var typeArguments = type.GetGenericArguments();
        foreach (var typeArgument in typeArguments)
        {
            if (typeArgument != typeArguments[0])
            {
                output.Write(',');
            }

            WriteParameterType(output, member, typeArgument);
        }

        output.Write('}');
    }

    private static void WriteParameterTypeGenericTypeParameter(TextWriter output, MemberInfo member, Type type)
    {
        var typeArguments = member
            .DeclaringType!
            .GetGenericTypeDefinition()
            .GetGenericArguments();

        var index = Array.IndexOf(typeArguments, type);

        output.Write('`');
        output.Write(index);
    }

    private static void WriteParameterTypeGenericMethodParameter(TextWriter output, MemberInfo member, Type type)
    {
        var method = (MethodBase)member;
        var methodArguments = method.GetGenericArguments();

        var index = Array.IndexOf(methodArguments, type);

        output.Write("``");
        output.Write(index);
    }

    private static void WriteParameterTypePointer(TextWriter output, MemberInfo member, Type type)
    {
        WriteParameterType(output, member, type.GetElementType()!);
        output.Write('*');
    }

    private static void WriteParameterTypeArray(TextWriter output, MemberInfo member, Type type)
    {
        WriteParameterType(output, member, type.GetElementType()!);
        output.Write('[');
        for (var f = 0; f < type.GetArrayRank() - 1; f++)
        {
            output.Write(',');
        }
        output.Write(']');
    }

    private static void WriteParameterTypeByRef(TextWriter output, MemberInfo member, Type type)
    {
        WriteParameterType(output, member, type.GetElementType()!);
        output.Write('@');
    }

    private static void WriteNamespace(TextWriter output, Type type)
    {
        if (type.Namespace != null)
        {
            output.Write(type.Namespace);
            output.Write('.');
        }
    }

    private static void WriteName(TextWriter output, MemberInfo member, bool includeTypeParameterNumber = true)
    {
        var name = includeTypeParameterNumber ? member.Name : member.Name[..^2];
        output.Write(name.Replace('.', '#').Replace('<', '{').Replace('>', '}').Replace(',', '@'));
    }

    private static void VerifyType(Type type)
    {
        if (type.IsPointer)
        {
            throw new ArgumentException($"Type {type.ToDisplayName()} is a pointer type; pointer types do not have XmlDocIds.", nameof(type));
        }

        if (type.IsArray)
        {
            throw new ArgumentException($"Type {type.ToDisplayName()} is an array type; array types do not have XmlDocIds.", nameof(type));
        }

        if (type.IsByRef)
        {
            throw new ArgumentException($"Type {type.ToDisplayName()}& is a by ref type; by ref types do not have XmlDocIds.", nameof(type));
        }

        if (type.IsConstructedGenericType)
        {
            throw new ArgumentException($"Type {type.ToDisplayName()} is a constructed generic type; constructed generic types do not have XmlDocIds.", nameof(type));
        }
    }
}