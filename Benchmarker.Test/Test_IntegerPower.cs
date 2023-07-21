using Benchmarker.Runners;

namespace Benchmarker.Test
{
    public class Test_IntegerPower
    {
        private readonly IntegerPower _runner = new();

        [Fact]
        public void Test_MathPow()
        {
            Assert.Equal(16, _runner.MathPow(2, 4));
            Assert.Equal(243, _runner.MathPow(3, 5));
            Assert.Equal(16_777_216, _runner.MathPow(8, 8));
            Assert.Equal(1_000_000_000, _runner.MathPow(10, 9));
        }

        [Fact]
        public void Test_FastPow()
        {
            Assert.Equal(16, _runner.FastPow(2, 4));
            Assert.Equal(243, _runner.FastPow(3, 5));
            Assert.Equal(16_777_216, _runner.FastPow(8, 8));
            Assert.Equal(1_000_000_000, _runner.FastPow(10, 9));
        }
    }
}