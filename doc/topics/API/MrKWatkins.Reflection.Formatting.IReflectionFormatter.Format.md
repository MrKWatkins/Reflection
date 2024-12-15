# IReflectionFormatter.Format Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Format(MemberInfo)](MrKWatkins.Reflection.Formatting.IReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-ireflectionformatter-format(system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo). |
| [Format(StringBuilder, MemberInfo)](MrKWatkins.Reflection.Formatting.IReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-ireflectionformatter-format(system-text-stringbuilder-system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo). |
| [Format(TextWriter, MemberInfo)](MrKWatkins.Reflection.Formatting.IReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-ireflectionformatter-format(system-io-textwriter-system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo). |

## Format(MemberInfo) {id="mrkwatkins-reflection-formatting-ireflectionformatter-format(system-reflection-memberinfo)"}

Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo).

```c#
public abstract string Format(MemberInfo member);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-ireflectionformatter-format(system-reflection-memberinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

## Returns {id="returns-mrkwatkins-reflection-formatting-ireflectionformatter-format(system-reflection-memberinfo)"}

[String](https://learn.microsoft.com/en-gb/dotnet/api/System.String)

A string representing `member`.
## Format(StringBuilder, MemberInfo) {id="mrkwatkins-reflection-formatting-ireflectionformatter-format(system-text-stringbuilder-system-reflection-memberinfo)"}

Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo).

```c#
public abstract void Format(StringBuilder output, MemberInfo member);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-ireflectionformatter-format(system-text-stringbuilder-system-reflection-memberinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [StringBuilder](https://learn.microsoft.com/en-gb/dotnet/api/System.Text.StringBuilder) | A [StringBuilder](https://learn.microsoft.com/en-gb/dotnet/api/System.Text.StringBuilder) to append a string representing `member` to. |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

## Format(TextWriter, MemberInfo) {id="mrkwatkins-reflection-formatting-ireflectionformatter-format(system-io-textwriter-system-reflection-memberinfo)"}

Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo).

```c#
public abstract void Format(TextWriter output, MemberInfo member);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-ireflectionformatter-format(system-io-textwriter-system-reflection-memberinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) | A [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) to write a string representing `member` to. |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

