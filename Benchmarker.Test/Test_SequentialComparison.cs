namespace Benchmarker.Test;

public class Test_SequentialComparison
{
    [Fact]
    public void Test_SequentialComparison_ShouldReturnTrue_WhenAllMethodsAllAreEqual()
    {
        // arrange
        var comp = new Runners.SequentialComparison();
        var expected = true;

        // act
        var forLoopActual = comp.ForLoop();
        var unsafeForLoopActual = comp.UnsafeForLoop();
        var sequentialEqualsActual = comp.SequentialEquals();
        var spanSequentialEqualsActual = comp.SpanSequentialEquals();

        // assert
        Assert.Equal(expected, forLoopActual);
        Assert.Equal(expected, unsafeForLoopActual);
        Assert.Equal(expected, sequentialEqualsActual);
        Assert.Equal(expected, spanSequentialEqualsActual);
    }
}
