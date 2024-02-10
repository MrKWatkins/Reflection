using System.Reflection;

namespace MrKWatkins.Reflection.Tests;

public abstract class TestFixture
{
    [Pure]
    protected static EventInfo GetEvent<T>(string name) =>
        typeof(T).GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find event {name} on {typeof(T).Name}.", nameof(name));

    [Pure]
    protected static FieldInfo GetField<T>(string name) =>
        typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find field {name} on {typeof(T).Name}.", nameof(name));

    [Pure]
    protected static MethodInfo GetMethod<T>(string name) =>
        typeof(T).GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find method {name} on {typeof(T).Name}.", nameof(name));

    [Pure]
    protected static PropertyInfo GetProperty<T>(string name) =>
        typeof(T).GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find property {name} on {typeof(T).DisplayName()}.", nameof(name));

    [Pure]
    protected static IReadOnlyList<PropertyInfo> GetProperties<T>(string name)
    {
        var properties = typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(p => p.Name == name)
            .ToList();

        if (properties.Count == 0)
        {
            throw new ArgumentException($"Could not find property {name} on {typeof(T).DisplayName()}.", nameof(name));
        }

        return properties;
    }
}