# EventInfoExtensions.IsPublicOrProtected Method
## Definition

Returns `true` if the event is public or protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool IsPublicOrProtected(this EventInfo @event);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| event | [EventInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.EventInfo) | The event. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the event is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.
