# ReflectionFormatter.Format Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Format(MemberInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-reflectionformatter-format(system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo). |
| [Format(StringBuilder, MemberInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-reflectionformatter-format(system-text-stringbuilder-system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo). |
| [Format(TextWriter, MemberInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-memberinfo)) | Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo). |
| [Format(TextWriter, ConstructorInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-constructorinfo)) | Formats the specified [ConstructorInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.ConstructorInfo). |
| [Format(TextWriter, EventInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-eventinfo)) | Formats the specified [EventInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.EventInfo). |
| [Format(TextWriter, FieldInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-fieldinfo)) | Formats the specified [FieldInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.FieldInfo). |
| [Format(TextWriter, MethodInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-methodinfo)) | Formats the specified [MethodInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodInfo). |
| [Format(TextWriter, PropertyInfo)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-propertyinfo)) | Formats the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo). |
| [Format(TextWriter, Type)](MrKWatkins.Reflection.Formatting.ReflectionFormatter.Format.md#mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-type)) | Formats the specified [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type). |

## Format(MemberInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-format(system-reflection-memberinfo)"}

Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo).

```c#
public string Format(MemberInfo member);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-format(system-reflection-memberinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

## Returns {id="returns-mrkwatkins-reflection-formatting-reflectionformatter-format(system-reflection-memberinfo)"}

[String](https://learn.microsoft.com/en-gb/dotnet/api/System.String)

A string representing `member`.
## Format(StringBuilder, MemberInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-format(system-text-stringbuilder-system-reflection-memberinfo)"}

Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo).

```c#
public void Format(StringBuilder output, MemberInfo member);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-format(system-text-stringbuilder-system-reflection-memberinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [StringBuilder](https://learn.microsoft.com/en-gb/dotnet/api/System.Text.StringBuilder) | A [StringBuilder](https://learn.microsoft.com/en-gb/dotnet/api/System.Text.StringBuilder) to append a string representing `member` to. |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

## Format(TextWriter, MemberInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-memberinfo)"}

Formats the specified [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo).

```c#
public void Format(TextWriter output, MemberInfo member);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-memberinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) | A [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) to write a string representing `member` to. |
| member | [MemberInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MemberInfo) | The member. |

## Format(TextWriter, ConstructorInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-constructorinfo)"}

Formats the specified [ConstructorInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.ConstructorInfo).

```c#
protected abstract void Format(TextWriter output, ConstructorInfo constructor);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-constructorinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) | A [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) to write a string representing `constructor` to. |
| constructor | [ConstructorInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.ConstructorInfo) | The constructor. |

## Format(TextWriter, EventInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-eventinfo)"}

Formats the specified [EventInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.EventInfo).

```c#
protected abstract void Format(TextWriter output, EventInfo @event);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-eventinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) | A [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) to write a string representing `event` to. |
| event | [EventInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.EventInfo) | The event. |

## Format(TextWriter, FieldInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-fieldinfo)"}

Formats the specified [FieldInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.FieldInfo).

```c#
protected abstract void Format(TextWriter output, FieldInfo field);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-fieldinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) | A [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) to write a string representing `field` to. |
| field | [FieldInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.FieldInfo) | The field. |

## Format(TextWriter, MethodInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-methodinfo)"}

Formats the specified [MethodInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodInfo).

```c#
protected abstract void Format(TextWriter output, MethodInfo method);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-methodinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) | A [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) to write a string representing `method` to. |
| method | [MethodInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.MethodInfo) | The method. |

## Format(TextWriter, PropertyInfo) {id="mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-propertyinfo)"}

Formats the specified [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo).

```c#
protected abstract void Format(TextWriter output, PropertyInfo property);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-reflection-propertyinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) | A [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) to write a string representing `property` to. |
| property | [PropertyInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.Reflection.PropertyInfo) | The property. |

## Format(TextWriter, Type) {id="mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-type)"}

Formats the specified [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type).

```c#
protected abstract void Format(TextWriter output, Type type);
```

## Parameters {id="parameters-mrkwatkins-reflection-formatting-reflectionformatter-format(system-io-textwriter-system-type)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| output | [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) | A [TextWriter](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.TextWriter) to write a string representing `type` to. |
| type | [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) | The type. |

