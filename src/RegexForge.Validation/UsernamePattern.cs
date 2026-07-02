using System.Text.RegularExpressions;
using RegexForge.Core;
using RegexForge.Patterns;

namespace RegexForge.Validation;

/// <summary>
/// Provides validation, matching, and string operations for usernames:
/// 3-20 characters, alphanumeric with underscores and hyphens, must start with a letter.
/// </summary>
/// <example>
/// <code>
/// bool isValid = UsernamePattern.IsValid("john_doe-99");
/// </code>
/// </example>
public static partial class UsernamePattern
{
#if NET7_0_OR_GREATER
    [GeneratedRegex(RegexPatterns.Username, RegexOptions.CultureInvariant)]
    private static partial Regex CreateRegex();

    private static readonly Regex _regex = CreateRegex();
#else
    private static readonly Regex _regex = new Regex(
        RegexPatterns.Username,
        RegexOptions.Compiled | RegexOptions.CultureInvariant);
#endif

    private static readonly Validator _validator = new Validator(_regex);

    /// <summary>
    /// Gets the raw pattern string used by this validator.
    /// </summary>
    public static string Pattern => RegexPatterns.Username;

    /// <inheritdoc cref="IRegexValidator.IsValid"/>
    public static bool IsValid(string? input) => _validator.IsValid(input);

    /// <inheritdoc cref="IRegexValidator.Match"/>
    public static Match Match(string? input) => _validator.Match(input);

    /// <inheritdoc cref="IRegexValidator.Matches"/>
    public static MatchCollection Matches(string? input) => _validator.Matches(input);

    /// <inheritdoc cref="IRegexValidator.TryMatch"/>
    public static bool TryMatch(string? input, out Match? match) => _validator.TryMatch(input, out match);

    /// <inheritdoc cref="IRegexValidator.Replace"/>
    public static string Replace(string? input, string replacement) => _validator.Replace(input, replacement);

    /// <inheritdoc cref="IRegexValidator.Split"/>
    public static string[] Split(string? input) => _validator.Split(input);

    /// <inheritdoc cref="IRegexValidator.Count"/>
    public static int Count(string? input) => _validator.Count(input);

    private sealed class Validator : RegexValidatorBase
    {
        public Validator(Regex regex) : base(regex, nameof(UsernamePattern))
        {
        }
    }
}
