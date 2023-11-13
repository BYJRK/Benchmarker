using System.Diagnostics;

namespace Benchmarker.Runners;

[MemoryDiagnoser(false)]
public class Stopwatchs
{
    [Benchmark(Baseline = true)]
    public TimeSpan DateTime_Now()
    {
        var now = DateTime.Now;
        return DateTime.Now - now;
    }

    [Benchmark]
    public TimeSpan Stopwatch_StartNew()
    {
        var sw = Stopwatch.StartNew();
        return sw.Elapsed;
    }

    [Benchmark]
    public TimeSpan Stopwatch_GetElapsedTime()
    {
        var now = Stopwatch.GetTimestamp();
        return Stopwatch.GetElapsedTime(now);
    }
}
