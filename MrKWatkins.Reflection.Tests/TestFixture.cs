using System.Reflection;

namespace MrKWatkins.Reflection.Tests;

public abstract class TestFixture
{
    [Pure]
    protected static ConstructorInfo GetConstructor(Type type, Type[] parameterTypes)
    {
        var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(c => c.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes))
            .ToList();

        if (constructors.Count == 0)
        {
            throw new ArgumentException($"Could not find a constructor with matching parameters on {type.ToDisplayName()}.", nameof(parameterTypes));
        }

        return constructors[0];
    }

    [Pure]
    protected static ConstructorInfo GetConstructor<T>(int parameterCount) => GetConstructor(typeof(T), parameterCount);

    [Pure]
    protected static ConstructorInfo GetConstructor(Type type, int parameterCount)
    {
        var constructors = GetConstructors(type, parameterCount);

        if (constructors.Count > 1)
        {
            throw new ArgumentException($"Found multiple constructors with {parameterCount} parameters on {type.ToDisplayName()}.", nameof(parameterCount));
        }

        return constructors[0];
    }

    [Pure]
    protected static IReadOnlyList<ConstructorInfo> GetConstructors<T>() => GetConstructors(typeof(T));

    [Pure]
    private static IReadOnlyList<ConstructorInfo> GetConstructors(Type type) =>
        type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

    [Pure]
    private static IReadOnlyList<ConstructorInfo> GetConstructors(Type type, int parameterCount)
    {
        var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(c => c.GetParameters().Length == parameterCount)
            .ToList();

        if (constructors.Count == 0)
        {
            throw new ArgumentException($"Could not find a constructor with {parameterCount} parameters on {type.ToDisplayName()}.", nameof(parameterCount));
        }

        return constructors;
    }

    [Pure]
    protected static EventInfo GetEvent<T>(string name) =>
        typeof(T).GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find an event {name} on {typeof(T).Name}.", nameof(name));

    [Pure]
    protected static FieldInfo GetField<T>(string name) =>
        typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find a field {name} on {typeof(T).Name}.", nameof(name));

    [Pure]
    protected static MethodInfo GetMethod<T>(string name) => GetMethod(typeof(T), name);

    [Pure]
    protected static MethodInfo GetMethod(Type type, string name) =>
        type.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find a method {name} on {type.Name}.", nameof(name));

    [Pure]
    protected static IReadOnlyList<MethodInfo> GetMethods<T>(string name)
    {
        var methods = typeof(T)
            .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m.Name == name)
            .ToList();

        if (methods.Count == 0)
        {
            throw new ArgumentException($"Could not find a method {name} on {typeof(T).ToDisplayName()}.", nameof(name));
        }

        return methods;
    }

    [Pure]
    protected static MethodInfo GetOperator<T>(CSharpOperator @operator) => GetMethod<T>(@operator.ToMethodName());

    [Pure]
    protected static PropertyInfo GetProperty<T>(string name) =>
        typeof(T).GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find a property {name} on {typeof(T).ToDisplayName()}.", nameof(name));

    [Pure]
    protected static PropertyInfo GetIndexer<T>() => GetProperty<T>("Item");

    [Pure]
    protected static PropertyInfo GetIndexer<T>(string parameterName)
    {
        var indexers = GetIndexers<T>()
            .Where(i =>
            {
                var parameters = i.GetIndexParameters();
                return parameters.Length == 1 && parameters[0].Name!.Equals(parameterName, StringComparison.OrdinalIgnoreCase);
            })
            .ToList();

        if (indexers.Count != 1)
        {
            throw new ArgumentException($"Could not find a single indexer with a single parameter named {parameterName} on {typeof(T).ToDisplayName()}.", nameof(parameterName));
        }

        return indexers[0];
    }

    [Pure]
    private static IReadOnlyList<PropertyInfo> GetProperties<T>(string name)
    {
        var properties = typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(p => p.Name == name)
            .ToList();

        if (properties.Count == 0)
        {
            throw new ArgumentException($"Could not find a property {name} on {typeof(T).ToDisplayName()}.", nameof(name));
        }

        return properties;
    }

    [Pure]
    protected static IReadOnlyList<PropertyInfo> GetIndexers<T>() => GetProperties<T>("Item");
}