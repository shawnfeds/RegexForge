using FluentAssertions;
using RegexForge.Validation;
using Xunit;

namespace RegexForge.Tests;

public class SwiftPatternTests
{
    [Theory]
    [InlineData("DEUTDEFF")]
    [InlineData("DEUTDEFF500")]

    public void IsValid_ReturnsTrue_ForValidInputs(string input)
    {
        SwiftPattern.IsValid(input).Should().BeTrue();
    }

    [Theory]
    [InlineData("SHORT")]
    [InlineData("deutdeff")]

    public void IsValid_ReturnsFalse_ForInvalidInputs(string input)
    {
        SwiftPattern.IsValid(input).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForNull() => SwiftPattern.IsValid(null).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForEmptyString() => SwiftPattern.IsValid(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void IsValid_ReturnsFalse_ForWhitespace(string input) => SwiftPattern.IsValid(input).Should().BeFalse();

    [Fact]
    public void IsValid_ReturnsFalse_ForVeryLargeInvalidString()
    {
        SwiftPattern.IsValid(new string('z', 100_000)).Should().BeFalse();
    }

    [Fact]
    public void Pattern_IsAccessibleAndNonEmpty() => SwiftPattern.Pattern.Should().NotBeNullOrEmpty();
}
