using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class MacAddressPatternTests
{
    [Theory]
    [InlineData("00:1A:2B:3C:4D:5E")]
    [InlineData("00-1A-2B-3C-4D-5E")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        MacAddressPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("00:1A:2B:3C:4D")]
    [InlineData("001A2B3C4D5E")]
    [InlineData("GG:1A:2B:3C:4D:5E")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        MacAddressPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => MacAddressPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => MacAddressPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => MacAddressPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        MacAddressPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => MacAddressPattern.Pattern.Should().NotBeNullOrEmpty();
}
