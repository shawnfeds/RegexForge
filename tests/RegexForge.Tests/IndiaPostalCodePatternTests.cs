using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class IndiaPostalCodePatternTests
{
    [Theory]
    [InlineData("110001")]
    [InlineData("400001")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        IndiaPostalCodePattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("012345")]
    [InlineData("1234567")]
    [InlineData("abcdef")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        IndiaPostalCodePattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => IndiaPostalCodePattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => IndiaPostalCodePattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => IndiaPostalCodePattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        IndiaPostalCodePattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => IndiaPostalCodePattern.Pattern.Should().NotBeNullOrEmpty();
}
