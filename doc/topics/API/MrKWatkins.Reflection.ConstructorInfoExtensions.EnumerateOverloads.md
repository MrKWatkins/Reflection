# ConstructorInfoExtensions.EnumerateOverloads Method
## Definition

Enumerates the overloads of the specified constructor.

```c#
public static IEnumerable<ConstructorInfo> EnumerateOverloads(this ConstructorInfo constructor);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| constructor | [ConstructorInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.ConstructorInfo) | The constructor. |

## Returns

[IEnumerable&lt;ConstructorInfo&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

The overloads of `constructor`; will be empty if the method is not overloaded.
