using System.Reflection;

namespace MrKWatkins.Reflection.Tests;

public abstract class TestFixture
{
    [Pure]
    protected static ConstructorInfo GetConstructor<T>(Type[] parameterTypes) => GetConstructor(typeof(T), parameterTypes);

    [Pure]
    protected static ConstructorInfo GetConstructor(Type type, Type[] parameterTypes)
    {
        var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(c => c.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes))
            .ToList();

        if (constructors.Count == 0)
        {
            throw new ArgumentException($"Could not find a constructor with matching parameters on {type.DisplayName()}.", nameof(parameterTypes));
        }

        return constructors[0];
    }

    [Pure]
    protected static ConstructorInfo GetConstructor<T>(int parameterCount = 0) => GetConstructor(typeof(T), parameterCount);

    [Pure]
    protected static ConstructorInfo GetConstructor(Type type, int parameterCount = 0)
    {
        var constructors = GetConstructors(type, parameterCount);

        if (constructors.Count > 1)
        {
            throw new ArgumentException($"Found multiple constructors with {parameterCount} parameters on {type.DisplayName()}.", nameof(parameterCount));
        }

        return constructors[0];
    }

    [Pure]
    protected static IReadOnlyList<ConstructorInfo> GetConstructors<T>(int parameterCount = 0) => GetConstructors(typeof(T), parameterCount);

    [Pure]
    protected static IReadOnlyList<ConstructorInfo> GetConstructors(Type type, int parameterCount = 0)
    {
        var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(c => c.GetParameters().Length == parameterCount)
            .ToList();

        if (constructors.Count == 0)
        {
            throw new ArgumentException($"Could not find a constructor with {parameterCount} parameters on {type.DisplayName()}.", nameof(parameterCount));
        }

        return constructors;
    }

    [Pure]
    protected static EventInfo GetEvent<T>(string name) =>
        typeof(T).GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find event {name} on {typeof(T).Name}.", nameof(name));

    [Pure]
    protected static FieldInfo GetField<T>(string name) =>
        typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find field {name} on {typeof(T).Name}.", nameof(name));

    [Pure]
    protected static MethodInfo GetMethod<T>(string name) => GetMethod(typeof(T), name);

    [Pure]
    protected static MethodInfo GetMethod(Type type, string name) =>
        type.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        ?? throw new ArgumentException($"Could not find method {name} on {type.Name}.", nameof(name));

    [Pure]
    protected static MethodInfo GetOperator<T>(CSharpOperator @operator) => GetMethod<T>(@operator.ToMethodName());

    [Pure]
    protected static MethodInfo GetOperator(Type type, CSharpOperator @operator) => GetMethod(type, @operator.ToMethodName());

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