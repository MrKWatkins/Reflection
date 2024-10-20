using System.Globalization;
using System.Reflection;

namespace MrKWatkins.Reflection.Tests.TestTypes.Methods;

// Wraps a method info but returns null from DeclaringType to simulate a global method.
public sealed class GlobalMethodInfo(MethodInfo method) : MethodInfo
{
    public override object[] GetCustomAttributes(bool inherit) => method.GetCustomAttributes(inherit);

    public override object[] GetCustomAttributes(Type attributeType, bool inherit) => method.GetCustomAttributes(attributeType, inherit);

    public override bool IsDefined(Type attributeType, bool inherit) => method.IsDefined(attributeType, inherit);

    public override Type? DeclaringType => null;

    public override string Name => method.Name;

    public override Type? ReflectedType => method.ReflectedType;

    public override MethodImplAttributes GetMethodImplementationFlags() => method.GetMethodImplementationFlags();

    public override ParameterInfo[] GetParameters() => method.GetParameters();

    public override object? Invoke(object? obj, BindingFlags invokeAttr, Binder? binder, object?[]? parameters, CultureInfo? culture) =>
        method.Invoke(obj, invokeAttr, binder, parameters, culture);

    public override MethodAttributes Attributes => method.Attributes;

    public override RuntimeMethodHandle MethodHandle => method.MethodHandle;

    public override MethodInfo GetBaseDefinition() => method.GetBaseDefinition() == method ? this : method.GetBaseDefinition();

    public override ICustomAttributeProvider ReturnTypeCustomAttributes => method.ReturnTypeCustomAttributes;
}