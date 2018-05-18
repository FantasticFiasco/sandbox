using System;
using System.Linq;
using Dapper;
using FileSystem;
using Shouldly;
using Xunit;

namespace Test
{
    public class UserPermissionsShould
    {
        private readonly Db db;
        private readonly NodeRepository nodeRepository;
        private readonly UserPermissionsRepository userPermissionsRepository;
        private readonly string userId;

        private Role administratorRole;
        private Role operatorRole;
        private Role viewerRole;
        private Operation readOperation;
        private Operation writeOperation;
        private Operation executeOperation;

        public UserPermissionsShould()
        {
            db = new Db();
            db.SetupTable();

            nodeRepository = new NodeRepository(db.Connection);
            userPermissionsRepository = new UserPermissionsRepository(db.Connection);
            userId = "John Doe";

            PopulateTable();
        }

        [Fact]
        public void ReturnDescendantsOfA()
        {
            // Arrange
            var nodeA = nodeRepository.GetNodesOnLevel(1).Single();

            // Act
            var descendants = nodeRepository.GetDescendants(nodeA);

            // Assert
            descendants.Select(node => node.Name).OrderBy(_ => _).ShouldBe(new[] { "B", "C", "D", "E", "F", "G" });
        }

        [Fact]
        public void ReturnDescendantsOfB()
        {
            // Arrange
            var nodeB = nodeRepository.GetNodesOnLevel(2).Single(node => node.Name == "B");

            // Act
            var descendants = nodeRepository.GetDescendants(nodeB);

            // Assert
            descendants.Select(node => node.Name).OrderBy(_ => _).ShouldBe(new[] { "G" });
        }

        [Fact]
        public void ReturnDescendantsOfC()
        {
            // Arrange
            var nodeC = nodeRepository.GetNodesOnLevel(2).Single(node => node.Name == "C");

            // Act
            var descendants = nodeRepository.GetDescendants(nodeC);

            // Assert
            descendants.Select(node => node.Name).OrderBy(_ => _).ShouldBe(new[] { "D", "E", "F" });
        }

        [Fact]
        public void ReturnDescendantsOfD()
        {
            // Arrange
            var nodeD = nodeRepository.GetNodesOnLevel(3).Single(node => node.Name == "D");

            // Act
            var descendants = nodeRepository.GetDescendants(nodeD);

            // Assert
            descendants.Select(node => node.Name).ShouldBeEmpty();
        }

        [Fact]
        public void ReturnDescendantsOfE()
        {
            // Arrange
            var nodeE = nodeRepository.GetNodesOnLevel(3).Single(node => node.Name == "E");

            // Act
            var descendants = nodeRepository.GetDescendants(nodeE);

            // Assert
            descendants.Select(node => node.Name).ShouldBeEmpty();
        }

        [Fact]
        public void ReturnDescendantsOfF()
        {
            // Arrange
            var nodeF = nodeRepository.GetNodesOnLevel(3).Single(node => node.Name == "F");

            // Act
            var descendants = nodeRepository.GetDescendants(nodeF);

            // Assert
            descendants.Select(node => node.Name).ShouldBeEmpty();
        }

        [Fact]
        public void ReturnDescendantsOfG()
        {
            // Arrange
            var nodeG = nodeRepository.GetNodesOnLevel(3).Single(node => node.Name == "F");

            // Act
            var descendants = nodeRepository.GetDescendants(nodeG);

            // Assert
            descendants.Select(node => node.Name).ShouldBeEmpty();
        }

        [Fact]
        public void AddUserAsAdministratorToA()
        {
            // Arrange
            var nodeA = nodeRepository.GetNodesOnLevel(1).Single(node => node.Name == "A");

            // Act
            userPermissionsRepository.Add(userId, nodeA, administratorRole);

            // Assert
            var userPermissions = userPermissionsRepository.GetForNode(nodeA);

            userPermissions.Length.ShouldBe(1);
            userPermissions[0].UserId.ShouldBe(userId);
            userPermissions[0].Roles.Count.ShouldBe(1);
            userPermissions[0].Roles[0].Id.ShouldBe(administratorRole.Id);
            userPermissions[0].Roles[0].Name.ShouldBe(administratorRole.Name);
            userPermissions[0].Roles[0].Operations.Count.ShouldBe(3);
            userPermissions[0].Roles[0].Operations.Any(operation => operation.Id == readOperation.Id).ShouldBeTrue();
            userPermissions[0].Roles[0].Operations.Any(operation => operation.Id == writeOperation.Id).ShouldBeTrue();
            userPermissions[0].Roles[0].Operations.Any(operation => operation.Id == executeOperation.Id).ShouldBeTrue();
        }

        private void PopulateTable()
        {
            // Populate with:
            // - A
            //   - B
            //     - G
            //   - C
            //     - D
            //     - E
            //     - F
            Console.WriteLine("Write nodes...");

            var idA = db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idA}', 'A', '{idA}')");

            var idB = db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idB}', 'B', '{idA}.{idB}')");

            var idG = db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idG}', 'G', '{idA}.{idB}.{idG}')");

            var idC = db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idC}', 'C', '{idA}.{idC}')");

            var idD = db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idD}', 'D', '{idA}.{idC}.{idD}')");

            var idE = db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idE}', 'E', '{idA}.{idC}.{idE}')");

            var idF = db.NewId();
            db.Connection.Execute($"INSERT INTO node (id, name, path) VALUES ('{idF}', 'F', '{idA}.{idC}.{idF}')");

            // Write roles
            Console.WriteLine("Write roles...");

            administratorRole = new Role(db.NewId(), "Administrator");
            db.Connection.Execute($"INSERT INTO role (id, name) VALUES ('{administratorRole.Id}', '{administratorRole.Name}')");

            operatorRole = new Role(db.NewId(), "Operator");
            db.Connection.Execute($"INSERT INTO role (id, name) VALUES ('{operatorRole.Id}', '{operatorRole.Name}')");

            viewerRole = new Role(db.NewId(), "Viewer");
            db.Connection.Execute($"INSERT INTO role (id, name) VALUES ('{viewerRole.Id}', '{viewerRole.Name}')");

            // Write operations
            Console.WriteLine("Write operations...");

            readOperation = new Operation { Id = db.NewId(), Name = "Read" };
            writeOperation = new Operation { Id = db.NewId(), Name = "Write" };
            executeOperation = new Operation { Id = db.NewId(), Name = "Execute" };

            db.Connection.Execute($"INSERT INTO operation (id, name, role_id) VALUES ('{readOperation.Id}', '{readOperation.Name}', '{administratorRole.Id}')");
            db.Connection.Execute($"INSERT INTO operation (id, name, role_id) VALUES ('{writeOperation.Id}', '{writeOperation.Name}', '{administratorRole.Id}')");
            db.Connection.Execute($"INSERT INTO operation (id, name, role_id) VALUES ('{executeOperation.Id}', '{executeOperation.Name}', '{administratorRole.Id}')");

            db.Connection.Execute($"INSERT INTO operation (id, name, role_id) VALUES ('{readOperation.Id}', '{readOperation.Name}', '{operatorRole.Id}')");
            db.Connection.Execute($"INSERT INTO operation (id, name, role_id) VALUES ('{writeOperation.Id}', '{writeOperation.Name}', '{operatorRole.Id}')");

            db.Connection.Execute($"INSERT INTO operation (id, name, role_id) VALUES ('{readOperation.Id}', '{readOperation.Name}', '{viewerRole.Id}')");
        }
    }
}
