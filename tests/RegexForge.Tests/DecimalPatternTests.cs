using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class DecimalPatternTests
{
    [Theory]
    [InlineData("123.45")]
    [InlineData("-0.5")]
    [InlineData("100")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        DecimalPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("1.2.3")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        DecimalPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => DecimalPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => DecimalPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => DecimalPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        DecimalPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => DecimalPattern.Pattern.Should().NotBeNullOrEmpty();
}
