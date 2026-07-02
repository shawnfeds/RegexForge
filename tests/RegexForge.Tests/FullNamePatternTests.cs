using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class FullNamePatternTests
{
    [Theory]
    [InlineData("John Doe")]
    [InlineData("Mary Anne Smith")]
    [InlineData("Jean-Luc Picard")]
    [InlineData("john doe")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        FullNamePattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("John")]
    [InlineData("123 456")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        FullNamePattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => FullNamePattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => FullNamePattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => FullNamePattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        FullNamePattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => FullNamePattern.Pattern.Should().NotBeNullOrEmpty();
}
