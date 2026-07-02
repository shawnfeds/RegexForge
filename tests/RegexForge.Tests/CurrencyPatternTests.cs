using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class CurrencyPatternTests
{
    [Theory]
    [InlineData("$1,234.56")]
    [InlineData("1234.56")]
    [InlineData("£1,000")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        CurrencyPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("12,34.5")]
    [InlineData("abc")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        CurrencyPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => CurrencyPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => CurrencyPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => CurrencyPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        CurrencyPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => CurrencyPattern.Pattern.Should().NotBeNullOrEmpty();
}
