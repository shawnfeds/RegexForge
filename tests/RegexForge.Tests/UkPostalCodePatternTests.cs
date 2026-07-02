using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class UkPostalCodePatternTests
{
    [Theory]
    [InlineData("SW1A 1AA")]
    [InlineData("EC1A1BB")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        UkPostalCodePattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("AAAA AAAA")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        UkPostalCodePattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => UkPostalCodePattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => UkPostalCodePattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => UkPostalCodePattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        UkPostalCodePattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => UkPostalCodePattern.Pattern.Should().NotBeNullOrEmpty();
}
