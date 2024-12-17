# MethodBaseExtensions.IsPublicOrProtected Method
## Definition

Returns `true` if the method is public or protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool IsPublicOrProtected(this MethodBase method);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| method | [MethodBase](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodBase) | The method. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the method is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.
