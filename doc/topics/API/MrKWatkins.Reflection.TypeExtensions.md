# TypeExtensions Class
## Definition

Extension methods for [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type).

```c#
public abstract sealed class TypeExtensions
```

## Methods

| Name | Description |
| ---- | ----------- |
| [EnumerateNestedTypes(Type)](MrKWatkins.Reflection.TypeExtensions.EnumerateNestedTypes.md) | If a type is a nested type then it enumerates its parents, starting at the outermost type, followed by the type itself. If it is not nested then it just returns the type. |
| [GetAccessibility(Type)](MrKWatkins.Reflection.TypeExtensions.GetAccessibility.md) | Returns the [Accessibility](MrKWatkins.Reflection.Accessibility.md) of the specified [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type). |
| [IsProtected(Type)](MrKWatkins.Reflection.TypeExtensions.IsProtected.md) | Returns `true` if the type is protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublic(Type)](MrKWatkins.Reflection.TypeExtensions.IsPublic.md) | Returns `true` if the type is public, nested or not, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublicOrProtected(Type)](MrKWatkins.Reflection.TypeExtensions.IsPublicOrProtected.md) | Returns `true` if the type is public or protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsReadOnlyStruct(Type)](MrKWatkins.Reflection.TypeExtensions.IsReadOnlyStruct.md) | Returns `true` if the specified [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) is a `readonly struct`; `false` otherwise. |
| [IsRecord(Type)](MrKWatkins.Reflection.TypeExtensions.IsRecord.md) | Returns `true` if the specified [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) is a `record class` or `record struct`; `false` otherwise. |
| [IsRefStruct(Type)](MrKWatkins.Reflection.TypeExtensions.IsRefStruct.md) | Returns `true` if the specified [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) is a `ref struct`; `false` otherwise. |

