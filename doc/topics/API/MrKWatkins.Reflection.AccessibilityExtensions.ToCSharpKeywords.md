# AccessibilityExtensions.ToCSharpKeywords Method
## Definition

Returns the equivalent of the specified [Accessibility](MrKWatkins.Reflection.Accessibility.md) in C# keywords, e.g. &quot;private protected&quot; for [PrivateProtected](MrKWatkins.Reflection.Accessibility.md#fields)

```c#
public static string ToCSharpKeywords(this Accessibility accessibility);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessibility | [Accessibility](MrKWatkins.Reflection.Accessibility.md) | The [Accessibility](MrKWatkins.Reflection.Accessibility.md). |

## Returns

[String](https://learn.microsoft.com/en-gb/dotnet/api/System.String)

The equivalent C# keywords.
