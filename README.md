# Benchmarker

一些常见的 C# 代码性能测试

## 遍历

### List 遍历

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

## 比较两个序列

|               Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|------:|--------:|----------:|------------:|
|              ForLoop | 83.05 ns | 1.557 ns | 2.233 ns |  1.00 |    0.00 |         - |          NA |
|        UnsafeForLoop | 53.04 ns | 1.075 ns | 1.360 ns |  0.64 |    0.03 |         - |          NA |
|     SequentialEquals | 29.07 ns | 0.526 ns | 0.492 ns |  0.35 |    0.01 |         - |          NA |
| SpanSequentialEquals | 10.37 ns | 0.232 ns | 0.418 ns |  0.12 |    0.01 |         - |          NA |

1. 使用 `for` 循环
1. 在 `unsafe` 代码块中使用 `for` 循环
1. 使用 `Enumerable.SequenceEquals()` 方法
1. 将 `Array` 转为 `Span` 后再使用 `SequenceEquals()` 方法

## 数学

### 计算正整数的正整数次方

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

## 字符串

### 将多个字符拼接为字符串

|        Method |       Mean |     Error |     StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------- |-----------:|----------:|-----------:|------:|--------:|----------:|------------:|
|    StringJoin | 176.797 ns | 6.8261 ns | 19.4752 ns |  1.00 |    0.00 |     392 B |        1.00 |
|     NewString |   9.374 ns | 0.2574 ns |  0.7090 ns |  0.05 |    0.01 |      48 B |        0.12 |
| StringBuilder |  34.751 ns | 0.7526 ns |  1.7886 ns |  0.21 |    0.02 |     152 B |        0.39 |

三个方法分别为：

1. 使用 `string.Join("", chars)` 的方式
1. 使用 `new string(chars)` 的方式
1. 使用 `StringBuilder` 的方式（初始化容量为 `chars.Length`）

### 格式化字符串

|              Method |     Mean |   Error |  StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------- |---------:|--------:|--------:|------:|--------:|----------:|------------:|
|        StringFormat | 210.1 ns | 4.08 ns | 6.10 ns |  1.00 |    0.00 |     264 B |        1.00 |
| StringInterpolation | 130.3 ns | 2.60 ns | 4.13 ns |  0.62 |    0.03 |     160 B |        0.61 |
|      AppendMultiple | 119.8 ns | 2.24 ns | 2.58 ns |  0.57 |    0.02 |     160 B |        0.61 |
|        AppendFormat | 189.0 ns | 3.63 ns | 7.58 ns |  0.90 |    0.04 |     208 B |        0.79 |

1. 使用 `StringBuilder.Append` + `String.Format` 的方式（效率最低）
1. 使用 `StringBuilder.Append` + 字符串插值（`$"{arg}"`）的方式
1. 多次使用 `StringBuilder.Append`（效率最高）
1. 使用 `StringBuilder.AppendFormat` 的方式

由于字符串插值本身所带来的性能提升，使得 `StringBuilder.Append` + 字符串插值的方式性能第二高；而传统的 `StringBuilder.AppendFormat` 的性能仅高于 `StringBuilder.Append` + `String.Format`

## 反射

### 用反射修改类的属性的值

|                        Method |       Mean |     Error |    StdDev |     Median | Ratio | Allocated | Alloc Ratio |
|------------------------------ |-----------:|----------:|----------:|-----------:|------:|----------:|------------:|
|               NaiveReflection | 43.5744 ns | 0.3113 ns | 0.2759 ns | 43.6294 ns | 1.000 |      24 B |        1.00 |
|      SetPropertyValueDirectly |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns | 0.000 |         - |        0.00 |
| SetPropertyWithCachedPropInfo | 24.8807 ns | 0.4217 ns | 0.3945 ns | 24.9604 ns | 0.571 |      24 B |        1.00 |
| SetPropertyWithCachedDelegate |  1.4382 ns | 0.0529 ns | 0.0839 ns |  1.3934 ns | 0.035 |         - |        0.00 |

四个方法分别为：

1. 使用 `PropertyInfo.SetValue()` 的方式
1. 直接使用 `obj.Value = 1` 的方式
1. 使用缓存的 `PropertyInfo.SetValue()` 的方式
1. 使用缓存的 `Action<T, TProperty>` 的方式

## 杂项

### 在每次循环时 Try-Catch

|             Method | LoopCount |       Mean |     Error |    StdDev |
|------------------- |---------- |-----------:|----------:|----------:|
| TryCatchInEachLoop |        10 |   9.769 ns | 0.1093 ns | 0.0913 ns |
|          PlainLoop |        10 |   2.840 ns | 0.0313 ns | 0.0277 ns |
| TryCatchInEachLoop |       100 |  85.265 ns | 1.0599 ns | 0.9396 ns |
|          PlainLoop |       100 |  36.678 ns | 0.3357 ns | 0.2803 ns |
| TryCatchInEachLoop |      1000 | 774.706 ns | 9.2063 ns | 8.1611 ns |
|          PlainLoop |      1000 | 311.519 ns | 2.7005 ns | 2.3939 ns |
