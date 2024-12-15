# DisplayNameFormatterOptions Record
## Definition

Options for a [DisplayNameFormatter](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.md).

```c#
public sealed record DisplayNameFormatterOptions : IEquatable<DisplayNameFormatterOptions>
```

## Constructors

| Name | Description |
| ---- | ----------- |
| [DisplayNameFormatterOptions()](MrKWatkins.Reflection.Formatting.DisplayNameFormatterOptions.-ctor.md) |  |

## Properties

| Name | Description |
| ---- | ----------- |
| [PrefixMembersWithType](MrKWatkins.Reflection.Formatting.DisplayNameFormatterOptions.PrefixMembersWithType.md) | Whether members should be prefixed with their type or not. Defaults to `true`. |
| [UseCSharpKeywordsForPrimitiveTypes](MrKWatkins.Reflection.Formatting.DisplayNameFormatterOptions.UseCSharpKeywordsForPrimitiveTypes.md) | Whether to use the C# keyword for primitive types rather than the type name, i.e. `int` instead of `Int32`. Defaults to `false`. |
| [UseFullyQualifiedTypes](MrKWatkins.Reflection.Formatting.DisplayNameFormatterOptions.UseFullyQualifiedTypes.md) | Whether types should be fully qualified with their namespace or not. Defaults to `false`. |
| [UseQuestionMarksForNullableTypes](MrKWatkins.Reflection.Formatting.DisplayNameFormatterOptions.UseQuestionMarksForNullableTypes.md) | Whether question marks should be used to identify nullable types or not, i.e. `int?` instead of `Nullable<T>`. Defaults to `true`. |

