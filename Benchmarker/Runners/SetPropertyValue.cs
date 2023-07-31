using System.Reflection;

namespace Benchmarker.Runners;

[MemoryDiagnoser(false)]
public class SetPropertyValue
{
    private readonly TestClass _testClass = new();

    private readonly PropertyInfo _propertyInfo = typeof(TestClass).GetProperty(
        nameof(TestClass.Number)
    )!;

    private readonly Action<TestClass, int> _setter = typeof(TestClass)
        .GetProperty(nameof(TestClass.Number))
        .GetSetMethod()
        .CreateDelegate<Action<TestClass, int>>();

    [Benchmark(Baseline = true)]
    public void NaiveReflection()
    {
        var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.Number));
        propertyInfo!.SetValue(_testClass, 1);
    }

    [Benchmark]
    public void SetPropertyValueDirectly()
    {
        _testClass.Number = 1;
    }

    [Benchmark]
    public void SetPropertyWithCachedPropInfo() 
    {
        _propertyInfo.SetValue(_testClass, 1);
    }

    [Benchmark]
    public void SetPropertyWithCachedDelegate()
    {
        _setter(_testClass, 1);
    }
}

public class TestClass
{
    public int Number { get; set; }
}
