using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class SemanticVersionPatternTests
{
    [Theory]
    [InlineData("1.2.3")]
    [InlineData("0.0.1")]
    [InlineData("1.2.3-alpha")]
    [InlineData("1.2.3-alpha.1")]
    [InlineData("1.2.3+build.5")]
    [InlineData("1.2.3-rc.1+build.5")]
    [InlineData("10.20.30")]
    public void IsValid_ReturnsTrue_ForValidSemVer(string input)
    {
        SemanticVersionPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("1.2")]
    [InlineData("1.2.3.4")]
    [InlineData("v1.2.3")]
    [InlineData("01.2.3")]
    [InlineData("1.2.3-")]
    [InlineData("not.a.version")]
    public void IsValid_ReturnsFalse_ForInvalidSemVer(string input)
    {
        SemanticVersionPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => SemanticVersionPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => SemanticVersionPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => SemanticVersionPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        SemanticVersionPattern.IsValid(new string('1', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Match_FindsVersionWithinText()
    {
        var match = SemanticVersionPattern.Match("version 1.2.3 released");
        match.Success.Should().BeTrue();
        match.Value.Should().Be("1.2.3");
    }

    [Fact]
    public void TryMatch_ReturnsFalse_WhenNotFound()
    {
        SemanticVersionPattern.TryMatch("nope", out var match).Should().BeFalse();
        match.Should().BeNull();
    }

    [Fact]
    public void Count_ReturnsNumberOfMatches()
    {
        SemanticVersionPattern.Count("1.2.3 and 4.5.6").Should().Be(2);
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => SemanticVersionPattern.Pattern.Should().NotBeNullOrEmpty();
}
