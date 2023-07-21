using Benchmarker.Runners;

namespace Benchmarker.Test
{
    public class Test_StringBuilder
    {
        private StringBuilderInit _init = new();
        private StringBuilderFormat _format = new();

        [Fact]
        public void Test_StringBuilderInit()
        {
            var o1 = _init.AppendWithoutInit();
            var o2 = _init.AppendWithInit();
            var o3 = _init.AppendJoin();
            Assert.Equal(o1, o2);
            Assert.Equal(o1, o3);
        }

        [Fact]
        public void Test_StringBuilderFormat()
        {
            string o1 = _format.AppendWithoutFormat();
            string o2 = _format.AppendFormat();
            string o3 = _format.AppendMultiple();
            Assert.Equal(o1, o2);
            Assert.Equal(o1, o3);
        }
    }
}
