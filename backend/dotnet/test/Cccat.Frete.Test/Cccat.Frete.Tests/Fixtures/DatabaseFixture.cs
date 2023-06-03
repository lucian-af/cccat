using Cccat.Frete.Infra.Database;
using Cccat.Frete.Infra.Seed;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Frete.Tests.Fixtures
{
    [CollectionDefinition(nameof(DatabaseFixtureCollection))]
    public class DatabaseFixtureCollection : ICollectionFixture<DatabaseFixture> { }

    public class DatabaseFixture : IDisposable
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            DbContext.Database.EnsureDeletedAsync().Wait();
            DbContext.Dispose();
        }

        ~DatabaseFixture()
            => Dispose(false);
    }
}
