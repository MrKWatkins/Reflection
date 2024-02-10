using System.Reflection;

namespace MrKWatkins.Reflection.Tests;

public abstract class TestFixture
{
    [Pure]
    protected static MethodInfo GetMethod<T>(string name) =>
        typeof(T).GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new InvalidOperationException($"Could not find method {name} on {typeof(T).Name}.");

    [Pure]
    protected static PropertyInfo GetProperty<T>(string name) =>
        typeof(T).GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new InvalidOperationException($"Could not find property {name} on {typeof(T).DisplayName()}.");

    [Pure]
    protected static IReadOnlyList<PropertyInfo> GetProperties<T>(string name)
    {
        var properties = typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(p => p.Name == name)
            .ToList();

        if (properties.Count == 0)
        {
            throw new InvalidOperationException($"Could not find property {name} on {typeof(T).DisplayName()}.");
        }

        return properties;
    }
}