# TypeExtensions.IsPublicOrProtected Method
## Definition

Returns `true` if the type is public or protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool IsPublicOrProtected(this Type type);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) | The type. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the type is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.
