namespace MrKWatkins.Reflection.Tests;

public class DisplayNameExtensionsTests
{
    [TestCase(typeof(string), "String")]
    [TestCase(typeof(int), "Int32")]
    [TestCase(typeof(List<string>), "List<String>")]
    [TestCase(typeof(Dictionary<string, int>), "Dictionary<String, Int32>")]
    [TestCase(typeof(ITestContravariant<>), "ITestContravariant<in T>")]
    [TestCase(typeof(ITestCovariant<>), "ITestCovariant<out T>")]
    public void DisplayName(Type type, string expected) => type.DisplayName().Should().Be(expected);

    // ReSharper disable once UnusedTypeParameter
    private interface ITestContravariant<in T>;

    // ReSharper disable once UnusedTypeParameter
    private interface ITestCovariant<out T>;
}