# TypeExtensions.IsPublic Method
## Definition

Returns `true` if the type is public, nested or not, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool IsPublic(Type type);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) | The type. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the type is [Public](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.
## Remarks

Differs from [IsPublic](https://learn.microsoft.com/en-gb/dotnet/api/System.Type.IsPublic); that property returns `false` if the type is nested, this method returns `true` for nested types.
