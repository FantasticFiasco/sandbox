using System;
using System.Collections.Generic;
using Dapper;
using FileSystem;
using Shared;

namespace Benchmark
{
    public static class Seed
    {
        public static void Nodes(Db db)
        {
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

            using (var writer = db.Connection.BeginTextImport("COPY node (id, name, path) FROM STDIN"))
            {
                foreach (var node in nodes)
                {
                    writer.Write($"{node.Id}\t{node.Name}\t{node.Path}\n");
                }
            }
        }

        public static void PopulateTable(Db db)
        {
            // Populate with:
            // - A
            //   - B
            //     - G
            //   - C
            //     - D
            //     - E
            //     - F
            var idA = Db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idA}', 'A', '{idA}')");

            var idB = Db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idB}', 'B', '{idA}.{idB}')");

            var idG = Db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idG}', 'G', '{idA}.{idB}.{idG}')");

            var idC = Db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idC}', 'C', '{idA}.{idC}')");

            var idD = Db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idD}', 'D', '{idA}.{idC}.{idD}')");

            var idE = Db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idE}', 'E', '{idA}.{idC}.{idE}')");

            var idF = Db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idF}', 'F', '{idA}.{idC}.{idF}')");
        }
    }
}
