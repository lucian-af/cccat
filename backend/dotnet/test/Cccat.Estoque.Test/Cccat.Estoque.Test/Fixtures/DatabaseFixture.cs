using Cccat.Estoque.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Estoque.Tests.Fixtures
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
			return dbContext;
		}

		public async Task LimparBase()
			=> await DbContext.Database.EnsureDeletedAsync();

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			LimparBase().Wait();
			DbContext.Dispose();
		}

		~DatabaseFixture()
			=> Dispose(false);
	}
}
