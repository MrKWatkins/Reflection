# MemberInfoExtensions.IsProtected Method
## Definition

Returns `true` if the member is protected as viewed from an external assembly, i.e. its [Accessibility](MrKWatkins.Reflection.Accessibility.md) is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.

```c#
public static bool IsProtected(this MemberInfo member);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the member is [Protected](MrKWatkins.Reflection.Accessibility.md#fields) or [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields); `false` otherwise.
