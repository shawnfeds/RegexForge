using System.Text.RegularExpressions;
using RegexForge.Core;
using RegexForge.Patterns;

namespace RegexForge.Validation;

/// <summary>
/// Provides validation, matching, and string operations for email addresses.
/// </summary>
/// <remarks>
/// <para>
/// On .NET 7 and later, this class uses a source-generated regex via <c>[GeneratedRegex]</c>
/// for zero-allocation, ahead-of-time compiled matching. On <c>netstandard2.0</c> targets
/// (including .NET Framework), it falls back to a single cached instance of
/// <see cref="Regex"/> constructed with <see cref="RegexOptions.Compiled"/>.
/// </para>
/// <para>
/// The public API is identical across all target frameworks — callers never need to know
/// which code path is active.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// bool isValid = EmailPattern.IsValid("john@example.com");
/// var match = EmailPattern.Match(text);
/// </code>
/// </example>
public static partial class EmailPattern
{
#if NET7_0_OR_GREATER
    [GeneratedRegex(RegexPatterns.Email, RegexOptions.CultureInvariant)]
    private static partial Regex CreateRegex();

    private static readonly Regex _regex = CreateRegex();
#else
    private static readonly Regex _regex = new Regex(
        RegexPatterns.Email,
        RegexOptions.Compiled | RegexOptions.CultureInvariant);
#endif

    private static readonly Validator _validator = new Validator(_regex);

    /// <summary>
    /// Gets the raw pattern string used by this validator.
    /// </summary>
    public static string Pattern => RegexPatterns.Email;

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

    /// <summary>
    /// Internal instance wrapper so the static class can delegate to <see cref="RegexValidatorBase"/>
    /// without itself needing to be an instance type.
    /// </summary>
    private sealed class Validator : RegexValidatorBase
    {
        public Validator(Regex regex) : base(regex, nameof(EmailPattern))
        {
        }
    }
}
