using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class JwtPatternTests
{
    [Theory]
    [InlineData("eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIn0.dozjgNryP4J3jVmNHl0w5N_XgL0n3I9PlFUP0THsR8U")]
    [InlineData("a.b.c")]
    [InlineData("a.b.")]
    public void IsValid_ReturnsTrue_ForValidJwtShapes(string input)
    {
        JwtPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("not.a.jwt.shape.extra")]
    [InlineData("missingsegments")]
    [InlineData("a.b")]
    [InlineData(".b.c")]
    public void IsValid_ReturnsFalse_ForInvalidJwtShapes(string input)
    {
        JwtPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => JwtPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => JwtPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => JwtPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        JwtPattern.IsValid(new string('!', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Match_FindsJwtWithinText()
    {
        var match = JwtPattern.Match("token: a.b.c end");
        match.Success.Should().BeTrue();
    }

    [Fact]
    public void TryMatch_ReturnsFalse_WhenNotFound()
    {
        JwtPattern.TryMatch("???", out var match).Should().BeFalse();
        match.Should().BeNull();
    }

    [Fact]
    public void Replace_SubstitutesJwt()
    {
        JwtPattern.Replace("token a.b.c here", "[token]").Should().Be("token [token] here");
    }

    [Fact]
    public void Count_ReturnsNumberOfMatches()
    {
        JwtPattern.Count("a.b.c d.e.f").Should().Be(2);
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => JwtPattern.Pattern.Should().NotBeNullOrEmpty();
}
