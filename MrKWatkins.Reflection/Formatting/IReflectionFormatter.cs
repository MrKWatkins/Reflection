using System.Reflection;
using System.Text;

namespace MrKWatkins.Reflection.Formatting;

/// <summary>
/// A type for formatting reflection types.
/// </summary>
public interface IReflectionFormatter
{
    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>A string representing <paramref name="member"/>.</returns>
    [Pure]
    string Format(MemberInfo member);

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="StringBuilder"/> to append a string representing <paramref name="member"/> to.</param>
    /// <param name="member">The member.</param>
    void Format(StringBuilder output, MemberInfo member);

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="member"/> to.</param>
    /// <param name="member">The member.</param>
    void Format(TextWriter output, MemberInfo member);

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />'s namespace.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>A string representing <paramref name="member"/>'s namespace.</returns>
    /// <exception cref="ArgumentException">If <paramref name="member"/> does not have a namespace.</exception>
    [Pure]
    string FormatNamespace(MemberInfo member);

    /// <summary>
    /// Formats the specified namespace.
    /// </summary>
    /// <param name="namespace">The namespace.</param>
    /// <returns>A string representing <paramref name="namespace"/>.</returns>
    [Pure]
    string FormatNamespace(string @namespace);

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />'s namespace.
    /// </summary>
    /// <param name="output">A <see cref="StringBuilder"/> to append a string representing <paramref name="member"/>'s namespace to.</param>
    /// <param name="member">The member.</param>
    /// <exception cref="ArgumentException">If <paramref name="member"/> does not have a namespace.</exception>
    void FormatNamespace(StringBuilder output, MemberInfo member);

    /// <summary>
    /// Formats the specified  namespace.
    /// </summary>
    /// <param name="output">A <see cref="StringBuilder"/> to append a string representing <paramref name="namespace"/> to.</param>
    /// <param name="namespace">The namespace.</param>
    void FormatNamespace(StringBuilder output, string @namespace);

    /// <summary>
    /// Formats the specified <see cref="MemberInfo" />'s namespace.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="member"/>'s namespace to.</param>
    /// <param name="member">The member.</param>
    /// <exception cref="ArgumentException">If <paramref name="member"/> does not have a namespace.</exception>
    void FormatNamespace(TextWriter output, MemberInfo member);

    /// <summary>
    /// Formats the specified namespace.
    /// </summary>
    /// <param name="output">A <see cref="TextWriter"/> to write a string representing <paramref name="namespace"/> to.</param>
    /// <param name="namespace">The namespace.</param>
    void FormatNamespace(TextWriter output, string @namespace);
}