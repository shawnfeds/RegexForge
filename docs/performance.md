# Performance

## Caching strategy

Every validator constructs its `Regex` instance exactly once, on first use, and
reuses it for the lifetime of the process. You never pay regex construction cost
per call.

## Target-specific compilation

| Target | What happens |
|---|---|
| `net8.0` / `net10.0` | `[GeneratedRegex]` generates a derived `Regex` subclass at compile time — no reflection, no runtime regex parsing, ahead-of-time compiled matching logic. |
| `netstandard2.0` | A `Regex` is constructed once with `RegexOptions.Compiled`, which JIT-compiles the matching logic to IL on first use and caches it. |

The public API (`IsValid`, `Match`, etc.) is identical regardless of which path is
active — the difference is purely internal.

## Benchmarking

The `RegexForge.Benchmarks` project (BenchmarkDotNet) compares:

1. Raw `Regex.IsMatch(input, pattern)` — re-parses the pattern string on every call
   unless .NET's internal static cache happens to still hold it.
2. A manually compiled, cached `Regex` — the best a careful developer typically writes
   by hand without this library.
3. A standalone `[GeneratedRegex]` with no wrapper — isolates whether RegexForge's
   abstraction layer adds measurable overhead versus the raw source-generated regex.
4. `RegexForge.Validation.EmailPattern` — what consumers actually call.

A separate `ColdStartBenchmarks` class uses `RunStrategy.ColdStart` with multiple
process launches to measure first-touch cost specifically — BenchmarkDotNet's default
warm-loop strategy would otherwise amortize construction cost away and hide it.

Run locally:

```bash
dotnet run -c Release --project tests/RegexForge.Benchmarks/RegexForge.Benchmarks.csproj
```

Results from the latest release benchmark run will be published here once CI's
`benchmark.yml` workflow has run against a tagged release.

## Known gap

The `netstandard2.0` (.NET Framework) code path is currently build-verified in CI
but not yet covered by the same automated test suite as the `net8.0`/`net10.0` paths,
since xUnit requires a real runtime to execute against. See the project `CHANGELOG.md`
for the tracked plan to close this gap.
