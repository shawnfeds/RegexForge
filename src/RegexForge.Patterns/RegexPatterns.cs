#pragma warning disable CA1720 // Identifier contains type name — intentional for a pattern catalog

namespace RegexForge.Patterns;

/// <summary>
/// Provides production-tested regular expression pattern strings as constants.
/// </summary>
/// <remarks>
/// <para>
/// These are raw pattern strings only — use them directly with <see cref="System.Text.RegularExpressions.Regex"/>
/// when you need full control, or use the validators in <c>RegexForge.Validation</c>
/// (e.g. <c>EmailPattern.IsValid(...)</c>) for a ready-to-use API with caching and
/// source-generated performance built in.
/// </para>
/// <para>
/// All patterns are exposed as a single flat class by design — this keeps IntelliSense
/// lookups to a single autocomplete step (e.g. typing <c>RegexPatterns.Em</c> finds
/// <see cref="Email"/> immediately) rather than requiring you to first know which
/// category a pattern belongs to.
/// </para>
/// </remarks>
public static class RegexPatterns
{
    /// <summary>Matches a standard email address (local-part@domain), ASCII-only.</summary>
    public const string Email = @"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)+$";

    /// <summary>Matches an absolute HTTP or HTTPS URL.</summary>
    public const string Url = @"^(https?:\/\/)(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$";

    /// <summary>Matches a GUID/UUID in the standard 8-4-4-4-12 hyphenated format, with or without surrounding braces.</summary>
    public const string Guid = @"^\{?[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}\}?$";

    /// <summary>Matches a semantic version string per the SemVer 2.0.0 specification.</summary>
    public const string SemanticVersion = @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";

    /// <summary>Matches the structural shape of a JSON Web Token: three base64url segments separated by periods. Validates structure only, not signature or claims.</summary>
    public const string Jwt = @"^[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+\.[A-Za-z0-9-_]*$";

