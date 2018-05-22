using System;
using System.Collections.Generic;
using System.Linq;
using FileSystem;
using Shared;
using Shouldly;
using Xunit;

namespace Test
{
    public class NodeRepositoryShould
    {
        private static readonly Db Db;
        private static readonly NodeRepository NodeRepository;

        static NodeRepositoryShould()
        {
            Db = new Db();
            Db.SetupTables();

            NodeRepository = new NodeRepository(Db.Connection);

            PopulateTable();
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 10 * 5)]
        [InlineData(3, 10 * 5 * 5)]
        [InlineData(4, 10 * 5 * 5 * 5)]
        [InlineData(5, 0)]
        public void ReturnNodesGivenLevel(int level, int expectedCount)
        {
            // Act
            var actual = NodeRepository.GetNodesOnLevel(level);

            // Assert
            actual.Count().ShouldBe(expectedCount);
        }

        [Theory]
        [InlineData(1, 5 + (5 * 5) + (5 * 5 * 5))]
        [InlineData(2, 5 + (5 * 5))]
        [InlineData(3, 5)]
        [InlineData(4, 0)]
        public void ReturnDescendantsGivenLevel(int level, int expectedCount)
        {
            // Arrange
            var node = NodeRepository.GetFirstNodeOnLevel(level);

            // Act
            var actual = NodeRepository.GetDescendants(node);

            // Assert
            actual.Count().ShouldBe(expectedCount);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        public void ReturnAncestorsGivenLevel(int level, int expectedCount)
        {
            // Arrange
            var node = NodeRepository.GetFirstNodeOnLevel(level);

            // Act
            var actual = NodeRepository.GetAncestors(node);

            // Assert
            actual.Count().ShouldBe(expectedCount);
        }

        [Fact]
        public void ReturnUserNodesGivenRoleOnLevelOne()
        {
            // Arrange
            var nodeOnLevelOne = NodeRepository.GetFirstNodeOnLevel(1);

            var userPermissionsRepository = new UserPermissionsRepository(Db.Connection);

            userPermissionsRepository.Add("John Doe", nodeOnLevelOne.Id, "administrator");

            // Act
            var actual = NodeRepository.GetNodesByUser("John Doe");

            // Assert
            actual.Length.ShouldBe(1 + 5 + (5 * 5) + (5 * 5 * 5));
        }

        [Fact]
        public void ReturnUserNodesGivenRoleOnLevelOneAndTwoInSameTree()
        {
            // Arrange
            var nodeOnLevelOne = NodeRepository.GetFirstNodeOnLevel(1);

            var nodeOnLevelTwo = NodeRepository
                .GetDescendants(nodeOnLevelOne)
                .First(node => node.Level() == 2);

            var userPermissionsRepository = new UserPermissionsRepository(Db.Connection);

            userPermissionsRepository.Add("John Doe", nodeOnLevelOne.Id, "administrator");
            userPermissionsRepository.Add("John Doe", nodeOnLevelTwo.Id, "administrator");

            // Act
            var actual = NodeRepository.GetNodesByUser("John Doe");

            // Assert
            actual.Length.ShouldBe(1 + 5 + (5 * 5) + (5 * 5 * 5));
        }

        private static void PopulateTable()
        {
            Console.WriteLine("Create nodes...");

            var nodes = new List<Node>();

            // Populate with:
            // - 10 root nodes
            // - Each node has 5 children
            // - That goes on until we have three levels
            // It would mean that we should create
            //   10 * (1 + 5 * 5²) = 1.560 nodes
            for (int levelOneIndex = 0; levelOneIndex < 10; levelOneIndex++)
            {
                var levelOneId = Db.NewId();
                var levelOneName = $"{levelOneIndex}";
                var levelOnePath = $"{levelOneId}";

                nodes.Add(new Node { Id = levelOneId, Name = levelOneName, Path = levelOnePath });

                for (int levelTwoIndex = 0; levelTwoIndex < 5; levelTwoIndex++)
                {
                    var levelTwoId = Db.NewId();
                    var levelTwoName = $"{levelOneName}.{levelTwoIndex}";
                    var levelTwoPath = $"{levelOnePath}.{levelTwoId}";

                    nodes.Add(new Node { Id = levelTwoId, Name = levelTwoName, Path = levelTwoPath });

                    for (int levelThreeIndex = 0; levelThreeIndex < 5; levelThreeIndex++)
                    {
                        var levelThreeId = Db.NewId();
                        var levelThreeName = $"{levelTwoName}.{levelThreeIndex}";
                        var levelThreePath = $"{levelTwoPath}.{levelThreeId}";

                        nodes.Add(new Node { Id = levelThreeId, Name = levelThreeName, Path = levelThreePath });

                        for (int levelFourIndex = 0; levelFourIndex < 5; levelFourIndex++)
                        {
                            var levelFourId = Db.NewId();
                            var levelFourName = $"{levelThreeName}.{levelFourIndex}";
                            var levelFourPath = $"{levelThreePath}.{levelFourId}";

                            nodes.Add(new Node { Id = levelFourId, Name = levelFourName, Path = levelFourPath });
                        }
                    }
                }
            }

            Console.WriteLine("Write nodes...");

            using (var writer = Db.Connection.BeginTextImport("COPY node (id, name, path) FROM STDIN"))
            {
                foreach (var node in nodes)
                {
                    writer.Write($"{node.Id}\t{node.Name}\t{node.Path}\n");
                }
            }
        }
    }
}
