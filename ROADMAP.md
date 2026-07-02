# Roadmap

## v1.0 — Foundation (this release)

- [x] `RegexForge.Core` abstractions
- [x] `RegexForge.Patterns` — 34 patterns across 8 categories
- [x] `RegexForge.Validation` — strongly-typed validators with full CRUD-style string operations
- [x] Multi-targeting: `netstandard2.0`, `net8.0`, `net10.0`
- [x] Source-generated regex on net8.0/net10.0, compiled fallback on netstandard2.0
- [x] Full unit test coverage (valid/invalid/null/empty/whitespace/unicode/large-input)
- [x] BenchmarkDotNet suite with cold-start and warm-start measurements
- [x] CI/CD: build, test, CodeQL, Dependabot, preview/release publishing, docs deployment
- [x] Console and Minimal API samples
- [ ] `netstandard2.0` fallback behavior testing (currently build-verified only — see CHANGELOG)

## v2.0 — Extensibility

- `RegexForge.Analyzers` — Roslyn analyzers warning on `new Regex(...)` where a
  RegexForge pattern exists, catastrophic backtracking detection, uncompiled regex
  warnings, duplicate regex detection, with automatic code fixes.
- Fluent `RegexBuilder` API.
- Country-specific localization validators beyond the current US/UK/India set
  (Canada, Australia, EU member states).
- Pattern category browsing API (e.g. `RegexPatterns.Identity.Email` as an alias
  path alongside the existing flat `RegexPatterns.Email`) — deferred from v1
  deliberately; revisit once real usage data shows whether category browsing is
  actually requested.
- Credit card / payment card number validation — deferred due to PCI-DSS scope
  considerations. See `CONTRIBUTING.md`.

## v3.0 — Tooling

- Source generator customization (user-supplied pattern sets compiled at build time).
- Regex visualizer.
- CLI for pattern testing against sample input.
- VS Code / Visual Studio extension.

## Explicitly out of scope (no current plans)

- Internationalized email (RFC 6531) validation — the v1 `EmailPattern` is
  ASCII-only by design.
- Full IBAN/SWIFT checksum validation (MOD-97) — structural validation only.
