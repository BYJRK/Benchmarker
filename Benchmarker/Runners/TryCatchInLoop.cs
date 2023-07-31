namespace Benchmarker.Runners
{
    public class TryCatchInLoop
    {
        [Params(10, 100, 1000)]
        public int LoopCount { get; set; }

        [Benchmark]
        public int TryCatchInEachLoop()
        {
            int sum = 0;
            for (int i = 0; i < LoopCount; i++)
            {
                try
                {
                    sum += i;
                }
                catch
                {

                }
            }
            return sum;
        }

        [Benchmark]
        public int PlainLoop()
        {
            int sum = 0;
            for (int i = 0; i < LoopCount; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
}
