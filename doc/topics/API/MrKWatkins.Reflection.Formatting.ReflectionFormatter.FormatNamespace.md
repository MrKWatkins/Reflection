# ReflectionFormatter.FormatNamespace Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [FormatNamespace(MemberInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo)&#39;s namespace. |
| [FormatNamespace(String)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-string)) | Formats the specified namespace. |
| [FormatNamespace(StringBuilder, MemberInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-text-stringbuilder-system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo)&#39;s namespace. |
| [FormatNamespace(StringBuilder, String)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-text-stringbuilder-system-string)) | Formats the specified namespace. |
| [FormatNamespace(TextWriter, MemberInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-io-textwriter-system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo)&#39;s namespace. |
| [FormatNamespace(TextWriter, String)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.FormatNamespace.md#mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-io-textwriter-system-string)) | Formats the specified namespace. |

## FormatNamespace(MemberInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-reflection-memberinfo)"}

Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo)&#39;s namespace.

```c#
public string FormatNamespace(MemberInfo member);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-reflection-memberinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

## Returns {id="returns-mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-reflection-memberinfo)"}

[String](https://learn.microsoft.com/en-gb/dotnet/api/System.String)

A string representing `member`&#39;s namespace.
## FormatNamespace(String) {id="mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-string)"}

Formats the specified namespace.

```c#
public string FormatNamespace(string @namespace);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| namespace | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The namespace. |

## Returns {id="returns-mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-string)"}

[String](https://learn.microsoft.com/en-gb/dotnet/api/System.String)

A string representing `namespace`.
## FormatNamespace(StringBuilder, MemberInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-text-stringbuilder-system-reflection-memberinfo)"}

Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo)&#39;s namespace.

```c#
public void FormatNamespace(StringBuilder output, MemberInfo member);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-text-stringbuilder-system-reflection-memberinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [StringBuilder](https://learn.microsoft.com/en-gb/dotnet/api/System.Text.StringBuilder) | A [StringBuilder](https://learn.microsoft.com/en-gb/dotnet/api/System.Text.StringBuilder) to append a string representing `member`&#39;s namespace to. |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

## FormatNamespace(StringBuilder, String) {id="mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-text-stringbuilder-system-string)"}

Formats the specified namespace.

```c#
public void FormatNamespace(StringBuilder output, string @namespace);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-text-stringbuilder-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [StringBuilder](https://learn.microsoft.com/en-gb/dotnet/api/System.Text.StringBuilder) | A [StringBuilder](https://learn.microsoft.com/en-gb/dotnet/api/System.Text.StringBuilder) to append a string representing `namespace` to. |
| namespace | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The namespace. |

## FormatNamespace(TextWriter, MemberInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-io-textwriter-system-reflection-memberinfo)"}

Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo)&#39;s namespace.

```c#
public void FormatNamespace(TextWriter output, MemberInfo member);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-io-textwriter-system-reflection-memberinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) | A [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) to write a string representing `member`&#39;s namespace to. |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

## FormatNamespace(TextWriter, String) {id="mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-io-textwriter-system-string)"}

Formats the specified namespace.

```c#
public abstract void FormatNamespace(TextWriter output, string @namespace);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-formatnamespace(system-io-textwriter-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) | A [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) to write a string representing `namespace` to. |
| namespace | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The namespace. |

