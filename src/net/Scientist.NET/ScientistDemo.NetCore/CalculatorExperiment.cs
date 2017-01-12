using GitHub;

namespace ScientistDemo.Net
{
    class CalculatorExperiment : ICalculator
    {
        private readonly ICalculator current;
        private readonly ICalculator candidate;

        public CalculatorExperiment()
        {
            current = new Calculator(1000);
            candidate = new Calculator(200);
        }

        public int Add(int x, int y)
        {
            return Scientist.Science<int>("add", experiment =>
            {
                experiment.Use(() => current.Add(x, y));
                experiment.Try(() => candidate.Add(x, y));
            });
        }

        public int Subtract(int x, int y)
        {
            return Scientist.Science<int>("subtract", experiment =>
            {
                experiment.Use(() => current.Subtract(x, y));
                experiment.Try(() => candidate.Subtract(x, y));
            });
        }
    }
}
