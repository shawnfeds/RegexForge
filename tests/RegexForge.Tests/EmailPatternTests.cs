using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class EmailPatternTests
{
    [Theory]
    [InlineData("john@example.com")]
    [InlineData("jane.doe@example.co.uk")]
    [InlineData("user+tag@example.com")]
    [InlineData("user_name@sub.example.com")]
    [InlineData("a@b.co")]
    [InlineData("first.last@example-domain.com")]
    public void IsValid_ReturnsTrue_ForValidEmails(string input)
    {
        EmailPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("notanemail")]
    [InlineData("missing@domain")]
    [InlineData("@missinglocal.com")]
    [InlineData("missingat.com")]
    [InlineData("two@@at.com")]
    [InlineData("trailing.dot.@example.com")]
    [InlineData("spaces in@example.com")]
    [InlineData("user@.com")]
    [InlineData("user@domain..com")]
    [InlineData(".leading@example.com")]
    [InlineData("double..dot@example.com")]
    public void IsValid_ReturnsFalse_ForInvalidEmails(string input)
    {
        EmailPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull()
    {
        EmailPattern.IsValid(null).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString()
    {
        EmailPattern.IsValid(string.Empty).Should().BeFalse();
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void IsValid_ReturnsFalse_ForWhitespaceOnly(string input)
    {
        EmailPattern.IsValid(input).Should().BeFalse();
    }

    [Theory]
    [InlineData("用户@example.com")]
    [InlineData("jöhn@example.com")]
    [InlineData("user@例え.jp")]
    public void IsValid_ReturnsFalse_ForUnicodeLocalOrDomainParts(string input)
    {
        // RFC 5322-style ASCII-only pattern by design — internationalized
        // email (RFC 6531/IDN) is explicitly out of scope for this validator.
        EmailPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        var input = new string('a', 100_000) + "@@notvalid";

        EmailPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_HandlesVeryLargeValidStringEfficiently()
    {
        var longLocalPart = new string('a', 60);
        var input = $"{longLocalPart}@example.com";

        EmailPattern.IsValid(input).Should().BeTrue();
    }

    [Fact]
    public void Match_ReturnsSuccessfulMatch_ForValidEmail()
    {
        var match = EmailPattern.Match("contact: john@example.com please");

        match.Success.Should().BeTrue();
        match.Value.Should().Be("john@example.com");
    }

    [Fact]
    public void Match_ReturnsUnsuccessfulMatch_ForNoEmailPresent()
    {
        var match = EmailPattern.Match("no email here");

        match.Success.Should().BeFalse();
    }

    [Fact]
    public void Matches_ReturnsAllOccurrences()
    {
        var matches = EmailPattern.Matches("a@example.com and b@example.com");

        matches.Count.Should().Be(2);
    }

    [Fact]
    public void Matches_ReturnsEmptyCollection_ForNullInput()
    {
        var matches = EmailPattern.Matches(null);

        matches.Count.Should().Be(0);
    }

    [Fact]
    public void TryMatch_ReturnsTrueAndMatch_WhenFound()
    {
        var found = EmailPattern.TryMatch("john@example.com", out var match);

        found.Should().BeTrue();
        match.Should().NotBeNull();
        match!.Value.Should().Be("john@example.com");
    }

    [Fact]
    public void TryMatch_ReturnsFalse_WhenNotFound()
    {
        var found = EmailPattern.TryMatch("not an email", out var match);

        found.Should().BeFalse();
        match.Should().BeNull();
    }

    [Fact]
    public void Replace_SubstitutesMatchedEmail()
    {
        var result = EmailPattern.Replace("contact john@example.com now", "[redacted]");

        result.Should().Be("contact [redacted] now");
    }

    [Fact]
    public void Replace_ReturnsEmptyString_ForNullInput()
    {
        EmailPattern.Replace(null, "x").Should().BeEmpty();
    }

    [Fact]
    public void Split_SplitsOnEmailOccurrences()
    {
        var result = EmailPattern.Split("before john@example.com after");

        result.Should().HaveCount(2);
        result[0].Should().Be("before ");
        result[1].Should().Be(" after");
    }

    [Fact]
    public void Split_ReturnsEmptyArray_ForNullInput()
    {
        EmailPattern.Split(null).Should().BeEmpty();
    }

    [Fact]
    public void Count_ReturnsNumberOfMatches()
    {
        EmailPattern.Count("a@example.com b@example.com c@example.com").Should().Be(3);
    }

    [Fact]
    public void Count_ReturnsZero_ForNullInput()
    {
        EmailPattern.Count(null).Should().Be(0);
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty()
    {
        EmailPattern.Pattern.Should().NotBeNullOrEmpty();
    }
}
