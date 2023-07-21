using System.Text;

namespace Benchmarker.Runners;

[MemoryDiagnoser(false)]
public class StringBuilderInit
{
    private readonly string[] strings =
    {
        "Mon.",
        "Tues.",
        "Wed.",
        "Thur.",
        "Fri.",
        "Sat.",
        "Sun."
    };

    [Benchmark(Baseline = true)]
    public string AppendWithoutInit()
    {
        StringBuilder sb = new StringBuilder();
        foreach (string s in strings)
            sb.Append(s);
        return sb.ToString();
    }

    [Benchmark]
    public string AppendWithInit()
    {
        StringBuilder sb = new StringBuilder(strings.Sum(x => x.Length));
        foreach (string s in strings)
            sb.Append(s);
        return sb.ToString();
    }

    [Benchmark]
    public string AppendJoin()
    {
        return new StringBuilder(strings.Sum(x => x.Length)).AppendJoin("", strings).ToString();
    }
}

[MemoryDiagnoser(false)]
public class StringBuilderFormat
{
    private string arg1 = "hello";
    private int arg2 = 123;
    private float arg3 = 30f;

    [Benchmark(Baseline = true)]
    public string AppendWithoutFormat()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{arg1}, {arg2}, {arg3}");
        return sb.ToString();
    }

    [Benchmark]
    public string AppendMultiple()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(arg1).Append(", ").Append(arg2).Append(", ").Append(arg3);
        return sb.ToString();
    }

    [Benchmark]
    public string AppendFormat()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("{0}, {1}, {2}", arg1, arg2, arg3);
        return sb.ToString();
    }
}