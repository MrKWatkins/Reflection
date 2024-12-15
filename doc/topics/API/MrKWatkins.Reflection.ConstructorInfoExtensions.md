# ConstructorInfoExtensions Class
## Definition

Extension methods for [ConstructorInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.ConstructorInfo).

```c#
public abstract sealed class ConstructorInfoExtensions
```

## Methods

| Name | Description |
| ---- | ----------- |
| [EnumerateOverloads(ConstructorInfo)](MrKWatkins.Reflection.ConstructorInfoExtensions.EnumerateOverloads.md) | Enumerates the overloads of the specified constructor. |
| [HasPublicOrProtectedOverloads(ConstructorInfo)](MrKWatkins.Reflection.ConstructorInfoExtensions.HasPublicOrProtectedOverloads.md) | Returns `true` if the specified [ConstructorInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.ConstructorInfo) has public or protected overloads, as viewed from an external assembly, i.e. their [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |

