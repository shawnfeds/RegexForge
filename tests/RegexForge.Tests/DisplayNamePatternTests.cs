using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class DisplayNamePatternTests
{
    [Theory]
    [InlineData("John Doe")]
    [InlineData("O'Brien")]
    [InlineData("Anne-Marie")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        DisplayNamePattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("123notaname")]
    [InlineData("")]
    [InlineData("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        DisplayNamePattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => DisplayNamePattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => DisplayNamePattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => DisplayNamePattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        DisplayNamePattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => DisplayNamePattern.Pattern.Should().NotBeNullOrEmpty();
}
