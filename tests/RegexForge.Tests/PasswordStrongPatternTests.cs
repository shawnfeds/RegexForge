using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class PasswordStrongPatternTests
{
    [Theory]
    [InlineData("Str0ng!Pass")]
    [InlineData("Abcdefg1!")]
    [InlineData("P@ssw0rd123")]
    public void IsValid_ReturnsTrue_ForStrongPasswords(string input)
    {
        PasswordStrongPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("short1!")]
    [InlineData("alllowercase1!")]
    [InlineData("ALLUPPERCASE1!")]
    [InlineData("NoDigitsHere!")]
    [InlineData("NoSpecialChar1")]
    public void IsValid_ReturnsFalse_ForWeakPasswords(string input)
    {
        PasswordStrongPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => PasswordStrongPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => PasswordStrongPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => PasswordStrongPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_WhenExceedingMaxLength()
    {
        // Pattern is capped at 128 chars as a defensive bound
        var tooLong = "Aa1!" + new string('a', 130);
        PasswordStrongPattern.IsValid(tooLong).Should().BeFalse();
    }

    [Fact]
    public void IsValid_HandlesVeryLargeInvalidStringEfficiently()
    {
        // Confirms no pathological slowdown on adversarial-length, non-matching input
        var input = new string('a', 100_000);
        PasswordStrongPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsTrue_ForUnicodeSpecialCharacterCategory()
    {
        // Non-ASCII characters satisfy the "special character" class since it's
        // defined as "not digit, not a-z, not A-Z"
        PasswordStrongPattern.IsValid("Abc123€x").Should().BeTrue();
    }

    [Fact]
    public void Match_ReturnsSuccessfulMatch_ForEmbeddedStrongPassword()
    {
        var match = PasswordStrongPattern.Match("pwd=Str0ng!Pass end");
        match.Success.Should().BeTrue();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => PasswordStrongPattern.Pattern.Should().NotBeNullOrEmpty();
}
