# CachedReflectionFormatter Class
## Definition

An implementation of [IReflectionFormatter](MrKWatkins.Reflection.Formatting.IReflectionFormatter.md) that wraps an underlying formatter with a thread-safe cache.

```c#
public sealed class CachedReflectionFormatter : IReflectionFormatter
```

## Constructors

| Name | Description |
| ---- | ----------- |
| [CachedReflectionFormatter(IReflectionFormatter)](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.-ctor.md) | Initialises a new instance of the [CachedReflectionFormatter](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.md) class. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Format(MemberInfo)](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-cachedreflectionformatter-format(system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo). |
| [Format(StringBuilder, MemberInfo)](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-cachedreflectionformatter-format(system-text-stringbuilder-system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo). |
| [Format(TextWriter, MemberInfo)](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-cachedreflectionformatter-format(system-io-textwriter-system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo). |
| [FormatNamespace(MemberInfo)](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-cachedreflectionformatter-formatnamespace(system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo)&#39;s namespace. |
| [FormatNamespace(String)](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-cachedreflectionformatter-formatnamespace(system-string)) | Formats the specified namespace. |
| [FormatNamespace(StringBuilder, MemberInfo)](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-cachedreflectionformatter-formatnamespace(system-text-stringbuilder-system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo)&#39;s namespace. |
| [FormatNamespace(StringBuilder, String)](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-cachedreflectionformatter-formatnamespace(system-text-stringbuilder-system-string)) | Formats the specified namespace. |
| [FormatNamespace(TextWriter, MemberInfo)](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-cachedreflectionformatter-formatnamespace(system-io-textwriter-system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo)&#39;s namespace. |
| [FormatNamespace(TextWriter, String)](MrKWatkins.Reflection.Formatting.CachedReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-cachedreflectionformatter-formatnamespace(system-io-textwriter-system-string)) | Formats the specified namespace. |

