# Contributing to RegexForge

Thanks for considering a contribution. A few ground rules before you open a PR.

## Getting started

```bash
git clone https://github.com/shawnfeds/RegexForge.git
cd RegexForge
dotnet restore
dotnet test tests/RegexForge.Tests/RegexForge.Tests.csproj
```

## Adding a new pattern

Every pattern requires, at minimum:

1. The raw pattern constant in `RegexForge.Patterns/RegexPatterns.cs`, with XML docs
   including a `<summary>` and an `<example>`.
2. A corresponding validator in `RegexForge.Validation/`, following the existing
   `#if NET7_0_OR_GREATER` / `[GeneratedRegex]` template used by every other validator.
3. A test class in `RegexForge.Tests/` covering: valid inputs, invalid inputs, `null`,
   empty string, whitespace-only, unicode where relevant, and at least one very large
   adversarial-length string to rule out pathological backtracking.
4. Before submitting, manually verify your test fixtures against the actual pattern
   (e.g. with a quick Python or `Regex.IsMatch` script) — generated test data has
   produced real mismatches in this project before. Don't assume a fixture is correct
   just because it reads correctly in English.

## Patterns we will not accept in this repository

**Credit card number validation.** Regex-based credit card validation sits in a
PCI-DSS grey area: shipping a general-purpose library that validates card numbers can
create compliance exposure for downstream consumers who treat "passes RegexForge
validation" as a substitute for proper PAN handling. We are deliberately not shipping
this pattern. If you need card number format checks, implement them in your own
application with appropriate PCI-DSS scoping, ideally backed by a real card-issuer
BIN/Luhn library rather than regex alone.

## Code style

- `.editorconfig` and StyleCop rules are enforced; `dotnet format --verify-no-changes`
  must pass.
- `TreatWarningsAsErrors` is on — a clean build is a prerequisite for merge.
- 100% XML documentation on public members is required (`GenerateDocumentationFile`
  is enabled and `CS1591` is suppressed only as a temporary convenience — don't rely
  on the suppression to skip writing docs).

## Pull requests

- One logical change per PR.
- Include or update tests for any behavior change.
- CI must be green (`build.yml`, `test.yml`, `codeql.yml`) before review.
