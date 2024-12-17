# PropertyInfoExtensions.IsPublicOrProtected Method
## Definition

Returns `true` if the property is public or protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool IsPublicOrProtected(this PropertyInfo property);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| property | [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) | The property. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if `property` is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.
