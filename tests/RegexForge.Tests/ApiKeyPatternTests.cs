using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class ApiKeyPatternTests
{
    [Theory]
    [InlineData("abcdefghij1234567890")]
    [InlineData("sk_test_abc123def456ghi789")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        ApiKeyPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("short")]
    [InlineData("has spaces here please")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        ApiKeyPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => ApiKeyPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => ApiKeyPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => ApiKeyPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        ApiKeyPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => ApiKeyPattern.Pattern.Should().NotBeNullOrEmpty();
}
