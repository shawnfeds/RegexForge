using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class TimeZonePatternTests
{
    [Theory]
    [InlineData("Z")]
    [InlineData("+05:30")]
    [InlineData("-08:00")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        TimeZonePattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("+15:00")]
    [InlineData("08:00")]
    [InlineData("GMT")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        TimeZonePattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => TimeZonePattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => TimeZonePattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => TimeZonePattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        TimeZonePattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => TimeZonePattern.Pattern.Should().NotBeNullOrEmpty();
}
