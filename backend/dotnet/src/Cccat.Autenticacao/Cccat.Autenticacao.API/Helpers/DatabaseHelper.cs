using Cccat.Autenticacao.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Autenticacao.API.Helpers
{
	public static class DatabaseHelper
	{
		private static void ExecutarMigrations(DatabaseContext context)
		{
			if (context.Database.IsInMemory()) return;

			context.Database.Migrate();
		}

		public static async Task ExecutarSeedDados(this IApplicationBuilder app)
		{
			using var serviceScope = app.ApplicationServices.CreateScope();
			var dbContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

			if (!dbContext.Database.GetMigrations().Any())
				await dbContext.Database.EnsureCreatedAsync();

			ExecutarMigrations(dbContext);
		}
	}
}
