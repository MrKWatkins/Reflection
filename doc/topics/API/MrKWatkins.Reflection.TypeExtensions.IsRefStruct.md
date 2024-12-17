# TypeExtensions.IsRefStruct Method
## Definition

Returns `true` if the specified [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) is a `ref struct`; `false` otherwise.

```c#
public static bool IsRefStruct(this Type type);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) | The type. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the specified [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) is a `ref struct`; `false` otherwise.
## Remarks

Equivalent to [IsByRefLike](https://learn.microsoft.com/en-gb/dotnet/api/System.Type.IsByRefLike).
