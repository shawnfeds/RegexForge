# Pattern Catalog

34 patterns across 8 categories. Each links to full documentation including known limitations.


## [Identity](identity.md)

- **Email** — Standard email address (local-part@domain), ASCII-only per RFC 5322 local-part rules.
- **Username** — 3-20 characters, alphanumeric with underscores/hyphens, must start with a letter.
- **PasswordStrong** — 8-128 characters with at least one uppercase, lowercase, digit, and special character.
- **DisplayName** — 1-50 characters: letters, spaces, apostrophes, hyphens.
- **FullName** — Two or more space-separated name parts, each starting with a letter.

## [Internet](internet.md)

- **Url** — Absolute HTTP/HTTPS URL.
- **Domain** — Domain name (e.g. example.com).
- **Host** — RFC 1123 hostname, max 253 characters.
- **Ipv4** — IPv4 address in dotted-quad notation.
- **Ipv6** — IPv6 address, full or compressed form.
- **MacAddress** — MAC address, colon- or hyphen-separated.

## [Programming](programming.md)

- **Guid** — GUID/UUID, 8-4-4-4-12 hex format, with or without braces.
- **Uuid** — Alias of GuidPattern — identical pattern, provided for discoverability.
- **SemanticVersion** — SemVer 2.0.0 compliant version string.
- **HexColor** — Hex color code, 3/4/6/8 digits, with or without #.
- **Base64** — Base64-encoded string, standard alphabet.
- **CssColor** — CSS color: hex, rgb(), or rgba().
- **HtmlTag** — A single HTML tag (opening, closing, or self-closing).

## [Numbers](numbers.md)

- **Integer** — Signed or unsigned integer.
- **Decimal** — Signed or unsigned decimal number.
- **Currency** — Currency amount, optional symbol and thousands separators.
- **ScientificNotation** — Number in scientific notation (e.g. 1.23e+10).

## [Dates](dates.md)

- **Iso8601** — ISO 8601 date or date-time string.
- **Rfc3339** — RFC 3339 date-time string.
- **DateYmd** — Date in yyyy-MM-dd format.
- **Time** — 24-hour time, HH:mm or HH:mm:ss.
- **TimeZone** — UTC offset timezone designator (e.g. +05:30, Z).

## [Security](security.md)

- **Jwt** — Structural shape of a JWT (three base64url segments).
- **ApiKey** — General structural shape of an API key, 20-128 characters.
- **Sha256** — SHA-256 hash, 64 hex characters.
- **Md5** — MD5 hash, 32 hex characters.

## [Payments](payments.md)

- **Iban** — Structural shape of an IBAN.
- **Swift** — SWIFT/BIC bank identifier code, 8 or 11 characters.

## [Localization](localization.md)

- **UsZipCode** — US ZIP code, 5 digits with optional 4-digit extension.
- **UsPhoneNumber** — US phone number in common formats.
- **UkPostalCode** — UK postcode.
- **IndiaPostalCode** — Indian PIN code, 6 digits.
- **IndiaPan** — Indian PAN tax identifier.
