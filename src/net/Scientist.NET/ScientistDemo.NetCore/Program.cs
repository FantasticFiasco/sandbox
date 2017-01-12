using System;
using GitHub;

namespace ScientistDemo.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configure scientist
            Scientist.ResultPublisher = new ResultPublisher();

            // Perform test
            ICalculator calculator = new CalculatorExperiment();
            int x = 3;
            int y = 1;
            
            int result = calculator.Add(x, y);
            Console.WriteLine($"Adding {x} to {y} gives {result}");

            result = calculator.Subtract(x, y);
            Console.WriteLine($"Subtracting {y} from {x} gives {result}");

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
