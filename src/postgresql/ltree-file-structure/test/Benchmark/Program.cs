using Benchmark.Benchmarks;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    class Program
    {
        static void Main()
        {
            // Nodes
            BenchmarkRunner.Run<GetNode>();
            BenchmarkRunner.Run<GetNodes>();
            BenchmarkRunner.Run<GetDescendants>();
            BenchmarkRunner.Run<GetAncestors>();

            // Permissions
            BenchmarkRunner.Run<GetInheritedRole>();
        }
    }
}
