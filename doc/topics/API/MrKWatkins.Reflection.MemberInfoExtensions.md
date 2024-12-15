# MemberInfoExtensions Class
## Definition

Extension methods for [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo).

```c#
public abstract sealed class MemberInfoExtensions
```

## Methods

| Name | Description |
| ---- | ----------- |
| [GetAccessibility(MemberInfo)](MrKWatkins.Reflection.MemberInfoExtensions.GetAccessibility.md) | Returns the [Accessibility](MrKWatkins.Reflection.Accessibility.md) of the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo). |
| [GetNamespace(MemberInfo)](MrKWatkins.Reflection.MemberInfoExtensions.GetNamespace.md) | Returns the namespace the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) is contained in, or `null` if it&#39;s in the global namespace. |
| [GetNamespaceOrThrow(MemberInfo)](MrKWatkins.Reflection.MemberInfoExtensions.GetNamespaceOrThrow.md) | Returns the namespace the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) is contained in, or throws if it&#39;s in the global namespace. |
| [IsProtected(MemberInfo)](MrKWatkins.Reflection.MemberInfoExtensions.IsProtected.md) | Returns `true` if the member is protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublic(MemberInfo)](MrKWatkins.Reflection.MemberInfoExtensions.IsPublic.md) | Returns `true` if the member is public, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [IsPublicOrProtected(MemberInfo)](MrKWatkins.Reflection.MemberInfoExtensions.IsPublicOrProtected.md) | Returns `true` if the member is public or protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Public](MrKWatkins.Reflection.Accessibility.md#fields), [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise. |
| [ToDisplayName(MemberInfo)](MrKWatkins.Reflection.MemberInfoExtensions.ToDisplayName.md) | Returns a display name for the member. Created using [DisplayNameFormatter](MrKWatkins.Reflection.MemberInfoExtensions.DisplayNameFormatter.md) with default options. |

