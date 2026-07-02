# RegexForge

Production-tested regular expression patterns and strongly-typed validators for .NET.

```csharp
using RegexForge.Validation;

bool isValid = EmailPattern.IsValid("john@example.com");
```

## Why RegexForge

Most projects accumulate ad-hoc regex copied from StackOverflow or Regex101, with no
tests, no documentation of edge cases, and no consistency between patterns. RegexForge
provides a single, documented, tested source for the patterns you reach for repeatedly.

Every pattern's documentation states explicitly what it does **not** guarantee — see
each pattern page for limitations (e.g. `IbanPattern` does not validate the MOD-97
checksum, `EmailPattern` is ASCII-only).

## Where to start

- [Getting Started](guides/getting-started.md) — installation and your first validator call.
- [Pattern catalog](patterns/index.md) — every pattern, organized by category.
- [Performance](performance.md) — how caching and source generation work across target frameworks.

## Supported frameworks

| Target | Strategy |
|---|---|
| `netstandard2.0` | Cached, compiled `Regex` (.NET Framework 4.6.1+ and modern .NET) |
| `net8.0` | `[GeneratedRegex]` source-generated regex |
| `net10.0` | `[GeneratedRegex]` source-generated regex |
