using System;
using System.Collections.Generic;
using System.Linq;
using FileSystem;
using Npgsql;
using Shouldly;
using Xunit;

namespace Test
{
    public class RepositoryShould
    {
        private static readonly Db Db;
        private static readonly Repository Repository;

        static RepositoryShould()
        {
            Db = new Db();
            Db.SetupTable();

            Repository = new Repository(Db.Connection);

            PopulateTable();
        }

        [Theory]
        [InlineData(1, 10000)]
        [InlineData(2, 10000 * 5)]
        [InlineData(3, 10000 * 5 * 5)]
        [InlineData(4, 10000 * 5 * 5 * 5)]
        [InlineData(5, 0)]
        public void ReturnNodesGivenLevel(int level, int expectedCount)
        {
            // Act
            var actual = Repository.GetNodesOnLevel(level);

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
            var node = Repository.GetFirstNodeOnLevel(level);

            // Act
            var actual = Repository.GetDescendants(node);

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
            var node = Repository.GetFirstNodeOnLevel(level);

            // Act
            var actual = Repository.GetAncestors(node);

            // Assert
            actual.Count().ShouldBe(expectedCount);
        }

        private static void PopulateTable()
        {
            Console.WriteLine("Create nodes...");

            var nodes = new List<Node>();

            // Populate with:
            // - 10.000 root nodes
            // - Each node has 5 children
            // - That goes on until we have four levels
            // It would mean that we should create
            //   10.000 * (1 + 5 * 5² + 5³) = 1.560.000 nodes
            for (int levelOneIndex = 0; levelOneIndex < 10000; levelOneIndex++)
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
