using BenchmarkDotNet.Attributes;

namespace Benchmarker.Runners;

/// <summary>
/// 测试计算 n 的 k 次方（n、k 均为正整数）的效率
/// </summary>
public class IntegerPower
{
    [Benchmark(Baseline = true)]
    [Arguments(2, 4)]
    [Arguments(3, 5)]
    [Arguments(8, 8)]
    [Arguments(10, 9)]
    public int MathPow(int n, int k)
    {
        return (int)Math.Pow(n, k);
    }

    [Benchmark]
    [Arguments(2, 4)]
    [Arguments(3, 5)]
    [Arguments(8, 8)]
    [Arguments(10, 9)]
    public int FastPow(int n, int k)
    {
        int result = 1;
        while (k > 0)
        {
            if ((k & 1) == 1)
            {
                result *= n;
            }
            n *= n;
            k >>= 1;
        }
        return result;
    }
}
