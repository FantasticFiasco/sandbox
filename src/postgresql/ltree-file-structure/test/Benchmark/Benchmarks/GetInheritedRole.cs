using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using FileSystem;
using Shared;

namespace Benchmark.Benchmarks
{
    [RPlotExporter, RankColumn]
    public class GetInheritedRole
    {
        private Db db;
        private UserPermissionsRepository userPermissionsRepository;
        private Node nodeOnFirstLevel;
        private Node nodeOnSecondLevel;
        private Node nodeOnThirdLevel;
        private Node nodeOnFourthLevel;
        private Node nodeOnFifthLevel;

        [GlobalSetup]
        public void Setup()
        {
            db = new Db();
            db.SetupTables();

            userPermissionsRepository = new UserPermissionsRepository(db.Connection);

            Seed.Nodes(db);

            var nodeRepository = new NodeRepository(db.Connection);

            // Add user "John Doe" with role "Administrator" to a node on first level
            nodeOnFirstLevel = nodeRepository.GetFirstNodeOnLevel(1);
            userPermissionsRepository.Add("John Doe", nodeOnFirstLevel.Id, "administrator");

            // Get one descendant on each level under the first level node
            var descendants = nodeRepository.GetDescendants(nodeOnFirstLevel);

            nodeOnSecondLevel = descendants.First(descendant => descendant.Level() == 2);
            nodeOnThirdLevel = descendants.First(descendant => descendant.Level() == 3);
            nodeOnFourthLevel = descendants.First(descendant => descendant.Level() == 4);
            nodeOnFifthLevel = descendants.First(descendant => descendant.Level() == 5);
        }

        [Benchmark]
        public UserPermissions[] OnSecondLevel() => userPermissionsRepository.GetForNode(nodeOnSecondLevel.Id);

        [Benchmark]
        public UserPermissions[] OnThirdLevel() => userPermissionsRepository.GetForNode(nodeOnThirdLevel.Id);

        [Benchmark]
        public UserPermissions[] OnFourthLevel() => userPermissionsRepository.GetForNode(nodeOnFourthLevel.Id);

        [Benchmark]
        public UserPermissions[] OnFifthLevel() => userPermissionsRepository.GetForNode(nodeOnFifthLevel.Id);
    }
}
