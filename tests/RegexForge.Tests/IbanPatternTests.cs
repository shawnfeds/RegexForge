using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class IbanPatternTests
{
    [Theory]
    [InlineData("GB29NWBK60161331926819")]
    [InlineData("DE89370400440532013000")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        IbanPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("1234567890")]
    [InlineData("GBnotanumber")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        IbanPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => IbanPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => IbanPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => IbanPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        IbanPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => IbanPattern.Pattern.Should().NotBeNullOrEmpty();
}
