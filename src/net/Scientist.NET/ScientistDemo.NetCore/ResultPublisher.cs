using System;
using System.Threading.Tasks;
using GitHub;

namespace ScientistDemo.Net
{
    class ResultPublisher : IResultPublisher
    {
        public Task Publish<T, TClean>(Result<T, TClean> result)
        {
            Console.WriteLine($"Publishing results for experiment '{result.ExperimentName}'");
            Console.WriteLine($"Result: {(result.Matched ? "MATCH" : "MISMATCH")}");
            Console.WriteLine($"Control value: {result.Control.Value}");
            Console.WriteLine($"Control duration: {result.Control.Duration}");

            foreach (var observation in result.Candidates)
            {
                Console.WriteLine($"Candidate name: {observation.Name}");
                Console.WriteLine($"Candidate value: {observation.Value}");
                Console.WriteLine($"Candidate duration: {observation.Duration}");
            }

            if (result.Mismatched)
            {
                Console.WriteLine($"ERROR: Candidate diverges from current implementation, is {result}");
            }

            return Task.CompletedTask;
        }
    }
}
