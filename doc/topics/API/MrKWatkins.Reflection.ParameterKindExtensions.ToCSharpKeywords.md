# ParameterKindExtensions.ToCSharpKeywords Method
## Definition

Returns the equivalent of the specified [ParameterKind](MrKWatkins.Reflection.ParameterKind.md) in C# keywords, e.g. &quot;params&quot; for [Params](MrKWatkins.Reflection.ParameterKind.md#fields). The empty string is returned for [Normal](MrKWatkins.Reflection.ParameterKind.md#fields).

```c#
public static string ToCSharpKeywords(ParameterKind parameterKind);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parameterKind | [ParameterKind](MrKWatkins.Reflection.ParameterKind.md) | The [ParameterKind](MrKWatkins.Reflection.ParameterKind.md). |

## Returns

[String](https://learn.microsoft.com/en-gb/dotnet/api/System.String)

The equivalent C# keywords.
