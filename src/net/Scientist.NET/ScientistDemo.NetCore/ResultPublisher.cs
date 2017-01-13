using System;
using System.Threading.Tasks;
using GitHub;

namespace ScientistDemo.Net
{
    class ResultPublisher : IResultPublisher
    {
        public Task Publish<T, TClean>(Result<T, TClean> result)
        {
			Log($"Publishing results for experiment '{result.ExperimentName}'");
			Log($"Result: {(result.Matched ? "MATCH" : "MISMATCH")}");
			Log($"Control value: {result.Control.Value}");
			Log($"Control duration: {result.Control.Duration}");

            foreach (var observation in result.Candidates)
            {
				Log($"Candidate value: {observation.Value}");
				Log($"Candidate duration: {observation.Duration}");
            }

            if (result.Mismatched)
            {
				Log($"ERROR: Candidate diverges from current implementation, is {result}");
            }

            return Task.CompletedTask;
        }

	    private static void Log(string message)
	    {
		    Console.WriteLine($"\t[EXPERIMENT] {message}");
	    }
    }
}
