# PropertyInfoExtensions Class
## Definition

Extension methods for [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo).

```c#
public static class PropertyInfoExtensions
```

## Methods

| Name | Description |
| ---- | ----------- |
| [EnumerateOverloads(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.EnumerateOverloads.md) | Enumerates the overloads of the specified property that are declared in the same type. Only indexer properties can be overloaded. |
| [GetAccessibility(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.GetAccessibility.md) | Returns the [Accessibility](MrKWatkins.Reflection.Accessibility.md) of the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo). |
| [GetBaseDefinition(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.GetBaseDefinition.md) | If the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) overrides a property in a base class then this returns the base [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo), otherwise it returns the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo). |
| [GetVirtuality(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.GetVirtuality.md) | Gets the [Virtuality](MrKWatkins.Reflection.Virtuality.md) of the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo). |
| [HasInitSetter(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.HasInitSetter.md) | Returns `true` if the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) has a setter marked with the init modifier; `false` otherwise. |
| [HasPublicOrProtectedOverloads(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.HasPublicOrProtectedOverloads.md) | Returns `true` if the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) has public or protected overloads, as viewed from an external assembly, i.e. their [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. Only applies to indexer properties. |
| [IsAbstract(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.IsAbstract.md) | Returns `true` if the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) is abstract; `false` otherwise. |
| [IsAbstractOrVirtual(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.IsAbstractOrVirtual.md) | Returns `true` if the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) is abstract or virtual; `false` otherwise. |
| [IsIndexer(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.IsIndexer.md) | Returns `true` if the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) is an indexer property; `false` otherwise. |
| [IsNew(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.IsNew.md) | Returns `true` if the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) is a property marked with the new modifier; `false` otherwise. |
| [IsProtected(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.IsProtected.md) | Returns `true` if the property is protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublic(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.IsPublic.md) | Returns `true` if the property is public, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublicOrProtected(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.IsPublicOrProtected.md) | Returns `true` if the property is public or protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsRequired(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.IsRequired.md) | Returns `true` if the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) represents a property marked with the required modifier; `false` otherwise. |
| [IsStatic(PropertyInfo)](MrKWatkins.Reflection.PropertyInfoExtensions.IsStatic.md) | Returns `true` if the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) represents a static property; `false` otherwise. |

