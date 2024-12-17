# MethodBaseExtensions Class
## Definition

Extension methods for [MethodBase](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodBase).

```c#
public static class MethodBaseExtensions
```

## Methods

| Name | Description |
| ---- | ----------- |
| [EnumerateOverloads(MethodBase)](MrKWatkins.Reflection.MethodBaseExtensions.EnumerateOverloads.md) | Enumerates the overloads of the specified method that are declared in the same type. |
| [GetAccessibility(MethodBase)](MrKWatkins.Reflection.MethodBaseExtensions.GetAccessibility.md) | Returns the [Accessibility](MrKWatkins.Reflection.Accessibility.md) of the specified [MethodBase](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodBase). |
| [HasPublicOrProtectedOverloads(MethodBase)](MrKWatkins.Reflection.MethodBaseExtensions.HasPublicOrProtectedOverloads.md) | Returns `true` if the specified [MethodBase](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodBase) has public or protected overloads, as viewed from an external assembly, i.e. their [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsProtected(MethodBase)](MrKWatkins.Reflection.MethodBaseExtensions.IsProtected.md) | Returns `true` if the method is protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublic(MethodBase)](MrKWatkins.Reflection.MethodBaseExtensions.IsPublic.md) | Returns `true` if the method is public, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublicOrProtected(MethodBase)](MrKWatkins.Reflection.MethodBaseExtensions.IsPublicOrProtected.md) | Returns `true` if the method is public or protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |

