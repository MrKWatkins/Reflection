# MethodBaseExtensions.EnumerateOverloads Method
## Definition

Enumerates the overloads of the specified method that are declared in the same type.

```c#
public static IEnumerable<MethodBase> EnumerateOverloads(MethodBase method);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| method | [MethodBase](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodBase) | The method. |

## Returns

[IEnumerable&lt;MethodBase&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

The overloads of `method` declared in the same type; will be empty if the method is not overloaded.
