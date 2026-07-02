using System.Text.RegularExpressions;

namespace RegexForge.Core;

/// <summary>
/// Base class for all RegexForge validators.
/// </summary>
/// <remarks>
/// <para>
/// Maintains two <see cref="Regex"/> instances internally:
/// an anchored one (used by <see cref="IsValid"/>) and an unanchored one
/// (used by <see cref="Match"/>, <see cref="Matches"/>, <see cref="Count"/>,
/// <see cref="Replace"/>, and <see cref="Split"/>), so that search operations
/// correctly find matches inside larger strings.
/// </para>
/// </remarks>
public abstract class RegexValidatorBase : IRegexValidator
{
    private readonly Regex _validationRegex;
    private readonly Regex _searchRegex;

    /// <summary>
    /// Initializes a new instance using an anchored validation regex.
    /// An unanchored search regex is derived automatically by stripping
    /// leading <c>^</c> and trailing <c>$</c> anchors.
    /// </summary>
    protected RegexValidatorBase(Regex validationRegex, string name)
    {
        _validationRegex = validationRegex ?? throw new ArgumentNullException(nameof(validationRegex));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _searchRegex = BuildSearchRegex(validationRegex);
    }

    /// <summary>
    /// Initializes a new instance with explicit validation and search regexes.
    /// Use this overload when the unanchored pattern cannot be derived simply
    /// by stripping <c>^</c>/<c>$</c> (e.g. patterns with internal anchors).
    /// </summary>
    protected RegexValidatorBase(Regex validationRegex, Regex searchRegex, string name)
    {
        _validationRegex = validationRegex ?? throw new ArgumentNullException(nameof(validationRegex));
        _searchRegex = searchRegex ?? throw new ArgumentNullException(nameof(searchRegex));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>Gets the human-readable name of this pattern.</summary>
    public string Name { get; }

    /// <summary>Gets the raw pattern string used for validation.</summary>
    public string Pattern => _validationRegex.ToString();

    /// <inheritdoc />
    public virtual bool IsValid(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        return _validationRegex.IsMatch(input);
    }

    /// <inheritdoc />
    public virtual Match Match(string? input) =>
        _searchRegex.Match(input ?? string.Empty);

    /// <inheritdoc />
    public virtual MatchCollection Matches(string? input) =>
        _searchRegex.Matches(input ?? string.Empty);

    /// <inheritdoc />
    public virtual bool TryMatch(string? input, out Match? match)
    {
        if (string.IsNullOrEmpty(input))
        {
            match = null;
            return false;
        }

        var result = _searchRegex.Match(input);
        if (result.Success)
        {
            match = result;
            return true;
        }

        match = null;
        return false;
    }

    /// <inheritdoc />
    public virtual string Replace(string? input, string replacement)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        return _searchRegex.Replace(input, replacement ?? string.Empty);
    }

    /// <inheritdoc />
    public virtual string[] Split(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return Array.Empty<string>();
        }

        return _searchRegex.Split(input);
    }

    /// <inheritdoc />
    public virtual int Count(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return 0;
        }

        return _searchRegex.Matches(input).Count;
    }

    private static Regex BuildSearchRegex(Regex source)
    {
        var pattern = source.ToString();

        // Strip leading ^ and trailing $ to make the pattern usable for
        // substring search. Handles \A / \Z / \z as well.
        pattern = StripLeadingAnchors(pattern);
        pattern = StripTrailingAnchors(pattern);

        return new Regex(pattern, source.Options);
    }

    private static string StripLeadingAnchors(string pattern)
    {
        // Strip ^ or \A at the very start
        if (pattern.StartsWith("^", StringComparison.Ordinal))
        {
            return pattern.Substring(1);
        }

        if (pattern.StartsWith("\\A", StringComparison.Ordinal))
        {
            return pattern.Substring(2);
        }

        return pattern;
    }

    private static string StripTrailingAnchors(string pattern)
    {
        // Strip $ or \Z or \z at the very end
        if (pattern.EndsWith("$", StringComparison.Ordinal))
        {
            return pattern.Substring(0, pattern.Length - 1);
        }

        if (pattern.EndsWith("\\Z", StringComparison.Ordinal) ||
            pattern.EndsWith("\\z", StringComparison.Ordinal))
        {
            return pattern.Substring(0, pattern.Length - 2);
        }

        return pattern;
    }
}
