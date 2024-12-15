namespace MrKWatkins.Reflection;

/// <summary>
/// The kind of parameter.
/// </summary>
public enum ParameterKind
{
    /// <summary>
    /// A normal parameter.
    /// </summary>
    Normal,

    /// <summary>
    /// A params parameter.
    /// </summary>
    Params,

    /// <summary>
    /// A ref parameter.
    /// </summary>
    Ref,

    /// <summary>
    /// An out parameter.
    /// </summary>
    Out,

    /// <summary>
    /// An in parameter.
    /// </summary>
    In
}