# MethodInfoExtensions.EnumerateOverloads Method
## Definition

Enumerates the overloads of the specified method that are declared in the same type.

```c#
public static IEnumerable<MethodInfo> EnumerateOverloads(MethodInfo method);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| method | [MethodInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodInfo) | The method. |

## Returns

[IEnumerable&lt;MethodInfo&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

The overloads of `method` declared in the same type; will be empty if the method is not overloaded.
