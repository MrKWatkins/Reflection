# PropertyInfoExtensions.HasInitSetter Method
## Definition

Returns `true` if the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) has a setter marked with the init modifier; `false` otherwise.

```c#
public static bool HasInitSetter(PropertyInfo property);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| property | [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) | The property. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if `property` has a setter marked with the init modifier; `false` otherwise.
