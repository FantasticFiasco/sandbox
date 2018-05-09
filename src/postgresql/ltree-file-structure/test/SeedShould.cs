using Xunit;

namespace Test
{
    public class SeedShould
    {
        [Fact]
        public void PopulateDatabase()
        {
            using (var connection = Db.OpenConnection())
            {
                Db.EnableExtension(connection);
                Db.RecreateTable(connection);
                Db.PopulateTable(connection);
            }
        }
    }
}
