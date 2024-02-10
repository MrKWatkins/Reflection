using System.Reflection;
using System.Text;

namespace MrKWatkins.Reflection.Formatting;

public abstract class ReflectionFormatter : IReflectionFormatter
{
    public string Format(MemberInfo member)
    {
        var output = new StringBuilder();
        Format(output, member);
        return output.ToString();
    }

    public void Format(StringBuilder output, MemberInfo member)
    {
        using var writer = new StringWriter(output);
        Format(writer, member);
    }

    public void Format(TextWriter output, MemberInfo member)
    {
        switch (member)
        {
            case ConstructorInfo constructor:
                Format(output, constructor);
                return;
            case EventInfo @event:
                Format(output, @event);
                return;
            case FieldInfo field:
                Format(output, field);
                return;
            case MethodInfo method:
                Format(output, method);
                return;
            case PropertyInfo property:
                Format(output, property);
                return;
            case Type type:
                Format(output, type);
                return;
        }
        throw new NotSupportedException($"Members of type {member.GetType().Name} are not supported.");
    }

    protected virtual void Format(TextWriter output, ConstructorInfo constructor) => Format(output, (MethodBase)constructor);

    protected abstract void Format(TextWriter output, EventInfo @event);

    protected abstract void Format(TextWriter output, FieldInfo field);

    protected virtual void Format(TextWriter output, MethodBase method) => throw new NotImplementedException($"{nameof(Format)}({nameof(TextWriter)}, {nameof(MethodBase)}) is not implemented.");

    protected virtual void Format(TextWriter output, MethodInfo method) => Format(output, (MethodBase)method);

    protected abstract void Format(TextWriter output, PropertyInfo property);

    protected abstract void Format(TextWriter output, Type type);
}