    /// <summary>Matches a strong password: 8-128 characters with at least one uppercase letter, one lowercase letter, one digit, and one special character.</summary>
    public const string PasswordStrong = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,128}$";

    /// <summary>Matches a username: 3-20 characters, alphanumeric with underscores and hyphens, must start with a letter.</summary>
    public const string Username = @"^[a-zA-Z][a-zA-Z0-9_-]{2,19}$";

    /// <summary>Matches a hexadecimal color code, with or without a leading '#', in 3, 4, 6, or 8 digit form.</summary>
    public const string HexColor = @"^#?([A-Fa-f0-9]{3,4}|[A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$";

    // ── Identity ──────────────────────────────────────────────────

    /// <summary>Matches a display name: 1-50 characters, letters, spaces, apostrophes, and hyphens.</summary>
    public const string DisplayName = @"^[A-Za-z][A-Za-z' -]{0,49}$";

    /// <summary>Matches a full name: two or more space-separated name parts, each starting with a letter.</summary>
    public const string FullName = @"^[A-Za-z][A-Za-z'-]*(\s[A-Za-z][A-Za-z'-]*)+$";

    // ── Internet ──────────────────────────────────────────────────

    /// <summary>Matches a domain name (e.g. "example.com", "sub.example.co.uk").</summary>
    public const string Domain = @"^(?!-)[A-Za-z0-9-]{1,63}(?<!-)(\.(?!-)[A-Za-z0-9-]{1,63}(?<!-))+$";

    /// <summary>Matches a hostname per RFC 1123: letters, digits, and hyphens, max 253 characters total.</summary>
    public const string Host = @"^(?=.{1,253}$)(?!-)[A-Za-z0-9-]{1,63}(?<!-)(\.(?!-)[A-Za-z0-9-]{1,63}(?<!-))*$";

    /// <summary>Matches an IPv4 address in dotted-quad notation.</summary>
    public const string Ipv4 = @"^(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}$";

    /// <summary>Matches a full or compressed IPv6 address.</summary>
    public const string Ipv6 = @"^(([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:))$";

    /// <summary>Matches a MAC address in colon- or hyphen-separated hexadecimal form.</summary>
    public const string MacAddress = @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$";

    // ── Programming ───────────────────────────────────────────────

    /// <summary>Matches a Base64-encoded string (standard alphabet, optional padding).</summary>
    public const string Base64 = @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$";

    /// <summary>Matches a CSS color value: hex, rgb(), or rgba() notation.</summary>
    public const string CssColor = @"^(#([A-Fa-f0-9]{3,4}|[A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})|rgb\(\s*\d{1,3}\s*,\s*\d{1,3}\s*,\s*\d{1,3}\s*\)|rgba\(\s*\d{1,3}\s*,\s*\d{1,3}\s*,\s*\d{1,3}\s*,\s*(0|1|0?\.\d+)\s*\))$";

    /// <summary>Matches a single HTML tag (opening, closing, or self-closing). Shallow structural match only — not a substitute for a real HTML parser.</summary>
    public const string HtmlTag = @"^<\/?[a-zA-Z][a-zA-Z0-9]*(\s+[a-zA-Z-]+(=(""[^""]*""|'[^']*'|[^\s>]+))?)*\s*\/?>$";

    // ── Numbers ───────────────────────────────────────────────────

    /// <summary>Matches a signed or unsigned integer.</summary>
    public const string Integer = @"^[+-]?\d+$";

    /// <summary>Matches a signed or unsigned decimal number.</summary>
    public const string Decimal = @"^[+-]?\d+(\.\d+)?$";

    /// <summary>Matches a currency amount with an optional leading currency symbol and thousands separators (e.g. "$1,234.56").</summary>
    public const string Currency = @"^[$€£¥]?\s?-?(\d{1,3}(,\d{3})*|\d+)(\.\d{2})?$";

    /// <summary>Matches a number in scientific notation (e.g. "1.23e+10").</summary>
    public const string ScientificNotation = @"^[+-]?\d+(\.\d+)?[eE][+-]?\d+$";

    // ── Dates ─────────────────────────────────────────────────────

    /// <summary>Matches an ISO 8601 date or date-time string.</summary>
    public const string Iso8601 = @"^\d{4}-\d{2}-\d{2}(T\d{2}:\d{2}:\d{2}(\.\d+)?(Z|[+-]\d{2}:\d{2})?)?$";

    /// <summary>Matches an RFC 3339 date-time string.</summary>
    public const string Rfc3339 = @"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(\.\d+)?(Z|[+-]\d{2}:\d{2})$";

    /// <summary>Matches a date in "yyyy-MM-dd" format.</summary>
    public const string DateYmd = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";

    /// <summary>Matches a 24-hour time in "HH:mm" or "HH:mm:ss" format.</summary>
    public const string Time = @"^([01]\d|2[0-3]):([0-5]\d)(:[0-5]\d)?$";

    /// <summary>Matches a UTC offset timezone designator (e.g. "+05:30", "-08:00", "Z").</summary>
    public const string TimeZone = @"^(Z|[+-](0\d|1[0-4]):[0-5]\d)$";

    // ── Security ──────────────────────────────────────────────────

    /// <summary>Matches the general structural shape of an API key: 20-128 alphanumeric characters, optionally including underscores or hyphens. Permissive — not tied to any specific issuer format.</summary>
    public const string ApiKey = @"^[A-Za-z0-9_-]{20,128}$";

    /// <summary>Matches a SHA-256 hash (64 hexadecimal characters).</summary>
    public const string Sha256 = @"^[A-Fa-f0-9]{64}$";

    /// <summary>Matches an MD5 hash (32 hexadecimal characters).</summary>
    public const string Md5 = @"^[A-Fa-f0-9]{32}$";

    /// <summary>Matches a UUID in the standard 8-4-4-4-12 hyphenated format (alias of <see cref="Guid"/>).</summary>
    public const string Uuid = Guid;

    // ── Payments ──────────────────────────────────────────────────

    /// <summary>Matches the structural shape of an IBAN. Validates structure only — does not verify the MOD-97 checksum or country-specific length rules.</summary>
    public const string Iban = @"^[A-Z]{2}\d{2}[A-Z0-9]{1,30}$";

    /// <summary>Matches a SWIFT/BIC code: 8 or 11 characters identifying bank, country, location, and optionally branch.</summary>
    public const string Swift = @"^[A-Z]{6}[A-Z0-9]{2}([A-Z0-9]{3})?$";

    // ── Localization ──────────────────────────────────────────────

    /// <summary>Matches a United States ZIP code (5 digits, optionally with a 4-digit extension).</summary>
    public const string UsZipCode = @"^\d{5}(-\d{4})?$";

    /// <summary>Matches a United States phone number in common formats (e.g. "(555) 123-4567", "555-123-4567").</summary>
    public const string UsPhoneNumber = @"^(\+1[\s.-]?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

    /// <summary>Matches a United Kingdom postcode.</summary>
    public const string UkPostalCode = @"^[A-Za-z]{1,2}\d[A-Za-z\d]?\s?\d[A-Za-z]{2}$";

    /// <summary>Matches an Indian PIN code (6 digits).</summary>
    public const string IndiaPostalCode = @"^[1-9]\d{5}$";

    /// <summary>Matches an Indian PAN (Permanent Account Number) tax identifier.</summary>
    public const string IndiaPan = @"^[A-Z]{5}\d{4}[A-Z]$";
}
