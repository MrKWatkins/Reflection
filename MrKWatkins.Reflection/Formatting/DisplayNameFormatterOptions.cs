namespace MrKWatkins.Reflection.Formatting;

[Flags]
public enum DisplayNameFormatterOptions
{
    None = 0,
    UseQualifiedNames = 1,
    DoNotPrefixMembersWithType = 2
}