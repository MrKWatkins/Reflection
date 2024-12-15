# TypeExtensions.IsProtected Method
## Definition

Returns `true` if the type is protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool IsProtected(Type type);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) | The type. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the type is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.
