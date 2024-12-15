using System.Reflection;

namespace MrKWatkins.Reflection.Tests.TestTypes.Events;

// For testing AddMethod/RemoveMethod that returns null. Not sure that can actually happen, but the signature implies it can.
public sealed class TestEventInfo(EventInfo @event, bool hasAdd, bool hasRemove) : EventInfo
{
    public override EventAttributes Attributes => @event.Attributes;

    public override Type? DeclaringType => @event.DeclaringType;

    public override string Name => @event.Name;

    public override Type? ReflectedType => @event.ReflectedType;

    public override object[] GetCustomAttributes(bool inherit) => @event.GetCustomAttributes(inherit);

    public override object[] GetCustomAttributes(Type attributeType, bool inherit) => @event.GetCustomAttributes(attributeType, inherit);

    public override bool IsDefined(Type attributeType, bool inherit) => @event.IsDefined(attributeType, inherit);

    public override MethodInfo? GetAddMethod(bool nonPublic) => hasAdd ? @event.GetAddMethod(nonPublic) : null;

    public override MethodInfo? GetRaiseMethod(bool nonPublic) => @event.GetRaiseMethod(nonPublic);

    public override MethodInfo? GetRemoveMethod(bool nonPublic) => hasRemove ? @event.GetRemoveMethod(nonPublic) : null;
}