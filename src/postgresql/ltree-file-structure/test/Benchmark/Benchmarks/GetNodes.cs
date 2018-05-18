using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using FileSystem;
using Shared;

namespace Benchmark.Benchmarks
{
    [RPlotExporter, RankColumn]
    public class GetNodes
    {
        private Db db;
        private NodeRepository nodeRepository;

        [GlobalSetup]
        public void Setup()
        {
            db = new Db();
            db.SetupTables();

            nodeRepository = new NodeRepository(db.Connection);

            Seed.Nodes(db);
        }

        [Benchmark]
        [Arguments(1)]
        [Arguments(2)]
        [Arguments(3)]
        [Arguments(4)]
        [Arguments(5)]
        public Node[] OnLevel(int level) => nodeRepository.GetNodesOnLevel(level);
    }
}
