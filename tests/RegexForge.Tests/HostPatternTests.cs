using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class HostPatternTests
{
    [Theory]
    [InlineData("example.com")]
    [InlineData("host-name.example.org")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        HostPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("-bad.com")]
    [InlineData("bad-.com")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        HostPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => HostPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => HostPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => HostPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        HostPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => HostPattern.Pattern.Should().NotBeNullOrEmpty();
}
