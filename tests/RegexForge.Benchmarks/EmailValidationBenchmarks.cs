using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using RegexForge.Patterns;
using RegexForge.Validation;

namespace RegexForge.Benchmarks;

/// <summary>
/// Compares RegexForge's <see cref="EmailPattern"/> against the alternatives a
/// developer would otherwise reach for, per the project's stated benchmark goals:
/// raw <see cref="Regex.IsMatch(string,string)"/>, a manually compiled <see cref="Regex"/>,
/// and (on .NET 7+) a standalone <c>[GeneratedRegex]</c> with no caching wrapper.
/// </summary>
[MemoryDiagnoser]
[RankColumn]
public partial class EmailValidationBenchmarks
{
    private const string ValidEmail = "john.doe@example.com";
    private const string InvalidEmail = "not-an-email-at-all";

    // Baseline 1: raw Regex.IsMatch — recompiles the pattern internally on every call
    // unless .NET's internal regex cache happens to still hold it.
    [Benchmark(Baseline = true)]
    public bool RawRegexIsMatch_Valid() => Regex.IsMatch(ValidEmail, RegexPatterns.Email);

    [Benchmark]
    public bool RawRegexIsMatch_Invalid() => Regex.IsMatch(InvalidEmail, RegexPatterns.Email);

    // Baseline 2: manually compiled and cached Regex — what a careful developer
    // would write by hand without this library.
    private static readonly Regex CompiledRegex = new Regex(
        RegexPatterns.Email,
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    [Benchmark]
    public bool ManuallyCompiledRegex_Valid() => CompiledRegex.IsMatch(ValidEmail);

    [Benchmark]
    public bool ManuallyCompiledRegex_Invalid() => CompiledRegex.IsMatch(InvalidEmail);

#if NET7_0_OR_GREATER
    // Baseline 3: standalone GeneratedRegex with no validator wrapper —
    // isolates whether RegexForge's abstraction adds any measurable overhead
    // on top of the raw source-generated regex.
    [GeneratedRegex(RegexPatterns.Email, RegexOptions.CultureInvariant)]
    private static partial Regex StandaloneGeneratedRegex();

    [Benchmark]
    public bool StandaloneGeneratedRegex_Valid() => StandaloneGeneratedRegex().IsMatch(ValidEmail);

    [Benchmark]
    public bool StandaloneGeneratedRegex_Invalid() => StandaloneGeneratedRegex().IsMatch(InvalidEmail);
#endif

    // RegexForge itself — what consumers actually call.
    [Benchmark]
    public bool RegexForge_EmailPattern_Valid() => EmailPattern.IsValid(ValidEmail);

    [Benchmark]
    public bool RegexForge_EmailPattern_Invalid() => EmailPattern.IsValid(InvalidEmail);
}
