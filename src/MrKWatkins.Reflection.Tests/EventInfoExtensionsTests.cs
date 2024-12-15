using System.Reflection;
using MrKWatkins.Reflection.Tests.TestTypes.Events;

namespace MrKWatkins.Reflection.Tests;

public sealed class EventInfoExtensionsTests : TestFixture
{
    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void GetAccessibility(EventInfo @event, Accessibility expected) => @event.GetAccessibility().Should().Be(expected);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void GetAccessibility_NoAddMethod(EventInfo @event, Accessibility expected)
    {
        var testEvent = new TestEventInfo(@event, false, true);
        testEvent.GetAccessibility().Should().Be(expected);
    }

    [Test]
    public void GetAccessibility_NoAddOrRemoveMethodThrows()
    {
        var testEvent = new TestEventInfo(GetEvent<EventAccessibility>(nameof(EventAccessibility.Public)), false, false);
        testEvent.Invoking(e => e.GetAccessibility()).Should().Throw<ArgumentException>();
    }

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsProtected(EventInfo @event, Accessibility visibility) =>
        @event.IsProtected().Should().Be(visibility is Accessibility.Protected or Accessibility.ProtectedInternal);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublic(EventInfo @event, Accessibility visibility) =>
        @event.IsPublic().Should().Be(visibility == Accessibility.Public);

    [TestCaseSource(nameof(AccessibilityTestCases))]
    public void IsPublicOrProtected(EventInfo @event, Accessibility visibility) =>
        @event.IsPublicOrProtected().Should().Be(visibility is Accessibility.Public or Accessibility.Protected or Accessibility.ProtectedInternal);

    [Pure]
    public static IEnumerable<TestCaseData> AccessibilityTestCases() =>
        typeof(EventAccessibility)
            .GetEvents(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Select(@event => new TestCaseData(@event, Enum.Parse<Accessibility>(@event.Name)).SetArgDisplayNames(@event.Name));
}