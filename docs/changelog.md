# Changelog

All notable changes to RegexForge are documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/).

## [Unreleased]

## [1.0.0] - TBD

### Added

- `RegexForge.Core` — base abstractions (`IRegexPattern`, `IRegexValidator`, `RegexValidatorBase`).
- `RegexForge.Patterns` — 34 production pattern constants across 8 categories:
  Identity, Internet, Programming, Numbers, Dates, Security, Payments, Localization.
- `RegexForge.Validation` — strongly-typed validators (`EmailPattern`, `GuidPattern`, etc.)
  with `IsValid`, `Match`, `Matches`, `TryMatch`, `Replace`, `Split`, and `Count`.
- Multi-targeting: `netstandard2.0` (covers .NET Framework 4.6.1+), `net8.0`, `net10.0`.
- Source-generated regex (`[GeneratedRegex]`) on `net8.0`/`net10.0`; cached compiled
  `Regex` fallback on `netstandard2.0`.
- Full unit test suite covering valid, invalid, null, empty, whitespace, unicode,
  and large-input edge cases for every validator.
- BenchmarkDotNet suite comparing RegexForge against raw `Regex.IsMatch`, manually
  compiled `Regex`, and standalone `GeneratedRegex`, including cold-start measurements.
- CI/CD: build, test, benchmark (manual/release-triggered), CodeQL, Dependabot,
  preview and release publish workflows, documentation deployment.
- Console and Minimal API samples.

### Known limitations

- `netstandard2.0` fallback code path is build-verified in CI but not yet
  behavior-tested (xUnit requires a real runtime; tracked as a follow-up).
- `IbanPattern` and `SwiftPattern` validate structural shape only — no checksum
  (MOD-97) or country-specific length verification.
- `HtmlTagPattern` is a shallow structural match, not a substitute for a real
  HTML parser.
- Credit card validation deferred to v2.0 (PCI-DSS scope considerations).

[Unreleased]: https://github.com/shawnfeds/RegexForge/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/shawnfeds/RegexForge/releases/tag/v1.0.0
