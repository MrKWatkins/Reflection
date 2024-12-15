using System.Globalization;
using System.Reflection;

namespace MrKWatkins.Reflection.Tests.TestTypes;

public sealed class UnsupportedMethodBase : MethodBase
{
    public override object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();
    public override object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();
    public override bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();
    public override Type DeclaringType => throw new NotSupportedException();
    public override MemberTypes MemberType => throw new NotSupportedException();
    public override string Name => throw new NotSupportedException();
    public override Type ReflectedType => throw new NotSupportedException();
    public override MethodImplAttributes GetMethodImplementationFlags() => throw new NotSupportedException();
    public override ParameterInfo[] GetParameters() => throw new NotSupportedException();
    public override object Invoke(object? obj, BindingFlags invokeAttr, Binder? binder, object?[]? parameters, CultureInfo? culture) => throw new NotSupportedException();
    public override MethodAttributes Attributes => throw new NotSupportedException();
    public override RuntimeMethodHandle MethodHandle => throw new NotSupportedException();
}