# Benchmarker

一些常见的 C# 代码性能测试

## List 遍历

|      Method |     Mean |    Error |   StdDev |   Median | Ratio | RatioSD |
|------------ |---------:|---------:|---------:|---------:|------:|--------:|
|     ForLoop | 66.86 ns | 0.635 ns | 0.594 ns | 66.65 ns |  1.00 |    0.00 |
| ForEachLoop | 69.73 ns | 2.010 ns | 5.767 ns | 67.98 ns |  0.97 |    0.03 |
|        Linq | 66.09 ns | 1.313 ns | 1.798 ns | 66.21 ns |  0.99 |    0.03 |
|        Span | 50.67 ns | 1.011 ns | 0.993 ns | 50.80 ns |  0.76 |    0.02 |

四个方法分别为：

1. 使用 `for` 循环
1. 使用 `foreach` 循环
1. 使用 `List.ForEach()` 方法
1. 使用 `CollectionsMarshal.AsSpan()` 将 list 转为 `Span<>`，再用 `for` 循环遍历

## 计算正整数的正整数次方

|  Method |  n | k |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|-------- |--- |-- |----------:|----------:|----------:|----------:|------:|--------:|
| MathPow |  2 | 4 | 25.771 ns | 0.9701 ns | 2.8604 ns | 25.549 ns |  1.00 |    0.00 |
| FastPow |  2 | 4 |  1.685 ns | 0.0800 ns | 0.2282 ns |  1.645 ns |  0.07 |    0.01 |
|         |    |   |           |           |           |           |       |         |
| MathPow |  3 | 5 | 22.966 ns | 0.4830 ns | 0.6108 ns | 23.140 ns |  1.00 |    0.00 |
| FastPow |  3 | 5 |  2.145 ns | 0.0723 ns | 0.1525 ns |  2.159 ns |  0.10 |    0.01 |
|         |    |   |           |           |           |           |       |         |
| MathPow |  8 | 8 | 23.491 ns | 0.4985 ns | 1.0406 ns | 23.436 ns |  1.00 |    0.00 |
| FastPow |  8 | 8 |  2.717 ns | 0.1906 ns | 0.5619 ns |  2.564 ns |  0.11 |    0.01 |
|         |    |   |           |           |           |           |       |         |
| MathPow | 10 | 9 | 25.494 ns | 1.1474 ns | 3.3830 ns | 24.312 ns |  1.00 |    0.00 |
| FastPow | 10 | 9 |  2.666 ns | 0.1396 ns | 0.4073 ns |  2.663 ns |  0.11 |    0.02 |

`MathPow` 方法使用 `Math.Pow()` 原生方法，而 `FastPow` 采用快速幂算法。

## 将多个字符拼接为字符串

|        Method |       Mean |     Error |     StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------- |-----------:|----------:|-----------:|------:|--------:|----------:|------------:|
|    StringJoin | 176.797 ns | 6.8261 ns | 19.4752 ns |  1.00 |    0.00 |     392 B |        1.00 |
|     NewString |   9.374 ns | 0.2574 ns |  0.7090 ns |  0.05 |    0.01 |      48 B |        0.12 |
| StringBuilder |  34.751 ns | 0.7526 ns |  1.7886 ns |  0.21 |    0.02 |     152 B |        0.39 |

三个方法分别为：

1. 使用 `string.Join("", chars)` 的方式
1. 使用 `new string(chars)` 的方式
1. 使用 `StringBuilder` 的方式（初始化容量为 `chars.Length`）
