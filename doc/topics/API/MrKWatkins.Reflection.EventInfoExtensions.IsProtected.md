# EventInfoExtensions.IsProtected Method
## Definition

Returns `true` if the event is protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool IsProtected(this EventInfo @event);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| event | [EventInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.EventInfo) | The event. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the event is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.
