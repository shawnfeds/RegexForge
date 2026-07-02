# Getting Started

## Install

```bash
dotnet add package RegexForge.Validation
```

This brings in `RegexForge.Core` and `RegexForge.Patterns` as transitive dependencies.

## Your first validator call

```csharp
using RegexForge.Validation;

if (EmailPattern.IsValid(request.Email))
{
    // proceed
}
```

Every validator exposes the same seven operations:

| Method | Purpose |
|---|---|
| `IsValid(string?)` | Returns `true`/`false`. Never throws on `null` or empty input. |
| `Match(string?)` | Returns the first `Match`, which may be unsuccessful. |
| `Matches(string?)` | Returns every match found. |
| `TryMatch(string?, out Match?)` | Non-throwing lookup; returns `false` with `match = null` if nothing found. |
| `Replace(string?, string)` | Replaces all matches with the given replacement. |
| `Split(string?)` | Splits the input on every match. |
| `Count(string?)` | Counts matches without allocating a result you don't need. |

## Choosing between `Patterns` and `Validation`

If you need the raw pattern string — for example, to build your own `Regex` instance
with custom options, or to pass into a third-party API that wants a pattern string —
use `RegexForge.Patterns` directly:

```csharp
using RegexForge.Patterns;

var customRegex = new Regex(RegexPatterns.Email, RegexOptions.IgnoreCase);
```

For everything else, `RegexForge.Validation` is almost always what you want — it
handles caching and platform-appropriate compilation for you.
