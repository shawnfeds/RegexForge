using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class UsZipCodePatternTests
{
    [Theory]
    [InlineData("90210")]
    [InlineData("12345-6789")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        UsZipCodePattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("123456")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        UsZipCodePattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => UsZipCodePattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => UsZipCodePattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => UsZipCodePattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        UsZipCodePattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => UsZipCodePattern.Pattern.Should().NotBeNullOrEmpty();
}
