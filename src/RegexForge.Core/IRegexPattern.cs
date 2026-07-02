namespace RegexForge.Core;

/// <summary>
/// Represents a named regular expression pattern exposed as a raw string.
/// </summary>
/// <remarks>
/// Implementations are expected to be immutable and thread-safe.
/// </remarks>
public interface IRegexPattern
{
    /// <summary>
    /// Gets the raw regular expression pattern string.
    /// </summary>
    string Pattern { get; }

    /// <summary>
    /// Gets the human-readable name of this pattern (e.g. "Email", "Guid").
    /// </summary>
    string Name { get; }
}
