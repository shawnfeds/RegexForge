using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class CssColorPatternTests
{
    [Theory]
    [InlineData("#FF5733")]
    [InlineData("rgb(255, 87, 51)")]
    [InlineData("rgba(255, 87, 51, 0.5)")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        CssColorPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("color: red")]
    [InlineData("rgb(256,1,1,1)")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        CssColorPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => CssColorPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => CssColorPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => CssColorPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        CssColorPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => CssColorPattern.Pattern.Should().NotBeNullOrEmpty();
}
