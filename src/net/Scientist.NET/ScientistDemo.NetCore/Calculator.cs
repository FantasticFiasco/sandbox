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
			using (new LogContext($"Add with delay {delay}"))
			{
				Thread.Sleep(delay);
				return x + y;
			}
        }

        public int Subtract(int x, int y)
        {
			using (new LogContext($"Subtract with delay {delay}"))
			{
				Thread.Sleep(delay);
				return x - y;
			}
        }
    }
}
