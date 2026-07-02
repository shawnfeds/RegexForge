using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class Ipv4PatternTests
{
    [Theory]
    [InlineData("192.168.1.1")]
    [InlineData("0.0.0.0")]
    [InlineData("255.255.255.255")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        Ipv4Pattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("256.1.1.1")]
    [InlineData("1.1.1")]
    [InlineData("1.1.1.1.1")]
    [InlineData("abc.def.ghi.jkl")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        Ipv4Pattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => Ipv4Pattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => Ipv4Pattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => Ipv4Pattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        Ipv4Pattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => Ipv4Pattern.Pattern.Should().NotBeNullOrEmpty();
}
