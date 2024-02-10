using System.Collections.Concurrent;
using System.Reflection;
using System.Text;

namespace MrKWatkins.Reflection.Formatting;

public sealed class CachedReflectionFormatter : IReflectionFormatter
{
    private readonly ConcurrentDictionary<MemberInfo, string> cache = new();
    private readonly IReflectionFormatter formatter;

    public CachedReflectionFormatter(IReflectionFormatter formatter)
    {
        if (formatter is CachedReflectionFormatter)
        {
            throw new ArgumentException($"Value is already a {nameof(CachedReflectionFormatter)}.", nameof(formatter));
        }

        this.formatter = formatter;
    }

    public string Format(MemberInfo member) => cache.GetOrAdd(member, formatter.Format);

    public void Format(StringBuilder output, MemberInfo member) => output.Append(Format(member));

    public void Format(TextWriter output, MemberInfo member) => output.Write(Format(member));
}