using Cccat.Infra;
using Cccat.Infra.Seed;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Tests.Fixtures
{
    [CollectionDefinition(nameof(DatabaseFixtureCollection))]
    public class DatabaseFixtureCollection : ICollectionFixture<DatabaseFixture> { }

    public class DatabaseFixture
    {
        public readonly DatabaseContext DbContext;

        public DatabaseFixture()
            => DbContext = CriarDatabaseInMemory();

        private static DatabaseContext CriarDatabaseInMemory()
        {
            var dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseInMemoryDatabase("CCCAT");
            var dbContext = new DatabaseContext(dbOptions.Options);
            SeedData.CriarDados(dbContext).Wait();
            return dbContext;
        }
    }
}
