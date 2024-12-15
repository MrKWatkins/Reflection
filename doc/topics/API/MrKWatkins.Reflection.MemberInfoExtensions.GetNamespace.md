# MemberInfoExtensions.GetNamespace Method
## Definition

Returns the namespace the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) is contained in, or `null` if it&#39;s in the global namespace.

```c#
public static string? GetNamespace(MemberInfo member);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

## Returns

[String](https://learn.microsoft.com/en-gb/dotnet/api/System.String)

The namespace `member` is contained in, or `null` if it&#39;s in the global namespace.