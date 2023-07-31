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
            var appendWithoutInit = _init.AppendWithoutInit();
            var appendWithInit = _init.AppendWithInit();
            var appendJoin = _init.AppendJoin();
            Assert.Equal(appendWithoutInit, appendWithInit);
            Assert.Equal(appendWithoutInit, appendJoin);
        }

        [Fact]
        public void Test_StringBuilderFormat()
        {
            var stringFormatResult = _format.StringFormat();
            var appendFormatResult = _format.AppendFormat();
            var appendMultipleResult = _format.AppendMultiple();
            var stringInterpolationResult = _format.StringInterpolation();
            Assert.True(stringInterpolationResult == stringFormatResult);
            Assert.True(stringInterpolationResult == appendFormatResult);
            Assert.True(stringInterpolationResult == appendMultipleResult);
        }
    }
}
