# VirtualityExtensions.ToCSharpKeywords Method
## Definition

Returns the equivalent of the specified [Virtuality](MrKWatkins.Reflection.Virtuality.md) in C# keywords, e.g. &quot;sealed override&quot; for [SealedOverride](MrKWatkins.Reflection.Virtuality.md#fields). The empty string is returned for [Normal](MrKWatkins.Reflection.Virtuality.md#fields).

```c#
public static string ToCSharpKeywords(Virtuality virtuality);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virtuality | [Virtuality](MrKWatkins.Reflection.Virtuality.md) | The [Virtuality](MrKWatkins.Reflection.Virtuality.md). |

## Returns

[String](https://learn.microsoft.com/en-gb/dotnet/api/System.String)

The equivalent C# keywords.
