using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class UrlPatternTests
{
    [Theory]
    [InlineData("https://example.com")]
    [InlineData("http://example.com")]
    [InlineData("https://www.example.com/path")]
    [InlineData("https://example.com/path?q=1&x=2")]
    [InlineData("https://sub.example.co.uk")]
    [InlineData("https://example.com:8080/path")]
    public void IsValid_ReturnsTrue_ForValidUrls(string input)
    {
        UrlPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("not a url")]
    [InlineData("ftp://example.com")]
    [InlineData("example.com")]
    [InlineData("http://")]
    [InlineData("https://.com")]
    public void IsValid_ReturnsFalse_ForInvalidUrls(string input)
    {
        UrlPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => UrlPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => UrlPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => UrlPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        UrlPattern.IsValid(new string('a', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Match_FindsUrlWithinText()
    {
        var match = UrlPattern.Match("visit https://example.com today");
        match.Success.Should().BeTrue();
    }

    [Fact]
    public void Matches_ReturnsAllOccurrences()
    {
        var matches = UrlPattern.Matches("https://a.com and https://b.com");
        matches.Count.Should().Be(2);
    }

    [Fact]
    public void TryMatch_ReturnsFalse_WhenNotFound()
    {
        UrlPattern.TryMatch("nope", out var match).Should().BeFalse();
        match.Should().BeNull();
    }

    [Fact]
    public void Replace_SubstitutesUrl()
    {
        UrlPattern.Replace("see https://example.com now", "[link]").Should().Be("see [link] now");
    }

    [Fact]
    public void Count_ReturnsNumberOfMatches()
    {
        UrlPattern.Count("https://a.com https://b.com https://c.com").Should().Be(3);
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => UrlPattern.Pattern.Should().NotBeNullOrEmpty();
}
