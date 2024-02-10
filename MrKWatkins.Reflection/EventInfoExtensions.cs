using System.Reflection;

namespace MrKWatkins.Reflection;

public static class EventInfoExtensions
{
    /// <summary>
    /// Returns the <see cref="Accessibility" /> of the specified <see cref="EventInfo" />.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <returns>
    /// The <see cref="Accessibility" /> of the <paramref name="event"/>.
    /// </returns>
    [Pure]
    public static Accessibility GetAccessibility(this EventInfo @event)
    {
        // Assumes add and remove have same accessibility; I believe this is the case for C#.
        var method = @event.AddMethod ?? @event.RemoveMethod ?? throw new ArgumentException("Value has no add or remove method.");

        return method.GetAccessibility();
    }

    /// <summary>
    /// Returns <c>true</c> if the event is protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <returns>
    /// <c>true</c> if the event is <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsProtected(this EventInfo @event) => @event.GetAccessibility() is Accessibility.Protected or Accessibility.ProtectedInternal;

    /// <summary>
    /// Returns <c>true</c> if the event is public, i.e. its <see cref="Accessibility" /> is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <returns>
    /// <c>true</c> if the event is <see cref="Accessibility.Public" />; <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublic(this EventInfo @event) =>  @event.GetAccessibility() == Accessibility.Public;

    /// <summary>
    /// Returns <c>true</c> if the event is public or protected as viewed from an external assembly, i.e. its <see cref="Accessibility" /> is
    /// <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />; <c>false</c> otherwise.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <returns>
    /// <c>true</c> if the event is <see cref="Accessibility.Public" />, <see cref="Accessibility.Protected" /> or <see cref="Accessibility.ProtectedInternal" />;
    /// <c>false</c> otherwise.
    /// </returns>
    [Pure]
    public static bool IsPublicOrProtected(this EventInfo @event) => @event.GetAccessibility() >= Accessibility.Protected;
}