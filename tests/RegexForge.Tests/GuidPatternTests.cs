using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class GuidPatternTests
{
    [Theory]
    [InlineData("550e8400-e29b-41d4-a716-446655440000")]
    [InlineData("{550E8400-E29B-41D4-A716-446655440000}")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff")]
    public void IsValid_ReturnsTrue_ForValidGuids(string input)
    {
        GuidPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("not-a-guid")]
    [InlineData("550e8400-e29b-41d4-a716")]
    [InlineData("550e8400e29b41d4a716446655440000")]
    [InlineData("550e8400-e29b-41d4-a716-44665544000g")]
    [InlineData("550e8400-e29b-41d4-a716-446655440000-extra")]
    public void IsValid_ReturnsFalse_ForInvalidGuids(string input)
    {
        GuidPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => GuidPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => GuidPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => GuidPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForUnicodeNoise()
    {
        GuidPattern.IsValid("550e8400-e29b-41d4-a716-44665544０000").Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        GuidPattern.IsValid(new string('a', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Match_FindsGuidWithinText()
    {
        var match = GuidPattern.Match("id: 550e8400-e29b-41d4-a716-446655440000 end");
        match.Success.Should().BeTrue();
        match.Value.Should().Be("550e8400-e29b-41d4-a716-446655440000");
    }

    [Fact]
    public void Matches_ReturnsAllOccurrences()
    {
        var matches = GuidPattern.Matches("550e8400-e29b-41d4-a716-446655440000 00000000-0000-0000-0000-000000000000");
        matches.Count.Should().Be(2);
    }

    [Fact]
    public void TryMatch_ReturnsFalse_WhenNotFound()
    {
        GuidPattern.TryMatch("nope", out var match).Should().BeFalse();
        match.Should().BeNull();
    }

    [Fact]
    public void Replace_SubstitutesGuid()
    {
        GuidPattern.Replace("id=550e8400-e29b-41d4-a716-446655440000", "[id]").Should().Be("id=[id]");
    }

    [Fact]
    public void Split_SplitsOnGuidOccurrences()
    {
        var result = GuidPattern.Split("a-550e8400-e29b-41d4-a716-446655440000-b");
        result.Should().HaveCount(2);
    }

    [Fact]
    public void Count_ReturnsNumberOfMatches()
    {
        GuidPattern.Count("550e8400-e29b-41d4-a716-446655440000 00000000-0000-0000-0000-000000000000").Should().Be(2);
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => GuidPattern.Pattern.Should().NotBeNullOrEmpty();
}
