using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class HexColorPatternTests
{
    [Theory]
    [InlineData("#FF5733")]
    [InlineData("FF5733")]
    [InlineData("#FFF")]
    [InlineData("FFF")]
    [InlineData("#FFFFFFFF")]
    [InlineData("#abc123")]
    [InlineData("#ABCD")]
    public void IsValid_ReturnsTrue_ForValidHexColors(string input)
    {
        HexColorPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("not-a-color")]
    [InlineData("#GGG")]
    [InlineData("#12345")]
    [InlineData("#1234567")]
    [InlineData("##FFF")]
    public void IsValid_ReturnsFalse_ForInvalidHexColors(string input)
    {
        HexColorPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => HexColorPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => HexColorPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => HexColorPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        HexColorPattern.IsValid(new string('a', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Match_FindsColorWithinText()
    {
        var match = HexColorPattern.Match("color: #FF5733 end");
        match.Success.Should().BeTrue();
    }

    [Fact]
    public void Count_ReturnsNumberOfMatches()
    {
        HexColorPattern.Count("#FFF #000 #ABCDEF").Should().Be(3);
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => HexColorPattern.Pattern.Should().NotBeNullOrEmpty();
}
