using System;
using GitHub;

namespace ScientistDemo.Net
{
    class Program
    {
		private static int x = 3;
		private static int y = 1;

		static void Main(string[] args)
        {
            // Configure scientist
            Scientist.ResultPublisher = new ResultPublisher();

            // Perform test
            ICalculator calculator = new CalculatorExperiment();
            
			// Add
            int result = calculator.Add(x, y);
            Console.WriteLine($"Adding {x} to {y} gives {result}");

			// Subtract
            result = calculator.Subtract(x, y);
            Console.WriteLine($"Subtracting {y} from {x} gives {result}");

			// Quit program
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
