namespace MrKWatkins.Reflection;

/// <summary>
/// The virtuality of a member.
/// </summary>
public enum Virtuality
{
    /// <summary>
    /// A normal member.
    /// </summary>
    Normal,

    /// <summary>
    /// An abstract member.
    /// </summary>
    Abstract,

    /// <summary>
    /// A virtual member.
    /// </summary>
    Virtual,

    /// <summary>
    /// A member that overrides a base member.
    /// </summary>
    Override,

    /// <summary>
    /// A member that overrides a base member and is sealed.
    /// </summary>
    SealedOverride,

    /// <summary>
    /// A new member that hides a base member.
    /// </summary>
    New,

    /// <summary>
    /// A new member that hides a base member and is abstract.
    /// </summary>
    NewAbstract,

    /// <summary>
    /// A new member that hides a base member and is virtual.
    /// </summary>
    NewVirtual
}