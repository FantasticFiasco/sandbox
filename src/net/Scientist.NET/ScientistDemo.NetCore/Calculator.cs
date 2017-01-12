using System.Threading;

namespace ScientistDemo.Net
{
    class Calculator : ICalculator
    {
        private readonly int delay;

        public Calculator(int delay)
        {
            this.delay = delay;
        }


        public int Add(int x, int y)
        {
            Thread.Sleep(delay);

            return x + y;
        }

        public int Subtract(int x, int y)
        {
            Thread.Sleep(delay);

            return x - y;
        }
    }
}
