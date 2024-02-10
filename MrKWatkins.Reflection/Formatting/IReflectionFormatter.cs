using System.Reflection;
using System.Text;

namespace MrKWatkins.Reflection.Formatting;

public interface IReflectionFormatter
{
    [Pure]
    string Format(MemberInfo member);

    void Format(StringBuilder output, MemberInfo member);

    void Format(TextWriter output, MemberInfo member);
}