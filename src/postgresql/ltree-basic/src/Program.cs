using System;
using System.Linq;
using System.Runtime.Loader;
using System.Threading;
using Dapper;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Db.Seed();

            PrintTree();
            PrintLevels();
            PrintSubPaths();

            CutBranch();
            PrintTree();

            RestoreBranch();
            PrintTree();

            CloneBranch();
            PrintTree();

            var resetEvent = new ManualResetEventSlim();

            AssemblyLoadContext.Default.Unloading += ctx =>
            {
                resetEvent.Set();
            };

            Console.WriteLine("Waiting for termination...");
            resetEvent.Wait();
        }

        private static void PrintTree()
        {
            Console.WriteLine("Children count");

            using (var connection = Db.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.AllResultTypesAreUnknown = true;
                command.CommandText = "SELECT letter, path, nlevel(path) FROM ltree_sample";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"  Letter: {reader.GetString(0)}; Path: {reader.GetString(1)}; Level: {reader.GetString(2)}");
                }
            }
        }

        private static void PrintLevels()
        {
            Console.WriteLine("Levels");

            using (var connection = Db.OpenConnection())
            {
                var count = connection.ExecuteScalar("SELECT count(*) FROM ltree_sample WHERE 'A' @> path");
                Console.WriteLine($"  Nbr of nodes including 'A' (should be 7): {count}");

                count = connection.ExecuteScalar("SELECT count(*) FROM ltree_sample WHERE 'A.C' @> path");
                Console.WriteLine($"  Nbr of nodes including 'A.C' (should be 4): {count}");
            }
        }

        private static void PrintSubPaths()
        {
            Console.WriteLine("Sub-paths of 'A.C' (including 'C' itself)");

            using (var connection = Db.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.AllResultTypesAreUnknown = true;
                command.CommandText = "SELECT letter, subpath(path, 1) FROM ltree_sample WHERE path <@ 'A.C'";

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"  Letter: {reader.GetString(0)}; Path: {reader.GetString(1)}");
                }
            }
        }

        private static void CutBranch()
        {
            Console.WriteLine("Cut off branch 'C'");

            using (var connection = Db.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.AllResultTypesAreUnknown = true;
                command.CommandText = "UPDATE ltree_sample SET path = subpath(path, nlevel('A.C')-1) WHERE path <@ 'A.C'";

                command.ExecuteNonQuery();

                command.AllResultTypesAreUnknown = false;
                command.CommandText = "SELECT nlevel(path) FROM ltree_sample WHERE letter = 'C'";

                var level = (int)command.ExecuteScalar();

                Console.WriteLine($"  Level of 'C': {level}");
            }
        }

        private static void RestoreBranch()
        {
            Console.WriteLine("Restore branch 'C' to 'A'");

            using (var connection = Db.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.AllResultTypesAreUnknown = true;
                command.CommandText =
                    @"UPDATE ltree_sample
                    SET path = 'A' || subpath(path, nlevel('C')-1)
                    WHERE path <@ 'C'";

                command.ExecuteNonQuery();

                command.AllResultTypesAreUnknown = false;
                command.CommandText = "SELECT nlevel(path) FROM ltree_sample WHERE letter = 'C'";

                var level = (int)command.ExecuteScalar();

                Console.WriteLine($"  Level of 'C': {level}");
            }
        }

        private static void CloneBranch()
        {
            Console.WriteLine("Clone branch 'C' to 'G'");

            using (var connection = Db.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.AllResultTypesAreUnknown = true;
                command.CommandText =
                    @"INSERT INTO ltree_sample (letter, path) (
                        SELECT letter, 'A.B.G' || subpath(path, nlevel('A.C')-1)
                        FROM ltree_sample
                        WHERE path <@ 'A.C'
                    )";

                command.ExecuteNonQuery();
            }
        }
    }
}
