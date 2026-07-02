# RegexForge

[![Build](https://img.shields.io/github/actions/workflow/status/shawnfeds/RegexForge/build.yml?branch=main&label=build)](https://github.com/shawnfeds/RegexForge/actions/workflows/build.yml)
[![Test](https://img.shields.io/github/actions/workflow/status/shawnfeds/RegexForge/test.yml?branch=main&label=tests)](https://github.com/shawnfeds/RegexForge/actions/workflows/test.yml)
[![CodeQL](https://img.shields.io/github/actions/workflow/status/shawnfeds/RegexForge/codeql.yml?branch=main&label=codeql)](https://github.com/shawnfeds/RegexForge/actions/workflows/codeql.yml)
[![NuGet](https://img.shields.io/nuget/v/RegexForge.Validation.svg)](https://www.nuget.org/packages/RegexForge.Validation)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A production-ready .NET library providing battle-tested regular expressions,
source-generated validators, and a fluent, consistent API — so you stop
re-deriving the email regex from StackOverflow every time you start a project.

## Introduction

RegexForge gives you three things, layered:

- **`RegexForge.Patterns`** — raw pattern string constants, if you just want the regex.
- **`RegexForge.Validation`** — strongly-typed validators (`EmailPattern.IsValid(...)`)
  with caching and source-generated performance built in, no setup required.
- **`RegexForge.Core`** — the abstractions underneath, useful if you're building your
  own pattern library on the same foundation.

Every pattern ships with documented limitations. We'd rather tell you upfront that
`IbanPattern` doesn't validate the checksum than have you find out in production.

## Installation

```bash
dotnet add package RegexForge.Validation
```

`RegexForge.Validation` pulls in `RegexForge.Core` and `RegexForge.Patterns`
automatically. If you only want the raw pattern strings without the validator API:

```bash
dotnet add package RegexForge.Patterns
```

## Quick Start

```csharp
using RegexForge.Validation;

bool isValid = EmailPattern.IsValid("john@example.com");      // true
bool isGuid  = GuidPattern.IsValid("not-a-guid");              // false

var match = EmailPattern.Match("Contact: jane@example.com");
Console.WriteLine(match.Value);                                // jane@example.com

string redacted = EmailPattern.Replace("Email me at me@x.com", "[hidden]");
// "Email me at [hidden]"
```

## Supported Frameworks

| Target | Notes |
|---|---|
| `netstandard2.0` | Covers .NET Framework 4.6.1+ and all modern .NET. Uses a cached, compiled `Regex` instance. |
| `net8.0` (LTS) | Uses `[GeneratedRegex]` source generation for ahead-of-time compiled matching. |
| `net10.0` (LTS) | Same as net8.0, plus any net10-specific runtime improvements as they land. |

The public API is identical across all three — your code doesn't change based on target.

## Examples

```csharp
// Validation
EmailPattern.IsValid(email);
GuidPattern.IsValid(value);
Ipv4Pattern.IsValid(ip);

// Matching
var match = EmailPattern.Match(value);
var all   = EmailPattern.Matches(value);
EmailPattern.TryMatch(value, out var found);

// String operations
string cleaned = EmailPattern.Replace(text, "[redacted]");
string[] parts = EmailPattern.Split(text);
int count       = EmailPattern.Count(text);

// Raw pattern access, if you need it
string pattern = RegexPatterns.Email;
```

See [`samples/ConsoleSample`](samples/ConsoleSample) and
[`samples/MinimalApiSample`](samples/MinimalApiSample) for complete, runnable examples
including request-validation use in an ASP.NET Core endpoint.

## Pattern Catalog (v1.0 — 34 patterns)

| Category | Patterns |
|---|---|
| Identity | Email, Username, PasswordStrong, DisplayName, FullName |
| Internet | Url, Domain, Host, Ipv4, Ipv6, MacAddress |
| Programming | Guid, SemanticVersion, HexColor, Base64, CssColor, HtmlTag |
| Numbers | Integer, Decimal, Currency, ScientificNotation |
| Dates | Iso8601, Rfc3339, DateYmd, Time, TimeZone |
| Security | Jwt, ApiKey, Sha256, Md5, Uuid |
| Payments | Iban, Swift |
| Localization | UsZipCode, UsPhoneNumber, UkPostalCode, IndiaPostalCode, IndiaPan |

Full XML documentation, including known limitations per pattern, is available via
IntelliSense or the [documentation site](https://shawnfeds.github.io/RegexForge).

## Performance

RegexForge validators use a single cached instance per pattern, constructed once at
first use:

- On `net8.0`/`net10.0`: a `[GeneratedRegex]`-produced `Regex`, compiled ahead of time.
- On `netstandard2.0`: a `Regex` built once with `RegexOptions.Compiled`.

Either way, you never pay regex construction cost more than once per process, and you
never accidentally write `new Regex(pattern)` inside a hot path.

## Benchmarks

Run locally:

```bash
dotnet run -c Release --project tests/RegexForge.Benchmarks/RegexForge.Benchmarks.csproj --framework net10.0
```

### Warm-start (steady-state) — `.NET 10.0.9`, `X64 RyuJIT AVX2`

> Baseline: `RawRegexIsMatch_Valid` (156 ns). Lower is better. All methods: **0 allocations**.

| Method | Mean | vs Raw Regex | Notes |
|---|---|---|---|
| `RawRegexIsMatch_Valid` | 156.16 ns | 1.00× (baseline) | Recompiles on every call |
| `RawRegexIsMatch_Invalid` | 549.30 ns | 3.52× slower | Worst case — no match, full scan |
| `ManuallyCompiledRegex_Valid` | 64.18 ns | **2.43× faster** | Cached compiled `Regex` by hand |
| `ManuallyCompiledRegex_Invalid` | 86.85 ns | **1.80× faster** | |
| `StandaloneGeneratedRegex_Valid` | 66.24 ns | **2.36× faster** | Raw `[GeneratedRegex]`, no wrapper |
| `StandaloneGeneratedRegex_Invalid` | 93.17 ns | **1.68× faster** | |
| **`RegexForge_EmailPattern_Valid`** | **67.65 ns** | **2.31× faster** | What you actually call |
| **`RegexForge_EmailPattern_Invalid`** | **91.46 ns** | **1.71× faster** | |

**RegexForge adds zero measurable overhead** over a standalone `[GeneratedRegex]`
(67.65 ns vs 66.24 ns — within noise). You get the clean API for free.

### Cold-start (first-call cost) — `RunStrategy.ColdStart`, 5 launches

> Measures the true first-call cost before any JIT/caching kicks in.

| Method | Mean | Allocated |
|---|---|---|
| `new Regex()` per call (uncompiled) | 2.687 ms | 10,320 B |
| `new Regex(Compiled)` per call | 12.313 ms | 43,120 B |
| **RegexForge `EmailPattern` (first touch)** | **7.742 ms** | **0 B** |

RegexForge's static initializer pays its setup cost once at first use,
then allocates nothing for every subsequent call — unlike `Regex(Compiled)`
which allocates 43 KB and takes 12 ms **every time** it is constructed.

## Roadmap

See [ROADMAP.md](ROADMAP.md) for the full v1/v2/v3 plan, including what's
deliberately deferred and why (e.g. credit card validation, category-nested pattern
browsing).

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md), including the pattern submission checklist
and the one category of pattern we won't accept (credit card numbers — PCI-DSS scope).

## FAQ

**Why isn't there an `IsValidCreditCard`?**
Deliberately deferred — see [CONTRIBUTING.md](CONTRIBUTING.md#patterns-we-will-not-accept-in-this-repository).

**Does `IbanPattern`/`SwiftPattern` validate the checksum?**
No — structural shape only. See the XML docs on each pattern for the specific limitation.

**Why is `RegexPatterns` a single flat class instead of nested by category?**
Flat IntelliSense lookup (`RegexPatterns.Em...` → `Email`) is faster for the common
case of "I know the pattern name" than requiring you to first know which category it
lives in. Category-based browsing is being considered for v2 as an additive alias,
not a replacement. See `ROADMAP.md`.

**Does this support .NET Framework?**
Yes, via `netstandard2.0` (.NET Framework 4.6.1+). Note: the `netstandard2.0` code
path is currently build-verified in CI but not yet behavior-tested with the same
rigor as the net8.0/net10.0 paths — see `CHANGELOG.md` known limitations.

**Is this fully RFC-compliant for email/URL/etc.?**
No regex-based validator is fully RFC 5322/3986 compliant — that's a known, accepted
limitation of regex-based validation in general, not unique to this library. We aim
for "correct for the overwhelming majority of real-world input," not formal RFC proof.

## License

MIT — see [LICENSE](LICENSE).
