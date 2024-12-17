# PropertyInfoExtensions.EnumerateOverloads Method
## Definition

Enumerates the overloads of the specified property that are declared in the same type. Only indexer properties can be overloaded.

```c#
public static IEnumerable<PropertyInfo> EnumerateOverloads(this PropertyInfo property);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| property | [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) | The property. |

## Returns

[IEnumerable&lt;PropertyInfo&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

The overloads of `property` declared in the same type; will be empty if the property is not overloaded or is not an indexer property.
