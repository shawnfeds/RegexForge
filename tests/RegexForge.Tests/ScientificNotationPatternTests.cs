using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class ScientificNotationPatternTests
{
    [Theory]
    [InlineData("1.23e+10")]
    [InlineData("5E-3")]
    [InlineData("1e10")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        ScientificNotationPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("123")]
    [InlineData("1.2.3e5")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        ScientificNotationPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => ScientificNotationPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => ScientificNotationPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => ScientificNotationPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        ScientificNotationPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => ScientificNotationPattern.Pattern.Should().NotBeNullOrEmpty();
}
