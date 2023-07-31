using System.Text;

namespace Benchmarker.Runners;

[MemoryDiagnoser(false)]
public class ConcatChars
{
    private readonly char[] chars = { 'h', 'e', 'l', 'l', 'o', ',', ' ', 'w', 'o', 'r', 'l', 'd', '!' };

    [Benchmark(Baseline = true)]
    public string StringJoin()
    {
        return string.Join("", chars);
    }

    [Benchmark]
    public string NewString()
    {
        return new string(chars);
    }

    [Benchmark]
    public string StringBuilder()
    {
        var sb = new StringBuilder(chars.Length);
        foreach (char c in chars)
            sb.Append(c);
        return sb.ToString();
    }
}
