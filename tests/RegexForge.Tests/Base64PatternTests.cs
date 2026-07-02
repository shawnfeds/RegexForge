using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class Base64PatternTests
{
    [Theory]
    [InlineData("SGVsbG8=")]
    [InlineData("YQ==")]
    [InlineData("YWJj")]
    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        Base64Pattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("not base64!")]
    [InlineData("abc=def")]
    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        Base64Pattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => Base64Pattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => Base64Pattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => Base64Pattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        // '!' is not a valid base64 character — confirmed invalid regardless of length
        Base64Pattern.IsValid(new string('!', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsTrue_ForVeryLargeValidString()
    {
        // 'z' repeated in multiples of 4 is structurally valid base64
        Base64Pattern.IsValid(new string('z', 100_000)).Should().BeTrue();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => Base64Pattern.Pattern.Should().NotBeNullOrEmpty();
}
