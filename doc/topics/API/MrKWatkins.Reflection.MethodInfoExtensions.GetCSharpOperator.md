# MethodInfoExtensions.GetCSharpOperator Method
## Definition

Returns the relevant [CSharpOperator](MrKWatkins.Reflection.CSharpOperator.md) value if the specified method is a C# operator; `null` otherwise.

```c#
public static CSharpOperator? GetCSharpOperator(this MethodInfo method);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| method | [MethodInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodInfo) | The method. |

## Returns

[CSharpOperator?](https://learn.microsoft.com/en-gb/dotnet/api/System.Nullable-1)

A [CSharpOperator](MrKWatkins.Reflection.CSharpOperator.md) value if the specified method is a C# operator; `null` otherwise.
## Remarks

Checks if the method is public, static, its name matches one of the operator method names and that the method has the correct signature.
