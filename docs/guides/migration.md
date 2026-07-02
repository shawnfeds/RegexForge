# Migrating from raw Regex

If your codebase already has hand-rolled regex scattered across the project, here's
the typical migration path.

## Before

```csharp
private static readonly Regex EmailRegex = new Regex(
    @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
    RegexOptions.Compiled);

public bool IsValidEmail(string email) => EmailRegex.IsMatch(email);
```

## After

```csharp
using RegexForge.Validation;

public bool IsValidEmail(string email) => EmailPattern.IsValid(email);
```

## What you gain

- One cached instance per pattern, shared across your whole process — no risk of
  someone accidentally instantiating `new Regex(...)` in a hot path.
- `[GeneratedRegex]` on net8.0/net10.0 automatically, no code change required.
- Documented edge-case behavior (null/empty handling, RFC limitations) instead of
  whatever your original pattern happened to do.
- Consistent API across every pattern (`Match`, `Replace`, `Split`, `Count` all behave
  the same way regardless of which validator you're using).

## What to check before swapping

RegexForge's patterns may be **stricter or looser** than your existing hand-rolled
regex in edge cases. Before swapping a pattern in a production validation path, run
your existing test suite (or write one) against the new pattern and diff the results.
The [pattern catalog](../patterns/index.md) documents each pattern's known limitations
so you can decide if the difference matters for your use case.
