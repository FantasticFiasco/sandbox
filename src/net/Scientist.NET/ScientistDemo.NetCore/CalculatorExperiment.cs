using GitHub;

namespace ScientistDemo.Net
{
    class CalculatorExperiment : ICalculator
    {
        private readonly ICalculator reference;
        private readonly ICalculator observation;

        public CalculatorExperiment()
        {
            reference = new Calculator(1000);
            observation = new Calculator(200);
        }

        public int Add(int x, int y)
        {
            return Scientist.Science<int>("calculator.add", experiment =>
            {
                experiment.Use(() => reference.Add(x, y));
                experiment.Try(() => observation.Add(x, y));
            });
        }

        public int Subtract(int x, int y)
        {
            return Scientist.Science<int>("calculator.subtract", experiment =>
            {
                experiment.Use(() => reference.Subtract(x, y));
                experiment.Try(() => observation.Subtract(x, y));
            });
        }
    }
}
