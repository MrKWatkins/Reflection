# PropertyInfoExtensions.GetBaseDefinition Method
## Definition

If the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) overrides a property in a base class then this returns the base [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo), otherwise it returns the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo).

```c#
public static PropertyInfo GetBaseDefinition(this PropertyInfo property);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| property | [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) | The property. |

## Returns

[PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo)

The [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) in the base class `property` overrides, otherwise `property`.
