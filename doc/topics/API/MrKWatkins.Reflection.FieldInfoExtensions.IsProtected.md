# FieldInfoExtensions.IsProtected Method
## Definition

Returns `true` if the field is protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool IsProtected(this FieldInfo field);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| field | [FieldInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.FieldInfo) | The field. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the field is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.
