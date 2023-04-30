using Cccat.Infra;
using Cccat.Infra.Seed;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Queue.Consumer.Helpers
{
    public static class DatabaseHelper
    {
        private static void ExecutarMigrations(DatabaseContext context)
        {
            if (context.Database.IsInMemory()) return;

            context.Database.Migrate();
        }

        public static async Task ExecutarSeedDados(this IHost host)
        {
            var serviceProvider = host.Services;
            using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var environment = serviceScope.ServiceProvider.GetRequiredService<IHostEnvironment>();

            var dbContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

            if (environment.IsDevelopment())
                await dbContext.Database.EnsureCreatedAsync();

            ExecutarMigrations(dbContext);
            await SeedData.CriarDados(dbContext);
        }
    }
}
