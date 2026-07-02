using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class IndiaPanPatternTests
{
    [Theory]
    [InlineData("ABCDE1234F")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        IndiaPanPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("ABCDE12345")]
    [InlineData("abcde1234f")]
    [InlineData("ABCD1234FF")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        IndiaPanPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => IndiaPanPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => IndiaPanPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => IndiaPanPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        IndiaPanPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => IndiaPanPattern.Pattern.Should().NotBeNullOrEmpty();
}
