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
}