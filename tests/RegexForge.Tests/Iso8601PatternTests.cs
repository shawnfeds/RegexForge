using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class Iso8601PatternTests
{
    [Theory]
    [InlineData("2024-01-15")]
    [InlineData("2024-01-15T10:30:00Z")]
    [InlineData("2024-01-15T10:30:00+05:00")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        Iso8601Pattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("01-15-2024")]
    [InlineData("2024/01/15")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        Iso8601Pattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => Iso8601Pattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => Iso8601Pattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => Iso8601Pattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        Iso8601Pattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => Iso8601Pattern.Pattern.Should().NotBeNullOrEmpty();
}
