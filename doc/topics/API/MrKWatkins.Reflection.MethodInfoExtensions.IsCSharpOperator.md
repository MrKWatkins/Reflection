# MethodInfoExtensions.IsCSharpOperator Method
## Definition

Returns `true` if the specified method is a C# operator; `false` otherwise.

```c#
public static bool IsCSharpOperator(MethodInfo method);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| method | [MethodInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodInfo) | The method. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the `method` is a C# operator; `false` otherwise.
## Remarks

Checks if the method is public, static, its name matches one of the operator method names and that the method has the correct signature.
