# DisplayNameFormatterOptions.UseFullyQualifiedTypes Property
## Definition

Whether types should be fully qualified with their namespace or not. Defaults to `false`.

```c#
public bool UseFullyQualifiedTypes { get; init; }
```

## Property Value

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)
## Remarks

Does not affect calls to [FormatNamespace(TextWriter, String)](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.FormatNamespace.md); they are always fully qualified.
