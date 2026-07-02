using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using RegexForge.Patterns;

namespace RegexForge.Benchmarks;

/// <summary>
/// Isolates first-call ("cold start") cost from steady-state ("warm start") cost.
/// A new <see cref="Regex"/> instance is constructed inside the benchmarked method
/// itself so BenchmarkDotNet's iteration setup cannot amortize the construction cost
/// away, which is what a naive warm-loop benchmark would otherwise hide.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(RunStrategy.ColdStart, launchCount: 5, warmupCount: 0, iterationCount: 1)]
public class ColdStartBenchmarks
{
    private const string ValidEmail = "john.doe@example.com";

    [Benchmark(Baseline = true, Description = "new Regex() per call (uncompiled)")]
    public bool NewUncompiledRegex_ColdStart()
    {
        var regex = new Regex(RegexPatterns.Email);
        return regex.IsMatch(ValidEmail);
    }

    [Benchmark(Description = "new Regex(Compiled) per call")]
    public bool NewCompiledRegex_ColdStart()
    {
        var regex = new Regex(RegexPatterns.Email, RegexOptions.Compiled);
        return regex.IsMatch(ValidEmail);
    }

#if NET7_0_OR_GREATER
    [Benchmark(Description = "RegexForge EmailPattern (first touch, triggers static init)")]
    public bool RegexForgeEmailPattern_ColdStart()
    {
        // Note: in-process, the static cctor for EmailPattern will only truly run
        // "cold" once per process. BenchmarkDotNet's separate launchCount processes
        // (out-of-process launches) are what make this a genuine cold measurement
        // rather than a no-op after the first iteration.
        return RegexForge.Validation.EmailPattern.IsValid(ValidEmail);
    }
#endif
}
