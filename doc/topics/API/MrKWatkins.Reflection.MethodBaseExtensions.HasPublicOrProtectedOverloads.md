# MethodBaseExtensions.HasPublicOrProtectedOverloads Method
## Definition

Returns `true` if the specified [MethodBase](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodBase) has public or protected overloads, as viewed from an external assembly, i.e. their [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool HasPublicOrProtectedOverloads(this MethodBase method);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| method | [MethodBase](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodBase) | The method. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if `method` has public or protected overloads; `false` otherwise.
