using System.Text.RegularExpressions;

namespace RegexForge.Core;

/// <summary>
/// Defines the standard validation operations exposed by every RegexForge validator
/// (e.g. <c>EmailPattern</c>, <c>GuidPattern</c>).
/// </summary>
/// <remarks>
/// Every RegexForge validator implements this contract so that consumers get a
/// consistent API regardless of which pattern they are validating against.
/// All implementations must be safe to call concurrently.
/// </remarks>
public interface IRegexValidator
{
    /// <summary>
    /// Determines whether the specified input is a valid match for this pattern.
    /// </summary>
    /// <param name="input">The input string to validate. May be <see langword="null"/> or empty.</param>
    /// <returns><see langword="true"/> if <paramref name="input"/> matches; otherwise <see langword="false"/>.</returns>
    bool IsValid(string? input);

    /// <summary>
    /// Searches the input for the first occurrence of this pattern.
    /// </summary>
    /// <param name="input">The input string to search. May be <see langword="null"/> or empty.</param>
    /// <returns>The first <see cref="Match"/> found, which may be unsuccessful if no match exists.</returns>
    Match Match(string? input);

    /// <summary>
    /// Searches the input for all occurrences of this pattern.
    /// </summary>
    /// <param name="input">The input string to search. May be <see langword="null"/> or empty.</param>
    /// <returns>A collection of all matches found. Empty if <paramref name="input"/> is <see langword="null"/> or empty.</returns>
    MatchCollection Matches(string? input);

    /// <summary>
    /// Attempts to find the first match without throwing on invalid input.
    /// </summary>
    /// <param name="input">The input string to search.</param>
    /// <param name="match">When this method returns, contains the match if one was found.</param>
    /// <returns><see langword="true"/> if a match was found; otherwise <see langword="false"/>.</returns>
    bool TryMatch(string? input, out Match? match);

    /// <summary>
    /// Replaces all occurrences of this pattern in the input with the specified replacement.
    /// </summary>
    /// <param name="input">The input string to operate on.</param>
    /// <param name="replacement">The replacement string.</param>
    /// <returns>The resulting string after replacement.</returns>
    string Replace(string? input, string replacement);

    /// <summary>
    /// Splits the input string at each occurrence of this pattern.
    /// </summary>
    /// <param name="input">The input string to split.</param>
    /// <returns>An array of substrings.</returns>
    string[] Split(string? input);

    /// <summary>
    /// Counts the number of occurrences of this pattern in the input.
    /// </summary>
    /// <param name="input">The input string to search.</param>
    /// <returns>The number of matches found.</returns>
    int Count(string? input);
}
