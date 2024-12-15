# TypeExtensions.EnumerateNestedTypes Method
## Definition

If a type is a nested type then it enumerates its parents, starting at the outermost type, followed by the type itself. If it is not nested then it just returns the type.

```c#
public static IEnumerable<Type> EnumerateNestedTypes(Type type);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) |  |

## Returns

[IEnumerable&lt;Type&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)
