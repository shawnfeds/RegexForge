using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class DomainPatternTests
{
    [Theory]
    [InlineData("example.com")]
    [InlineData("sub.example.co.uk")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        DomainPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("-example.com")]
    [InlineData("example-.com")]
    [InlineData("example")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        DomainPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => DomainPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => DomainPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => DomainPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        DomainPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => DomainPattern.Pattern.Should().NotBeNullOrEmpty();
}
