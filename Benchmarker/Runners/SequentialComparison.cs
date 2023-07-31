using System.Collections;

namespace Benchmarker.Runners;

[MemoryDiagnoser(false)]
public class SequentialComparison
{
    private readonly int[] _array1;
    private readonly int[] _array2;

    public SequentialComparison()
    {
        _array1 = Enumerable.Range(1, 100).ToArray();
        _array2 = Enumerable.Range(1, 100).ToArray();
    }

    [Benchmark(Baseline = true)]
    public bool ForLoop()
    {
        if (_array1.Length != _array2.Length)
        {
            return false;
        }

        for (var i = 0; i < _array1.Length; i++)
        {
            if (_array1[i] != _array2[i])
            {
                return false;
            }
        }

        return true;
    }

    [Benchmark]
    public unsafe bool UnsafeForLoop()
    {
        if (_array1.Length != _array2.Length)
        {
            return false;
        }

        fixed (int* p1 = _array1, p2 = _array2)
        {
            var i = 0;
            var len = _array1.Length;
            while (i < len - 1)
            {
                if (p1[i] != p2[i])
                {
                    return false;
                }

                i++;
            }

            return true;
        }
    }

    [Benchmark]
    public bool SequentialEquals()
    {
        return _array1.SequenceEqual(_array2);
    }

    [Benchmark]
    public bool SpanSequentialEquals()
    {
        return _array1.AsSpan().SequenceEqual(_array2.AsSpan());
    }

    /// <summary>
    /// 调用 <see cref="IStructuralEquatable.Equals"/> 方法来比较两个数组是否相等
    /// </summary>
    /// <remarks>
    /// 该方法速度过慢，基本没有使用的价值，仅作为参考，不参与测试
    /// </remarks>
    public bool StructuralEquatable()
    {
        var seq1 = _array1 as IStructuralEquatable;
        var seq2 = _array2 as IStructuralEquatable;
        return seq1.Equals(seq2, EqualityComparer<int>.Default);
    }
}
