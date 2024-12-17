# EventInfoExtensions Class
## Definition

Extension methods for [EventInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.EventInfo).

```c#
public static class EventInfoExtensions
```

## Methods

| Name | Description |
| ---- | ----------- |
| [GetAccessibility(EventInfo)](MrKWatkins.Reflection.EventInfoExtensions.GetAccessibility.md) | Returns the [Accessibility](MrKWatkins.Reflection.Accessibility.md) of the specified [EventInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.EventInfo). |
| [IsProtected(EventInfo)](MrKWatkins.Reflection.EventInfoExtensions.IsProtected.md) | Returns `true` if the event is protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublic(EventInfo)](MrKWatkins.Reflection.EventInfoExtensions.IsPublic.md) | Returns `true` if the event is public, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublicOrProtected(EventInfo)](MrKWatkins.Reflection.EventInfoExtensions.IsPublicOrProtected.md) | Returns `true` if the event is public or protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |

