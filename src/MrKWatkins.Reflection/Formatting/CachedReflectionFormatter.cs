using System.Collections.Concurrent;
using System.Reflection;
using System.Text;

namespace MrKWatkins.Reflection.Formatting;

/// <summary>
/// An implementation of <see cref="IReflectionFormatter" /> that wraps an underlying formatter with a thread-safe cache.
/// </summary>
public sealed class CachedReflectionFormatter : IReflectionFormatter
{
    private readonly ConcurrentDictionary<MemberInfo, string> memberCache = new();
    private readonly ConcurrentDictionary<string, string> namespaceCache = new();
    private readonly IReflectionFormatter formatter;

    /// <summary>
    /// Initialises a new instance of the <see cref="CachedReflectionFormatter"/> class.
    /// </summary>
    /// <param name="formatter">The underlying formatter.</param>
    /// <exception cref="ArgumentException">If <paramref name="formatter"/> is a <see cref="CachedReflectionFormatter" />.</exception>
    public CachedReflectionFormatter(IReflectionFormatter formatter)
    {
        if (formatter is CachedReflectionFormatter)
        {
            throw new ArgumentException($"Value is already a {nameof(CachedReflectionFormatter)}.", nameof(formatter));
        }

        this.formatter = formatter;
    }

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>A string representing <paramref name="member"/>.</returns>
    public string Format(MemberInfo member) => memberCache.GetOrAdd(member, formatter.Format);

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="StringBuilder"/> to append a string representing <paramref name="member"/> to.</param>
    /// <param name="member">The member.</param>
    public void Format(StringBuilder output, MemberInfo member) => output.Append(Format(member));

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="member"/> to.</param>
    /// <param name="member">The member.</param>
    public void Format(TextWriter output, MemberInfo member) => output.Write(Format(member));

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
    public string FormatNamespace(string @namespace) => namespaceCache.GetOrAdd(@namespace, formatter.FormatNamespace);

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />'s namespace.
    /// </summary>
    /// <param name="output">A <see cref="StringBuilder"/> to append a string representing <paramref name="member"/>'s namespace to.</param>
    /// <param name="member">The member.</param>
    public void FormatNamespace(StringBuilder output, MemberInfo member) => output.Append(FormatNamespace(member));

    /// <summary>
    /// Formats the specified  namespace.
    /// </summary>
    /// <param name="output">A <see cref="StringBuilder"/> to append a string representing <paramref name="namespace"/> to.</param>
    /// <param name="namespace">The namespace.</param>
    public void FormatNamespace(StringBuilder output, string @namespace) => output.Append(FormatNamespace(@namespace));

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />'s namespace.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="member"/>'s namespace to.</param>
    /// <param name="member">The member.</param>
    public void FormatNamespace(TextWriter output, MemberInfo member) => output.Write(FormatNamespace(member));

    /// <summary>
    /// Formats the specified namespace.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="namespace"/> to.</param>
    /// <param name="namespace">The namespace.</param>
    public void FormatNamespace(TextWriter output, string @namespace) => output.Write(FormatNamespace(@namespace));
}