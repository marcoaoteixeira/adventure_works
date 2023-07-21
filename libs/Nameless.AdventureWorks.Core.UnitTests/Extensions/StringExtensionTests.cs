namespace Nameless.AdventureWorks;

public class StringExtensionTests {
    [TestCase("true", true)]
    [TestCase("True", true)]
    [TestCase("TRUE", true)]
    [TestCase("false", false)]
    [TestCase("False", false)]
    [TestCase("FALSE", false)]
    [TestCase("", false)]
    [TestCase("0", false)]
    [TestCase("1", true)]
    [TestCase("100", true)]
    [TestCase("-1", false)]
    [TestCase(null, false)]
    public void IsTrueString_Returns_Valid_Output_For_Inputs(string input, bool expected) {
        // arrange

        // act
        var actual = StringExtension.IsTrueString(input);

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}