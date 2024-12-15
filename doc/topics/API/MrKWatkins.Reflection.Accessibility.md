# Accessibility Enum
## Definition

Describes how accessible a member is.

```c#
public sealed enum Accessibility : IComparable, ISpanFormattable, IFormattable, IConvertible
```

## Fields

| Name | Description |
| ---- | ----------- |
| [Internal](MrKWatkins.Reflection.Accessibility.md#fields) | Access is limited to the current assembly. |
| [Private](MrKWatkins.Reflection.Accessibility.md#fields) | Access is limited to the containing type. |
| [PrivateProtected](MrKWatkins.Reflection.Accessibility.md#fields) | Access is limited to the containing class or types derived from the containing class within the current assembly. |
| [Protected](MrKWatkins.Reflection.Accessibility.md#fields) | Access is limited to the containing class or types derived from the containing class. |
| [ProtectedInternal](MrKWatkins.Reflection.Accessibility.md#fields) | Access is limited to the current assembly or types derived from the containing class. |
| [Public](MrKWatkins.Reflection.Accessibility.md#fields) | Access is not restricted. |

