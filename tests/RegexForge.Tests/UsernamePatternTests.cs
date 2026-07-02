using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class UsernamePatternTests
{
    [Theory]
    [InlineData("john_doe-99")]
    [InlineData("abc")]
    [InlineData("Alice123")]
    [InlineData("a-b_c-d")]
    public void IsValid_ReturnsTrue_ForValidUsernames(string input)
    {
        UsernamePattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("ab")] // too short
    [InlineData("123abc")] // starts with digit
    [InlineData("_abc")] // starts with underscore
    [InlineData("this_username_is_way_too_long_for_the_pattern")] // too long
    [InlineData("has space")]
    [InlineData("has.dot")]
    public void IsValid_ReturnsFalse_ForInvalidUsernames(string input)
    {
        UsernamePattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => UsernamePattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => UsernamePattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => UsernamePattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForUnicodeStart()
    {
        UsernamePattern.IsValid("用户名123").Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        UsernamePattern.IsValid(new string('a', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Match_FindsUsernameWithinText()
    {
        var match = UsernamePattern.Match("user: john_doe-99 active");
        match.Success.Should().BeTrue();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => UsernamePattern.Pattern.Should().NotBeNullOrEmpty();
}
