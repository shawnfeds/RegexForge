using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class Rfc3339PatternTests
{
    [Theory]
    [InlineData("2024-01-15T10:30:00Z")]
    [InlineData("2024-01-15T10:30:00.123+05:00")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        Rfc3339Pattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("2024-01-15")]
    [InlineData("not-a-date")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        Rfc3339Pattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => Rfc3339Pattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => Rfc3339Pattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => Rfc3339Pattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        Rfc3339Pattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => Rfc3339Pattern.Pattern.Should().NotBeNullOrEmpty();
}
