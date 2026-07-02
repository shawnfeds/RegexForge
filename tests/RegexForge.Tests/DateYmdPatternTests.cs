using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class DateYmdPatternTests
{
    [Theory]
    [InlineData("2024-01-15")]
    [InlineData("2024-12-31")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        DateYmdPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("2024-13-01")]
    [InlineData("2024-01-32")]
    [InlineData("01-15-2024")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        DateYmdPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => DateYmdPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => DateYmdPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => DateYmdPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        DateYmdPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => DateYmdPattern.Pattern.Should().NotBeNullOrEmpty();
}
