using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class TimePatternTests
{
    [Theory]
    [InlineData("14:30")]
    [InlineData("23:59:59")]
    [InlineData("00:00")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        TimePattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("24:00")]
    [InlineData("12:60")]
    [InlineData("1:30")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        TimePattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => TimePattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => TimePattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => TimePattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        TimePattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => TimePattern.Pattern.Should().NotBeNullOrEmpty();
}
