# MethodInfoExtensions Class
## Definition

Extension methods for [MethodInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodInfo).

```c#
public static class MethodInfoExtensions
```

## Methods

| Name | Description |
| ---- | ----------- |
| [EnumerateOverloads(MethodInfo)](MrKWatkins.Reflection.MethodInfoExtensions.EnumerateOverloads.md) | Enumerates the overloads of the specified method that are declared in the same type. |
| [GetCSharpOperator(MethodInfo)](MrKWatkins.Reflection.MethodInfoExtensions.GetCSharpOperator.md) | Returns the relevant [CSharpOperator](MrKWatkins.Reflection.CSharpOperator.md) value if the specified method is a C# operator; `null` otherwise. |
| [GetVirtuality(MethodInfo)](MrKWatkins.Reflection.MethodInfoExtensions.GetVirtuality.md) | Gets the [Virtuality](MrKWatkins.Reflection.Virtuality.md) of the specified [MethodInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodInfo). |
| [HasPublicOrProtectedOverloads(MethodInfo)](MrKWatkins.Reflection.MethodInfoExtensions.HasPublicOrProtectedOverloads.md) | Returns `true` if the specified [MethodInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodInfo) has public or protected overloads, as viewed from an external assembly, i.e. their [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsCSharpOperator(MethodInfo)](MrKWatkins.Reflection.MethodInfoExtensions.IsCSharpOperator.md) | Returns `true` if the specified method is a C# operator; `false` otherwise. |
| [IsExtensionMethod(MethodInfo)](MrKWatkins.Reflection.MethodInfoExtensions.IsExtensionMethod.md) | Returns `true` if the specified method is an extension method; `false` otherwise. |
| [IsNew(MethodInfo)](MrKWatkins.Reflection.MethodInfoExtensions.IsNew.md) | Returns `true` if the specified method has the `new` modifier; `false` otherwise. |
| [IsReturnNullableReferenceType(MethodInfo)](MrKWatkins.Reflection.MethodInfoExtensions.IsReturnNullableReferenceType.md) | Returns `true` if the specified method&#39;s return type is a nullable reference type. |

