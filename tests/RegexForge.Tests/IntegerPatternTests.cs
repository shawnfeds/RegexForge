using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class IntegerPatternTests
{
    [Theory]
    [InlineData("123")]
    [InlineData("-456")]
    [InlineData("+789")]
    [InlineData("0")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        IntegerPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("12.3")]
    [InlineData("abc")]
    [InlineData("1,000")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        IntegerPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => IntegerPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => IntegerPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => IntegerPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        IntegerPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => IntegerPattern.Pattern.Should().NotBeNullOrEmpty();
}
