using Cccat.Infra;
using Cccat.Infra.Seed;
using Microsoft.EntityFrameworkCore;

namespace Cccat.API.Helpers
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
            var serviceProvider = app.ApplicationServices;
            using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var environment = serviceScope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var dbContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

            if (environment.IsDevelopment())
                await dbContext.Database.EnsureCreatedAsync();

            ExecutarMigrations(dbContext);
            await SeedData.CriarDados(dbContext);
        }
    }
}
