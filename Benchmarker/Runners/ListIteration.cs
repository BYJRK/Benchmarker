using System.Runtime.InteropServices;

namespace Benchmarker.Runners;

[MemoryDiagnoser(false)]
public class ListIteration
{
    private readonly List<int> _list = Enumerable.Range(1, 100).ToList();

    [Benchmark(Baseline = true)]
    public int ForLoop()
    {
        var sum = 0;
        for (var i = 0; i < _list.Count; i++)
        {
            sum += _list[i];
        }

        return sum;
    }

    [Benchmark]
    public int ForEachLoop()
    {
        var sum = 0;
        foreach (var item in _list)
        {
            sum += item;
        }

        return sum;
    }

    [Benchmark]
    public int Linq()
    {
        return _list.Sum();
    }

    [Benchmark]
    public int Span()
    {
        var sum = 0;
        var span = CollectionsMarshal.AsSpan(_list);
        for (var i = 0; i < span.Length; i++)
        {
            sum += span[i];
        }
        return sum;
    }
}
