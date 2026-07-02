using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class UsPhoneNumberPatternTests
{
    [Theory]
    [InlineData("(555) 123-4567")]
    [InlineData("555-123-4567")]
    [InlineData("+1 555 123 4567")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        UsPhoneNumberPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("555-123")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        UsPhoneNumberPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => UsPhoneNumberPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => UsPhoneNumberPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => UsPhoneNumberPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        UsPhoneNumberPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => UsPhoneNumberPattern.Pattern.Should().NotBeNullOrEmpty();
}
