# ConstructorInfoExtensions.HasPublicOrProtectedOverloads Method
## Definition

Returns `true` if the specified [ConstructorInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.ConstructorInfo) has public or protected overloads, as viewed from an external assembly, i.e. their [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool HasPublicOrProtectedOverloads(this ConstructorInfo constructor);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| constructor | [ConstructorInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.ConstructorInfo) | The method. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if `constructor` has public or protected overloads; `false` otherwise.
