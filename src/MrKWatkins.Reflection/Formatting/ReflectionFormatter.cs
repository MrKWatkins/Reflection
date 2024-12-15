using System.Reflection;
using System.Text;

namespace MrKWatkins.Reflection.Formatting;

/// <summary>
/// Base implementation of <see cref="IReflectionFormatter" />.
/// </summary>
public abstract class ReflectionFormatter : IReflectionFormatter
{
    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>A string representing <paramref name="member"/>.</returns>
    public string Format(MemberInfo member)
    {
        var output = new StringBuilder();
        Format(output, member);
        return output.ToString();
    }

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="StringBuilder"/> to append a string representing <paramref name="member"/> to.</param>
    /// <param name="member">The member.</param>
    public void Format(StringBuilder output, MemberInfo member)
    {
        using var writer = new StringWriter(output);
        Format(writer, member);
    }

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="member"/> to.</param>
    /// <param name="member">The member.</param>
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

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />'s namespace.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>A string representing <paramref name="member"/>'s namespace.</returns>
    /// <exception cref="ArgumentException">If <paramref name="member"/> does not have a namespace.</exception>
    [Pure]
    public string FormatNamespace(MemberInfo member) => FormatNamespace(member.GetNamespaceOrThrow());

    /// <summary>
    /// Formats the specified namespace.
    /// </summary>
    /// <param name="namespace">The namespace.</param>
    /// <returns>A string representing <paramref name="namespace"/>.</returns>
    [Pure]
    public string FormatNamespace(string @namespace)
    {
        var output = new StringBuilder();
        FormatNamespace(output, @namespace);
        return output.ToString();
    }

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />'s namespace.
    /// </summary>
    /// <param name="output">A <see cref="StringBuilder"/> to append a string representing <paramref name="member"/>'s namespace to.</param>
    /// <param name="member">The member.</param>
    /// <exception cref="ArgumentException">If <paramref name="member"/> does not have a namespace.</exception>
    public void FormatNamespace(StringBuilder output, MemberInfo member) => FormatNamespace(output, member.GetNamespaceOrThrow());

    /// <summary>
    /// Formats the specified  namespace.
    /// </summary>
    /// <param name="output">A <see cref="StringBuilder"/> to append a string representing <paramref name="namespace"/> to.</param>
    /// <param name="namespace">The namespace.</param>
    public void FormatNamespace(StringBuilder output, string @namespace)
    {
        using var writer = new StringWriter(output);
        FormatNamespace(writer, @namespace);
    }

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />'s namespace.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="member"/>'s namespace to.</param>
    /// <param name="member">The member.</param>
    /// <exception cref="ArgumentException">If <paramref name="member"/> does not have a namespace.</exception>
    public void FormatNamespace(TextWriter output, MemberInfo member) => FormatNamespace(output, member.GetNamespaceOrThrow());

    /// <summary>
    /// Formats the specified namespace.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="namespace"/> to.</param>
    /// <param name="namespace">The namespace.</param>
    public abstract void FormatNamespace(TextWriter output, string @namespace);

    /// <summary>
    /// Formats the specified <see cref="ConstructorInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="constructor"/> to.</param>
    /// <param name="constructor">The constructor.</param>
    protected abstract void Format(TextWriter output, ConstructorInfo constructor);

    /// <summary>
    /// Formats the specified <see cref="EventInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="event"/> to.</param>
    /// <param name="event">The event.</param>
    protected abstract void Format(TextWriter output, EventInfo @event);

    /// <summary>
    /// Formats the specified <see cref="FieldInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="field"/> to.</param>
    /// <param name="field">The field.</param>
    protected abstract void Format(TextWriter output, FieldInfo field);

    /// <summary>
    /// Formats the specified <see cref="MethodInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="method"/> to.</param>
    /// <param name="method">The method.</param>
    protected abstract void Format(TextWriter output, MethodInfo method);

    /// <summary>
    /// Formats the specified <see cref="PropertyInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="property"/> to.</param>
    /// <param name="property">The property.</param>
    protected abstract void Format(TextWriter output, PropertyInfo property);

    /// <summary>
    /// Formats the specified <see cref="Type" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="type"/> to.</param>
    /// <param name="type">The type.</param>
    protected abstract void Format(TextWriter output, Type type);
}