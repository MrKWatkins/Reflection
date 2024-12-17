# FieldInfoExtensions Class
## Definition

Extension methods for [FieldInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.FieldInfo).

```c#
public static class FieldInfoExtensions
```

## Methods

| Name | Description |
| ---- | ----------- |
| [GetAccessibility(FieldInfo)](MrKWatkins.Reflection.FieldInfoExtensions.GetAccessibility.md) | Returns the [Accessibility](MrKWatkins.Reflection.Accessibility.md) of the specified [FieldInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.FieldInfo). |
| [IsConst(FieldInfo)](MrKWatkins.Reflection.FieldInfoExtensions.IsConst.md) | Returns `true` if the field is const; `false` otherwise. |
| [IsProtected(FieldInfo)](MrKWatkins.Reflection.FieldInfoExtensions.IsProtected.md) | Returns `true` if the field is protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublic(FieldInfo)](MrKWatkins.Reflection.FieldInfoExtensions.IsPublic.md) | Returns `true` if the field is public, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublicOrProtected(FieldInfo)](MrKWatkins.Reflection.FieldInfoExtensions.IsPublicOrProtected.md) | Returns `true` if the field is public or protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsReadOnly(FieldInfo)](MrKWatkins.Reflection.FieldInfoExtensions.IsReadOnly.md) | Returns `true` if the field is `readonly`; `false` otherwise. |

