# DisplayNameFormatter Class
## Definition

Formats reflection types into a human readable display name.

```c#
public sealed class DisplayNameFormatter : ReflectionFormatter, IReflectionFormatter
```

## Constructors

| Name | Description |
| ---- | ----------- |
| [DisplayNameFormatter()](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.-ctor.md#mrkwatkins-reflection-formatting-displaynameformatter-ctor) | Initialises a new instance of the [DisplayNameFormatter](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.md) class using default options. |
| [DisplayNameFormatter(DisplayNameFormatterOptions)](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.-ctor.md#mrkwatkins-reflection-formatting-displaynameformatter-ctor(mrkwatkins-reflection-formatting-displaynameformatteroptions)) | Initialises a new instance of the [DisplayNameFormatter](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.md) class with the specified options. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Format(TextWriter, ConstructorInfo)](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.Format.md#mrkwatkins-reflection-formatting-displaynameformatter-format(system-io-textwriter-system-reflection-constructorinfo)) | Formats the specified [ConstructorInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.ConstructorInfo). |
| [Format(TextWriter, EventInfo)](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.Format.md#mrkwatkins-reflection-formatting-displaynameformatter-format(system-io-textwriter-system-reflection-eventinfo)) | Formats the specified [EventInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.EventInfo). |
| [Format(TextWriter, FieldInfo)](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.Format.md#mrkwatkins-reflection-formatting-displaynameformatter-format(system-io-textwriter-system-reflection-fieldinfo)) | Formats the specified [FieldInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.FieldInfo). |
| [Format(TextWriter, MethodInfo)](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.Format.md#mrkwatkins-reflection-formatting-displaynameformatter-format(system-io-textwriter-system-reflection-methodinfo)) | Formats the specified [MethodInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodInfo). |
| [Format(TextWriter, PropertyInfo)](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.Format.md#mrkwatkins-reflection-formatting-displaynameformatter-format(system-io-textwriter-system-reflection-propertyinfo)) | Formats the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo). |
| [Format(TextWriter, Type)](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.Format.md#mrkwatkins-reflection-formatting-displaynameformatter-format(system-io-textwriter-system-type)) | Formats the specified [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type). |
| [FormatNamespace(TextWriter, String)](MrKWatkins.Reflection.Formatting.DisplayNameFormatter.FormatNamespace.md) | Formats the specified namespace. |

